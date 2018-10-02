using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public AudioSource click;
    public Animation pulser;
    int onlyonce = 0;

    void Start()
    {
        pulser = GameObject.Find("play").GetComponent<Animation>();
        click = GameObject.Find("play").GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        click.Play();
        pulser.Play("Pulse");
        onlyonce = 1;
    }

    void Update()
    {
        if (onlyonce >= 1)
        {
            onlyonce += 1;
        }
        if (onlyonce > 35)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
