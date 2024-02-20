using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics.Tracing;
using MoreMountains.CorgiEngine;

//LAST UPDATED: 2/19

public class startCutscene : MonoBehaviour
{
    //public TextMeshProUGUI textTimer;
    public TextMeshProUGUI breathePrompt;
    public GameObject fadeToBlack;
    //int timer = 3;
    public GameObject player;
    public Character character;

    public bool finishedExercise = false;
    public GameObject oMeter;
    bool pressing = false;
    Image meterFill;

    public GameObject cam;

    Vector3 spawnPos = new Vector3(-11f, 7f, 0);

    public Image oBar;

    // Start is called before the first frame update
    void Start()
    {//may wanna change the timing on this to after the initial cutscene plays
        //if we do, then all we have to do is just make a coroutine that simply WaitForSeconds([length of the cutscene]) and at the end of THAT we invoke coundown()
        //StartCoroutine(countdown());

        //see the if statement below about thoughts of player shenanigans because that really does impact what goes on up here in terms of 
        //what needs to be located in this
        meterFill = oMeter.GetComponent<Image>();
        meterFill.fillAmount = 0f;

        character = player.GetComponent<Character>();
        //textTimer.gameObject.SetActive(false);

        cam = GameObject.Find("CinemachineVirtualCamera");

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) {
            player = GameObject.FindWithTag("Player");
            //if (player == null)
                //Debug.Log("Still null");
        }

        if (Input.GetKey("space") && !finishedExercise) { 
            StartCoroutine(fillTheMeter());
        }
        //not sure what to do here. we could either: 
        //a) actively fill the player's O2 meter here
        //b) just have a bar that means literally nothing to anyone but the player and acts more as a timer
        //for the PT
        //the latter would probably be better, but no matter what we do we'll have to change the player to have the "canMove" bool or smth along those lines
        //so that way it can be disabled here. 
        if (Input.GetKey("tab")) {
            pressing = true;
        }
        else
            pressing = false;

    }

    /*
    public IEnumerator countdown() {
        //update the UI text to have the countdown text
        textTimer.text = timer.ToString() + "...";
        //wait 1 second
        yield return new WaitForSeconds(1f);
        timer--;
        //find out if it's time to start the exercise
        if (timer < 1)
        {//if it is time then just start it
            textTimer.text = "Go!";
            //StartCoroutine(fillTheMeter());
            yield return null;
        }
        else
        {
            //repeat the coroutine if it's not time yet
            //in case anyone needs explanations about coroutines, I (Jenna) don't mind explaining them because i do fear we'll have to use a bit of them
            //not too much though, hopefully. just depends on frame rate dependencies/if we want them or not. 
            StartCoroutine(countdown());
        }
    }
    */

    public IEnumerator fillTheMeter() {
        //find out if the button is being pressed and if it's not filled all the way
        if (pressing && meterFill.fillAmount < 1f) {
            //this is just an arbitrarily small amount
            //and since we don't know how long these exercises will last for, we can change this and fine-tune it later
            meterFill.fillAmount += 0.1f;
            yield return new WaitForSeconds(0.5f);
            //and if not repeat the process
            StartCoroutine(fillTheMeter());
        }
            else if (meterFill.fillAmount >= 1f){//this boolean determines whether or not to fade to black on the image
            yield return new WaitForSeconds(1f);
            StartCoroutine(meterDecay());
            finishedExercise = true;
            Debug.Log("Exercise has finished");
            breathePrompt.gameObject.SetActive(false);
            //Debug.Log(spawnPos);
            player.transform.position = spawnPos;
            //cam.transform.position = spawnPos;
            
            }
        
    }

    public IEnumerator meterDecay() {
        if (finishedExercise)
        {
            Debug.Log("Meter: " + meterFill.fillAmount);
            meterFill.fillAmount -= 0.001f;
            //again, arbitrary amount of  time
            yield return new WaitForSeconds(0.5f);
            //if the meter's not empty keep going. otherwise kill them
            if (meterFill.fillAmount > 0f)
            {
                StartCoroutine(meterDecay());
            }
            else if (meterFill.fillAmount <= 0f)
            {
                Destroy(player);
            }
        }
    }

}
