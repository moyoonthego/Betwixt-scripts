using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour {

    public static int scoreValue = 0;
    public static Text score;

    void Start()
    {
        score = GetComponent<Text>();
    }

    public void ChangeScore()
    {
        scoreValue += 10;
        score.text = scoreValue.ToString();
    }

    public static void ResetScore()
    {
        scoreValue = 0;
        score.text = "0";
    }
}
