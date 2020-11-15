using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text score;
    public Text hp;
    public static int currentHealth = 5;
    public static int currentScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.Find("Canvas/PlayerUI/Score").GetComponent<Text>();
        hp = GameObject.Find("Canvas/PlayerUI/HP").GetComponent<Text>();
        Debug.Log("start");
    }

    // Update is called once per frame
    void Update()
    {

    }
    // public static void HpUpdate(string value)
    // {
    //     score.text = value;
    // }

    private void TakeDamage(int amount)
    {
        currentHealth -= amount;
        HeatlhUpdate();
        Debug.Log(currentHealth);

    }
    private void HeatlhUpdate()
    {
        hp.text = currentHealth.ToString();
    }
    private void GetScore(int amount)
    {
        currentScore += amount;
        ScoreUpdate();
        Debug.Log(currentScore);
    }
    private void ScoreUpdate()
    {
        score.text = currentScore.ToString();
    }

}
