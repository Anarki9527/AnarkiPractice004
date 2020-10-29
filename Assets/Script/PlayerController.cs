using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;



// [RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // public float speed = 0.1f;
    private Rigidbody2D rigid2D;
    private float speed_x_constraint = 50f;
    private SpriteRenderer sR;
    // public int playerHP = 10;
    public static int score = 0;
    public static int hp = 5;
    private bool invincible = false;
    private GameObject lose;
    private GameObject g_Canvas;
    private Transform playerTransform;
    private GameObject restart;
    private GameObject close;

    // Start is called before the first frame update
    void Start()
    {
        rigid2D = this.gameObject.GetComponent<Rigidbody2D>();
        sR = this.gameObject.GetComponent<SpriteRenderer>();
        lose = Utility.AssetRelate.ResourcesLoadCheckNull<GameObject>("Prefabs/lose");
        g_Canvas = GameObject.Find("Canvas");
        playerTransform = this.gameObject.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        //move
        // if (Input.GetKey(KeyCode.RightArrow))
        // {
        // sR.flipX = true;
        // rigid2D.velocity = new Vector2(speed_x_constraint, rigid2D.velocity.y);
        // rigid2D.AddForce(new Vector2(100*speed,0),ForceMode2D.Force);
        // this.gameObject.transform.position += new Vector3(speed, 0, 0);
        // }
        // if (Input.GetKey(KeyCode.LeftArrow))
        // {
        //     SpriteRenderer sR = this.GetComponent<SpriteRenderer>();
        //     sR.flipX = false;
        //     rigid2D.velocity = new Vector2(-speed_x_constraint, rigid2D.velocity.y);
        //     // rigid2D.AddForce(new Vector2(-100*speed,0),ForceMode2D.Force);
        //     // this.gameObject.transform.position -= new Vector3(speed, 0, 0);
        // }

        // if (Input.GetKeyDown(KeyCode.Z))
        // {
        //     rigid2D.AddForce(new Vector2(0, 250), ForceMode2D.Impulse);
        // }
        MovementX();
        ControlSpeed();
        TryJump();

        if (hp == 0)
        {
            // Destroy(this.gameObject);
            Utility.GameObjectRelate.SetObjectActiveToggle(this.gameObject);
            Utility.GameObjectRelate.InstantiateGameObject(g_Canvas, lose);
            GameObject restart = GameObject.Find("Canvas/lose(Clone)/Restart");
            GameObject close = GameObject.Find("Canvas/lose(Clone)/Close");

            EventTriggerListener.Get(restart).onUp += OnUp;
            EventTriggerListener.Get(close).onUp += OnUp;


        }

        if (playerTransform.position.y <= -10)
        {
            hp = 0;
        }

        //速度限制
        // if (rigid2D.velocity.x > speed_x_constraint)
        // {
        //     rigid2D.velocity = new Vector2(speed_x_constraint, rigid2D.velocity.y);
        // }

        // if (rigid2D.velocity.x < -speed_x_constraint)
        // {
        //     rigid2D.velocity = new Vector2(-speed_x_constraint, rigid2D.velocity.y);
        // }


    }

    [Header("目前的水平速度")]
    public float speedX = 20;

    [Header("目前的水平方向")]
    public float horizontalDirection = 1;//數值會在 -1~1之間

    const string HORIZONTAL = "Horizontal";

    [Header("水平推力")]
    [Range(0, 999)]
    public float xForce = 750;

    //目前垂直速度
    float speedY;

    [Header("最大水平速度")]
    public float maxSpeedX;

    [Header("垂直向上推力")]
    public float yForce = 500;

    [Header("感應地板的距離")]
    [Range(0, 0.5f)]
    public float distance;

    [Header("偵測地板的射線起點")]
    public Transform groundCheck;

    [Header("地面圖層")]
    public LayerMask groundLayer;

    public bool grounded;

    public void ControlSpeed()
    {
        speedX = rigid2D.velocity.x;
        speedY = rigid2D.velocity.y;
        float newSpeedX = Mathf.Clamp(speedX, -maxSpeedX, maxSpeedX);
        rigid2D.velocity = new Vector2(newSpeedX, speedY);
    }

    public bool JumpKey
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Z);
        }
    }

    void TryJump()
    {
        // if (IsGround && JumpKey)
        if (JumpKey)
        {
            rigid2D.AddForce(Vector2.up * yForce);
        }
    }

    void MovementX()
    {
        // horizontalDirection = Input.GetAxis(HORIZONTAL);
        rigid2D.AddForce(new Vector2(xForce * horizontalDirection, 0));
    }

    //在玩家的底部射一條很短的射線 如果射線有打到地板圖層的話 代表正在踩著地板
    bool IsGround
    {
        get
        {
            Vector2 start = groundCheck.position;
            Vector2 end = new Vector2(start.x, start.y - distance);

            Debug.DrawLine(start, end, Color.blue);
            grounded = Physics2D.Linecast(start, end, groundLayer);
            return grounded;
        }
    }


    private void OnCollisionEnter2D(Collision2D coll)
    {

    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (!invincible)
            {
                hp -= 1;
                Debug.Log(hp);
                invincible = true;
                // sR.color = new Color(100f, 100f, 100f);
                sR.color = new Color(1f, 1f, 1f, 0.5f);
                Invoke("InvincibleTime", 2);
            }

        }

    }

    public void InvincibleTime()
    {
        Debug.Log("2sec");
        sR.color = new Color(1f, 1f, 1f, 1f);
        invincible = false;
    }

    private void OnUp(GameObject Btn)
    {
        switch (Btn.name)
        {
            case "Restart":
                Restart();
                break;
            case "Close":
                Application.Quit();
                break;
        }
    }

    private void Restart()
    {
        Debug.Log("restart");
                    // Utility.GameObjectRelate.SetObjectActiveToggle(this.gameObject);

    }


}