using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListScores : MonoBehaviour
{

    Text score;

    void Start()
    {
        score = GameObject.Find("Scores").GetComponent<Text>();
        score.text = PlayerListLL.printAll();
        PlayerListLL.Start();
        UpdateScore.ResetScore();
    }

}

