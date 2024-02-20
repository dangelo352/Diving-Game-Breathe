using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//LAST UPDATED: 2/17

public class fadeToBlack : MonoBehaviour
{
    string screenTag;
    float currentTransparency = 0f;
    float transparencyRate;
    public GameObject manager;
    public GameObject player;
    bool startAnim = false;

    // Start is called before the first frame update
    void Awake()
    {
        //find out how long the fade animation should take via the tag
        //we just have to make sure that this is updated on the object itself ("blackScreen") and NOT the canvas it's a child of
        screenTag = gameObject.tag;
    }

    private void Start()
    {
        //since we've discovered what the tag is on Awake() (I only did on Awake in case we have some SetActive shenanigans that may happen earlier/later down the line
        //if we don't need it i can move that logic here, technically
        //otherwise i'll just look extra it's fine
        switch (screenTag) {//all this will do is find how quickly we want to transition the fade to black. we'll actually 
            case "startCutscene":
                transparencyRate = 0.05f;
                break;
            default:
                break;
        }
        startAnim = true;
        StartCoroutine(fadeAnim());
    }

    // Update is called once per frame
    void Update()
    {
        //not sure if this is the best way to do it, but basically just see if the exercise has ended
        //if (manager.GetComponent<startCutscene>().finishedExercise) {
        //startAnim = true;
        //}
    }

    public IEnumerator fadeAnim()
    {
        Debug.Log(transparencyRate);
        currentTransparency += transparencyRate;
        if(currentTransparency < 0f) { currentTransparency = 0; }
        else if (currentTransparency > 1f) { currentTransparency = 1f; }
        this.GetComponent<Image>().color = new Color(0f, 0f, 0f, currentTransparency);
        //right now, this is an arbitrarily low number so that way it looks smooth. we can poke around at this if we want though/make it a public variable so it's easier to test.
        //up to everyone though, IDC
        yield return new WaitForSeconds(0.1f);
        //if it's in the middle of transitioning then keep going
        if (currentTransparency > 0f && currentTransparency < 1f && startAnim)
        {
            StartCoroutine(fadeAnim());
        }
        else {
            startAnim = false;
            transparencyRate *= -1;
            StartCoroutine(levelTransition());
        }
        yield return null;
    }

    public IEnumerator levelTransition() {
        yield return new WaitForSeconds(1f);
        StartCoroutine(fadeAnim());
    }
}
