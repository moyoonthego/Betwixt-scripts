using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScores : MonoBehaviour
{
    public AudioSource click;
    int onlyonce;
    public GameObject replaypanel;
    public Animation replayanimation;
    int shouldI;
    public bool dontdothatagain = false;

    void Start()
    {
        click = GameObject.Find("replay").GetComponent<AudioSource>();
        replaypanel = GameObject.Find("Replay Panel");
        replayanimation = replaypanel.GetComponent<Animation>();
        onlyonce = 0;
        shouldI = Random.Range(1, 7);
    }

    void OnMouseDown()
    {
        onlyonce = 1;
    }

    void Update()
    {
        if ((onlyonce >= 60) && (dontdothatagain == false))
        {
            // end the process, reboot game
            GameObject.Find("High Score AddPanel").GetComponent<Animation>().Play("replaycomesin");
            dontdothatagain = true;
        }
        else if (onlyonce > 1)
        {
            // Less than 60, increment onlyonce every frame by 1
            onlyonce += 1;
        }
        else if ((onlyonce == 1))
        {
            replayanimation.Stop("replaycomesin");
            replayanimation.Play("replaygoesout");
            onlyonce += 1;
            click.Play();
        }
        else if ((replayanimation.IsPlaying("replaycomesin") == false) && (replayanimation.IsPlaying("replaygoesout") == false))
        {
            replayanimation.Play("idle");
        }

    }
}
