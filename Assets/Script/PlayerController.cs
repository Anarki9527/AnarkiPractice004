﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;
using UnityEngine.SceneManagement;


// [RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    #region 宣告
    private Rigidbody2D rigid2D;
    private SpriteRenderer sR;
    private bool invincible = false;  //撞到怪物後是否有無敵狀態
    private GameObject lose;  //Lose UI
    private GameObject end;   //End UI
    private GameObject g_Canvas;
    private Transform playerTransform;
    private GameObject restart;
    private GameObject close;
    private UIManager playerUI;
    private bool diedyet;  //玩家是否已死亡
    private ObjectPool pool;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rigid2D = this.gameObject.GetComponent<Rigidbody2D>();
        sR = this.gameObject.GetComponent<SpriteRenderer>();
        lose = Utility.AssetRelate.ResourcesLoadCheckNull<GameObject>("Prefabs/UI/Lose");
        end = Utility.AssetRelate.ResourcesLoadCheckNull<GameObject>("Prefabs/UI/End");
        g_Canvas = GameObject.Find("Canvas");
        playerUI = this.gameObject.GetComponent<UIManager>();
        playerTransform = this.gameObject.GetComponent<Transform>();
        pool = GameObject.Find("ObjectPool").GetComponent<ObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementX();
        ControlSpeed();
        TryJump();

        if (playerTransform.position.y <= -10 && diedyet == false)  //掉下去
        {
            playerUI.TakeDamage(playerUI.currentHealth);
            diedyet = true;
            Gameover(lose);
        }
    }


    #region 角色移動相關
    private float speedX = 10; //目前的水平速度
    private float horizontalDirection = 1;  //目前的水平方向 數值會在 -1~1之間
    private float xForce = 750;  //水平推力
    private float speedY;  //目前垂直速度
    private float maxSpeedX = 10;  //最大水平速度
    private float yForce = 700; //垂直向上推力
    private bool isGround = false;  //是否踩到地面
    private void ControlSpeed() //速度限制
    {
        speedX = rigid2D.velocity.x;
        speedY = rigid2D.velocity.y;
        float newSpeedX = Mathf.Clamp(speedX, -maxSpeedX, maxSpeedX);
        rigid2D.velocity = new Vector2(newSpeedX, speedY);
    }

    private bool JumpKey  //跳躍
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Z);
        }
    }

    void TryJump()  //符合條件跳躍
    {
        if (isGround && JumpKey)
        {
            rigid2D.AddForce(Vector2.up * yForce);
        }
    }

    void MovementX()  //持續往右移動
    {
        rigid2D.AddForce(new Vector2(xForce * horizontalDirection, 0));
    }
    #endregion


    #region isGround的設置 碰到地板就設定True 反之
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Level")
        {
            isGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Level")
        {
            isGround = false;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Level")
        {
            isGround = true;
        }
    }
    #endregion

    #region Trigger碰撞器
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")  //碰到敵人
        {
            if (!invincible)
            {
                playerUI.TakeDamage(1);
                if (playerUI.currentHealth <= 0)
                {
                    Gameover(lose);
                    return;
                }
                invincible = true;
                sR.color = new Color(1f, 1f, 1f, 0.5f);
                Invoke("InvincibleTime", 2);
            }
        }

        if (this.gameObject.name == "Player" && other.gameObject.tag == "ScoreGet")
        {
            playerUI.GetScore(10);
            pool.RecycleObj(other.gameObject);
        }

        if (other.gameObject.tag == "End" && other.gameObject.name == "End")  //關卡結束
        {
            Gameover(end);
            Destroy(other);
        }
    }
    #endregion


    public void InvincibleTime()  //被敵人擊中後的無敵時間
    {
        sR.color = new Color(1f, 1f, 1f, 1f);
        invincible = false;
    }

    private void OnUp(GameObject Btn)  //UI偵測
    {
        switch (Btn.name)
        {
            case "Restart":
                Restart();
                break;
            case "Close":
                SceneManager.LoadScene("Menu");
                break;
        }
    }

    private void Restart()   //重新開始
    {
        SceneManager.LoadScene("GameLevel1");
        sR.enabled = true;
        Utility.GameObjectRelate.ClearChildren(g_Canvas.GetComponent<Transform>());
        playerUI.currentHealth = 10;
        playerUI.currentScore = 0;
        xForce = 750;
        diedyet = false;
        rigid2D.simulated = true;


    }

    private void Gameover(GameObject calledUI) //遊戲結束
    {
        sR.enabled = false;
        rigid2D.simulated = false;
        Utility.GameObjectRelate.InstantiateGameObject(g_Canvas, calledUI);
        GameObject restart = GameObject.Find("Canvas/" + calledUI.name + "(Clone)/Restart");
        GameObject close = GameObject.Find("Canvas/" + calledUI.name + "(Clone)/Close");

        EventTriggerListener.Get(restart).onUp += OnUp;
        EventTriggerListener.Get(close).onUp += OnUp;
    }

}