using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        // if (other.gameObject.tag == "Enemy")
        // {
        //     if (!invincible)
        //     {
        //         hp -= 1;
        //         Debug.Log(PlayerController.hp);
        //         invincible = true;
        //         sR.color = new Color(100f, 100f, 100f);
        //         // sR.color = new Color(100, 100, 100);

        //         InvincibleTime();
        //     }
        // }
        // if (collider.gameObject.tag == "Player")
        // {
        //                    PlayerController. hp -= 1;
        //         Debug.Log(PlayerController.hp);
        //         // invincible = true;
        //         // sR.color = new Color(100f, 100f, 100f);

        // }

    }
}
