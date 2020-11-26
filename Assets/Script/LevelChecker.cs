using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChecker : MonoBehaviour
{
    private GameObject parentLevel;
    private RoadCurrent parentLevelRC;
    private List<Transform> listT = new List<Transform>();
    private List<Transform> listM = new List<Transform>();
    private ObjectPool pool;
    private Transform[] searchArry;

    private void Awake()
    {
        pool = GameObject.Find("ObjectPool").GetComponent<ObjectPool>();
        parentLevel = this.gameObject.transform.parent.gameObject;
        parentLevelRC = parentLevel.GetComponent<RoadCurrent>();
    }

    private void Start() //第一個level1的初始化
    {
        if (RoadCurrent.waitToDeleteLevel == null)
        {
            listT = Utility.GameObjectRelate.SearchChildsPartName(parentLevel.transform, "ToastSpawnTrigger");
            foreach (var v in listT)
            {
                GameObject toast = pool.GetObj("Toast1", v.transform.position, v.transform.rotation);
                toast.transform.SetParent(parentLevel.transform);

            }
            listM = Utility.GameObjectRelate.SearchChildsPartName(parentLevel.transform, "MinionSpawnTrigger");
            foreach (var v in listM)
            {
                GameObject minion = pool.GetObj("Minion1", v.transform.position, v.transform.rotation);
                minion.transform.SetParent(parentLevel.transform);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        listM.Clear();
        listT.Clear();
        if (other.gameObject.tag == "Player")
        {
            if (RoadCurrent.waitToDeleteLevel != null)
            {
                searchArry = RoadCurrent.waitToDeleteLevel.GetComponentsInChildren<Transform>();
                foreach (var child in searchArry)
                {
                    if (child.gameObject.name == "Toast1")
                    {
                        pool.RecycleObj(child.gameObject);
                    }
                    if (child.gameObject.name == "Minion1")
                    {
                        pool.RecycleObj(child.gameObject);
                    }
                }
                parentLevelRC.RoadRecovery();
            }
            parentLevelRC.RoadLoad();
        }
    }
}
