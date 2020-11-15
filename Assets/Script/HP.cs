using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    Text hp;
    // Start is called before the first frame update
    void Start()
    {
        hp =  this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // hp.text =  PlayerController.hp.ToString();
    }
}
