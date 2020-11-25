using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjectPool : MonoBehaviour
{
    public GameObject[] levelPrafabs;
    private int initailSize = 2;

    private Queue<GameObject> m_pool = new Queue<GameObject>();

    void Awake()
    {
        for (int cnt = 0; cnt < initailSize; cnt++)
        {
            GameObject go = Instantiate(levelPrafabs[cnt]) as GameObject;
            m_pool.Enqueue(go); go.SetActive(false);
        }
    }

    public GameObject ReUse(Vector2 position)
    {
        if (m_pool.Count > 0)
        {
            GameObject reuse = m_pool.Dequeue();
            reuse.transform.position = position;
            reuse.SetActive(true);
            return reuse;
        }
        else
        {
            int type = Random.Range(0, levelPrafabs.Length);
            GameObject go = Instantiate(levelPrafabs[type]) as GameObject;
            go.transform.position = position;
            return go;
        }
    }


    public void Recovery(GameObject recovery)
    {
        m_pool.Enqueue(recovery);
        recovery.SetActive(false);
    }
}
