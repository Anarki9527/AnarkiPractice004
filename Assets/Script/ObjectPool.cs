using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    /// <summary>
    /// 物件池
    /// </summary>
    private Dictionary<string, List<GameObject>> pool;

    /// <summary>
    /// 預設體
    /// </summary>
    private Dictionary<string, GameObject> prefabs;
    #region 單例
    private static ObjectPool instance;
    private ObjectPool()
    {
        pool = new Dictionary<string, List<GameObject>>();
        prefabs = new Dictionary<string, GameObject>();
    }
    public static ObjectPool GetInstance()
    {
        if (instance == null)
        {
            instance = new ObjectPool();
        }
        return instance;
    }
    #endregion

    /// <summary>
    /// 從物件池中獲取物件
    /// </summary>
    /// <param name="objName"></param>
    /// <returns></returns>
    public GameObject GetObj(string objName, Vector3 position, Quaternion quaternion)
    {
        //結果物件
        GameObject result = null;
        //判斷是否有該名字的物件池
        if (pool.ContainsKey(objName))
        {
            //物件池裡有物件
            if (pool[objName].Count > 0)
            {
                //獲取結果
                result = pool[objName][0];
                //啟用物件
                result.transform.position = position;
                result.transform.rotation = quaternion;
                result.SetActive(true);
                //從池中移除該物件
                pool[objName].Remove(result);
                //返回結果
                return result;
            }
        }
        //如果沒有該名字的物件池或者該名字物件池沒有物件

        GameObject prefab = null;
        //如果已經載入過該預設體
        if (prefabs.ContainsKey(objName))
        {
            prefab = prefabs[objName];
        }
        else     //如果沒有載入過該預設體
        {
            //載入預設體
            prefab = Resources.Load<GameObject>("Prefabs/" + objName);
            //更新字典
            prefabs.Add(objName, prefab);
        }

        //生成
        result = Object.Instantiate(prefab);
        result.transform.position = position;
        result.transform.rotation = quaternion;
        //改名（去除 Clone）
        result.name = objName;
        //返回
        return result;
    }

    /// <summary>
    /// 回收物件到物件池
    /// </summary>
    /// <param name="objName"></param>
    public void RecycleObj(GameObject obj)
    {
        //設定為非啟用
        obj.SetActive(false);
        //判斷是否有該物件的物件池
        if (pool.ContainsKey(obj.name))
        {
            //放置到該物件池
            pool[obj.name].Add(obj);
        }
        else
        {
            //建立該型別的池子，並將物件放入
            pool.Add(obj.name, new List<GameObject>() { obj });
        }

    }

}




