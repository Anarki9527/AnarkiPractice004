using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{

    // public RoadCurrent road1;
    // public RoadCurrent road2;             //这里保留了两个路径
    // public RoadCurrent road3;             //这里保留了两个路径
    public GameObject road1;
    public GameObject road2;             //这里保留了两个路径
    public GameObject road3;             //这里保留了两个路径 
    private Dictionary<int, GameObject> gameObjectDic = new Dictionary<int, GameObject>();

    public static RoadManager _instance;
    private LevelObjectPool pool;
    void Awake()
    {
        _instance = this;
        gameObjectDic.Add(0, road1);
        gameObjectDic.Add(1, road2);
        gameObjectDic.Add(2, road3);
        pool = GameObject.Find("LevelObjectPool").GetComponent<LevelObjectPool>();
    }

    private void Update()
    {

    }

    public void GenerateRoad()
    {
        // int type = Random.Range(0, gameObjectDic.Count-1);
        // GameObject newRoad = Instantiate(gameObjectDic[type], new Vector2(RoadCurrent.nowRoadX + 280, 0), Quaternion.identity) as GameObject;
        RoadCurrent.nowRoadX += 280;
        pool.ReUse(new Vector2(RoadCurrent.nowRoadX, 0));
    }
}
