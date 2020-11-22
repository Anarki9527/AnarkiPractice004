using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Text score;
    private Text hp;
    public int currentHealth = 5;
    public int currentScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.Find("Canvas/PlayerUI/Score").GetComponent<Text>();
        hp = GameObject.Find("Canvas/PlayerUI/HP").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        HeatlhUpdate();
    }
    private void HeatlhUpdate()
    {
        hp.text = currentHealth.ToString();
    }
    public void GetScore(int amount)
    {
        currentScore += amount;
        ScoreUpdate();
    }
    private void ScoreUpdate()
    {
        score.text = currentScore.ToString();
    }

}
