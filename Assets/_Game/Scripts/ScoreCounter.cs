using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public Text scoreLabel;
    private int currentScore;

    private void Start()
    {
        //scoreLabel = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        currentScore = 0;
        ScoreUpdate();
    }

    private void Update()
    {
        ScoreUpdate();
    }

    public void ScoreUpdate()
    {
        scoreLabel.text = "Score: " + currentScore;
    }

    public void AddScore()
    {
        currentScore++;
    }
}
