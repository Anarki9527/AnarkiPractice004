using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCurrent : MonoBehaviour
{
    private Transform player;
    public static int nowRoadX;
    public static GameObject waitToDeleteLevel;
    private LevelObjectPool pool;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nowRoadX = (int)this.transform.position.x;
        pool = GameObject.Find("LevelObjectPool").GetComponent<LevelObjectPool>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
    }

    public void RoadLoad()
    {
        waitToDeleteLevel = this.gameObject;  //在載入新關卡的同時，記錄自己為待刪除物件。
        RoadManager._instance.GenerateRoad();
    }

    public void RoadRecovery()
    {
        pool.Recovery(waitToDeleteLevel);
    }
}
