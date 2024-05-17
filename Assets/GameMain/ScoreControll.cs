using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreControll : MonoBehaviour
{
    [SerializeField] Text ScoreText;
    public int score;
    public float scoreTime;


    void Update()
    {
        UpdateScore();
    }
    void UpdateScore()
    {
        scoreTime += Time.deltaTime;
        if (scoreTime > 5f)
        {
            score++;
            scoreTime = 0;
            ScoreText.text = "¦a¤U²Ä" + score.ToString() + "¼h";
        }

    }
}
