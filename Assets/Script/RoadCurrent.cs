using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCurrent : MonoBehaviour
{
    private Transform player;
    public static int nowRoadX;
    private bool done = false;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nowRoadX = (int)this.transform.position.x;
    }

    void Update()
    {
        if (player.position.x > nowRoadX + 150 && !done)
        {
            RoadManager._instance.GenerateRoad();
            done = true;
            Debug.Log("go");
        }
        if (player.position.x > this.transform.position.x + 300)
        {
            GameObject.Find("LevelObjectPool").GetComponent<LevelObjectPool>().Recovery(this.gameObject);
        }

    }
}
