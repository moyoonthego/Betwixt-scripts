using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class ReplayGame : MonoBehaviour {
    public AudioSource click;
    int onlyonce;
    public GameObject replaypanel;
    public GameObject replaybutton;
    public Animation replaybuttonanimation;
    public Animation replayanimation;
    int shouldI;

    void Start()
    {
        const string adGameID = "2690066";
        Advertisement.Initialize(adGameID);
        click = GameObject.Find("replay").GetComponent<AudioSource>();
        replaypanel = GameObject.Find("Replay Panel");
        replaybutton = GameObject.Find("replay");
        replayanimation = replaypanel.GetComponent<Animation>();
        replaybuttonanimation = replaybutton.GetComponent<Animation>();
        onlyonce = 0;
        shouldI = Random.Range(1, 4);
    }

    void OnMouseDown()
    {
        onlyonce = 1;
        UpdateScore.ResetScore();
        if ((Advertisement.IsReady() == true) && (shouldI == 3))
        {
            Advertisement.Show();
        }
    }

    void Update()
    {
        if ((onlyonce >= 60))
        {
            // end the process, reboot game
            SceneManager.LoadScene("GameScene");
        } else if (onlyonce > 1)
        {
            // Less than 60, increment onlyonce every frame by 1
            onlyonce += 1;
        }
        else if ((onlyonce == 1))
        {
            //replayanimation["replaycomesin"].speed = -1;
            //replayanimation["replaycomesin"].time = replayanimation["replaycomesin"].length;
            replaybuttonanimation.Play("rePulse");
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
