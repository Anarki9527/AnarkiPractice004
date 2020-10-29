using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Text scoretext;
    // Start is called before the first frame update
    void Start()
    {
        scoretext =  this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoretext.text =  PlayerController.score.ToString();
    }
}
