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
    private LevelObjectPool pool;
    void Awake()
    {
        _instance = this;
        pool = GameObject.Find("LevelObjectPool").GetComponent<LevelObjectPool>();
    }

    private void Update()
    {

    }

    public void GenerateRoad()
    {
        RoadCurrent.nowRoadX += 280;
        pool.ReUse(new Vector2(RoadCurrent.nowRoadX, 0));
    }
}
