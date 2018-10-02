using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBehaviour : MonoBehaviour {
    public GameObject replaypanel;
    public GameObject scores;
    public GameObject replay;
    public CircleCollider2D replaybutton;

    void Awake () {
        replaypanel = GameObject.Find("Replay Panel");
        scores = GameObject.Find("score");
        replay = GameObject.Find("replay");
        replaybutton = replay.GetComponent<CircleCollider2D>();
    }

    public void ButtonSelection()
    {
        replaypanel.GetComponent<Animation>().Play("idle");
        //if (replay.GetComponent<Click>().OnPointerClick() == true)
        {

        }
    }
}
