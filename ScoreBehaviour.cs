using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreBehaviour : MonoBehaviour {

    int counter = 0;
    string realname;

    public void ValueChanged(string name)
    {
        GameObject.Find("High Score AddPanel").GetComponent<Animation>().Play("replaygoesout");
        realname = name;
        counter = 1;
    }

    void Update()
    {
        if (counter == 30)
        {
            PlayerListLL.players.Add(realname, UpdateScore.score.text);
            SceneManager.LoadScene("ScoreScene");
            counter = 0;
        } else if (counter >= 1) {
            counter++;
        }
    }
}
