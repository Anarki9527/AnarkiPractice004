using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{

    public GameObject road1;
    public GameObject road2;
    public GameObject road3;
    private Dictionary<int, GameObject> gameObjectDic = new Dictionary<int, GameObject>();

    public static RoadManager _instance;
    private LevelObjectPool LevelPool;
    private List<Transform> listT = new List<Transform>();
    private List<Transform> listM = new List<Transform>();
    private ObjectPool pool;
    void Awake()
    {
        _instance = this;
        LevelPool = GameObject.Find("LevelObjectPool").GetComponent<LevelObjectPool>();
        pool = GameObject.Find("ObjectPool").GetComponent<ObjectPool>();

    }

    private void Start() 
    {
        GameObject newRoad = LevelPool.ReUse(new Vector2(RoadCurrent.nowRoadX, 0));
    }

    public void GenerateRoad()
    {
        RoadCurrent.nowRoadX += 280;
        GameObject newRoad = LevelPool.ReUse(new Vector2(RoadCurrent.nowRoadX, 0));
        listT = Utility.GameObjectRelate.SearchChildsPartName(newRoad.transform, "ToastSpawnTrigger");
        foreach (var v in listT)
        {
            GameObject toast = pool.GetObj("Toast1", v.transform.position, v.transform.rotation);
            toast.transform.SetParent(newRoad.transform);
        }
        listM = Utility.GameObjectRelate.SearchChildsPartName(newRoad.transform, "MinionSpawnTrigger");
        foreach (var v in listM)
        {
            GameObject minion = pool.GetObj("Minion1", v.transform.position, v.transform.rotation);
            minion.transform.SetParent(newRoad.transform);
        }        

    }
}
