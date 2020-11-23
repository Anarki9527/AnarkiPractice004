using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChecker : MonoBehaviour
{
    GameObject parentLevel;
    RoadCurrent parentLevelRC;
    private void Awake()
    {
        parentLevel = this.gameObject.transform.parent.gameObject;
        parentLevelRC = parentLevel.GetComponent<RoadCurrent>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (RoadCurrent.waitToDeleteLevel != null)
            {
                parentLevelRC.RoadRecovery();
            }
            parentLevelRC.RoadLoad();

        }
    }
}
