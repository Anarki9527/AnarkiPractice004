using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastObjectPool : MonoBehaviour
{
    public GameObject Prafabs;
    private int initailSize = 80;

    private Queue<GameObject> m_pool = new Queue<GameObject>();

    void Awake()
    {
        for (int cnt = 0; cnt < initailSize; cnt++)
        {
            GameObject go = Instantiate(Prafabs) as GameObject;
            go.name = Prafabs.name;
            go.transform.SetParent(this.gameObject.transform);
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
            GameObject go = Instantiate(Prafabs) as GameObject;
            go.name = Prafabs.name;
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
