using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingBehaviour : MonoBehaviour {

    //Create vars for all our rings and sprites
    public GameObject ring1;
    public GameObject ring2;
    public GameObject ring3;
    public GameObject character;
    public GameObject startchar;
    public GameObject replaypanel;
    //Create var for starting animation
    private Animation start;
    //Create vars for ring animations
    private Animation closerings1;
    private Animation closerings2;
    private Animation closerings3;
    // We need a var for our first ring to start closing in
    public GameObject fallingring;
    private Animation closefallingring;
    //set up it's speed
    float animspeed = 0.5f;
    //Also need a random var
    int rando;
    //Get a var to check if it stopped closing in
    bool isitfalling = false;
    //int to check if replay button stopped popping out
    int onlyonce = 0;
    int wait4tap = 0;
    //Get a var for death
    bool earlydeath = false;
    bool latedeath = false;
    //Need to count the # of taps (initially none)
    int counter = 0;

    //AUDIO
    //Get all audio clips
    // Create audio sources
    public AudioSource ring1died;
    public AudioSource ring2died;
    public AudioSource ring3died;
    public AudioSource whodied;
    public AudioSource earlydied;
    public AudioSource latedied;

    // Use this for initialization
    void Awake () {
        //Define all our vars
        ring1 = GameObject.Find("Level 1 Ring");
        ring2 = GameObject.Find("Level 2 Ring");
        ring3 = GameObject.Find("Level 3 Ring");
        replaypanel = GameObject.Find("Replay Panel");
        character = GameObject.Find("Game Sprite");
        startchar = GameObject.Find("Starter Sprite");
        start = startchar.GetComponent<Animation>();
        closerings1 = ring1.GetComponent<Animation>();
        closerings2 = ring2.GetComponent<Animation>();
        closerings3 = ring3.GetComponent<Animation>();
        //Disable multiple finger inputs, it will only confuse the game
        Input.multiTouchEnabled = false;
        // Set up audio sources
        ring1died = ring1.GetComponent<AudioSource>();
        ring2died = ring2.GetComponent<AudioSource>();
        ring3died = ring3.GetComponent<AudioSource>();
        earlydied = GameObject.Find("Too Early Sprite").GetComponent<AudioSource>();
        latedied = GameObject.Find("Too Late Sprite").GetComponent<AudioSource>();
        start.Play("startanimation");
    }
	
	// Update is called once per frame
	void Update () {
        //if it is not still falling, it is time to choose another ring to fall and play the fall animation 
        if (isitfalling == false && (earlydeath == false && latedeath == false) && ((wait4tap == 0) || (wait4tap == 20)))  {
            //Get a new randomly generated number for our ring
            wait4tap = 0;
            rando = Random.Range(1, 4);
            if (closefallingring != null)
            {
                closefallingring.Stop("firstRingReset");
                closefallingring.Play("resetRingstart");
            }
            // Depending on if it is the 1st, 2nd or 3rd ring, assign the falling ring and animation accordingly
            if (rando == 1)  {
                fallingring = ring1;
                //Change audio too
                whodied = ring1died;
                closefallingring = closerings1;
            } else if (rando == 2) {
                fallingring = ring2;
                whodied = ring2died;
                closefallingring = closerings2;
            } else {
                fallingring = ring3;
                whodied = ring3died;
                closefallingring = closerings3;
            }
            //Play the falling animation for the respective closing ring
            closefallingring.Play("firstRingClosing");
            // Finally, set the falling var to be true
            isitfalling = true;
        }

        if (wait4tap >= 1)
        {
            wait4tap++;
        }

        if (Input.GetMouseButtonDown(0) && (earlydeath == false && latedeath == false)) {
            //Check to make sure that the size (position) of the current ring was in the required region.
            // First, make sure the circle is even on the screen (so they don't lose by tapping too happily)
            if (2.62 > fallingring.transform.localScale.x) {
                if ((1.69 > fallingring.transform.localScale.x) && (1.67 > fallingring.transform.localScale.y) && (wait4tap == 0)) {
                    //This if statement adds audio effect for each tap
                    //increment our counter
                    counter++;
                    if (counter == 1 && rando != 1) {
                        ring1died.Play();
                    } else if (counter == 2 && rando != 2) {
                        ring2died.Play();
                    }
                    // If the tap counter is same as ring number, they stopped this ring and its time for a new one
                    if (counter >= rando) {
                        isitfalling = false;
                        whodied.Play();
                        // Update the score text
                        GameObject.Find("score").GetComponent<UpdateScore>().ChangeScore();
                        closefallingring.Play("firstRingReset");
                        closefallingring["firstRingClosing"].speed += animspeed;
                        counter = 0;
                        wait4tap += 1;
                    }
                } else {
                    //If it wasn't we have died! Obvs, early death, since we are still checking taps 
                    earlydeath = true;
                    earlydied.Play();
                }
            }
        }

        if (0.8421551 > fallingring.transform.localScale.x)
        {
            latedeath = true;
            latedied.Play();
        }

        if ((earlydeath == true) | (latedeath == true)) {
            // End the game and boot up the end screen
            if (earlydeath == true)
            {
                closefallingring.Play("resetRingstart");
                GameObject.Find("Too Early Sprite").GetComponent<Animation>().Play("earlydeath");
            } else {
                closefallingring.Play("resetRingstart");
                GameObject.Find("Too Late Sprite").GetComponent<Animation>().Play("earlydeath");
            }
            // Once the death animation has ended, load the replay screen
            if((GameObject.Find("Too Early Sprite").GetComponent<Animation>().isPlaying == false) || (GameObject.Find("Too Late Sprite").GetComponent<Animation>().isPlaying == false))
            {
                if (onlyonce <= 30) {
                    replaypanel.GetComponent<Animation>().Play("replaycomesin");
                    onlyonce++;
                }
                //if (onlyonce > 59 )
                //{
                    //replaypanel.GetComponent<Animation>().Play("idle");
                    //replaypanel.GetComponent<EndBehaviour>().ButtonSelection();
                //}
            }
        }
        
		
	}
}
