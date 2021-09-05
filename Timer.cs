using SG;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HTC.UnityPlugin.Vive;

public class Timer : MonoBehaviour
{
    public TextMesh timertext;//3DText
    //public GameObject calibrationObject;//3DText for calibration 
    public GameObject levelWon;//3DText for achieving the level
    public GameObject levelFailed;//3DText for failing the level
    public GameObject alertNext;//3DText for alerting to touch the door
    private float startTime; //variable indicating the time of first interaction
    public static float beginSeconds;//capture seconds upon interaction
    public static float beginMinutes;//capture minutes upon interaction
    public static float endSeconds;//capture seconds when releasing the object
    public static float endMinutes;//capture minutes upon interaction
    public static float minutess;//capture minutes upon interaction
    public static bool finished = false;//flag to capture if the level is finished
    public float restartdelay = 30f;//delay before the next level
    public bool firstGrab = false;//flag for grabbing the object at the beginning
    public static float averageVelocity;//calculate averageVelocity
    public static float averageAcceleration;//calculate averageAcceleration
    public AudioSource ticksource;//sound effect
    
    // Start is called before the first frame update
    void Start()
    {
        //Thumb Initialization
        SG_Grabable.initialThumbFlexions = SG_HandPose.getThumb;
        SG_Grabable.CMCFlexionThumbInitial = SG_Grabable.initialThumbFlexions[0];
        SG_Grabable.MCPFlexionThumbInitial = SG_Grabable.initialThumbFlexions[1];
        SG_Grabable.IPFlexionThumbInitial = SG_Grabable.initialThumbFlexions[2];

        //Index Initialization
        SG_Grabable.initialIndexFlexions = SG_HandPose.getIndex;
        SG_Grabable.MCPFlexionIndexInitial = SG_Grabable.initialIndexFlexions[0];
        SG_Grabable.PIPFlexionIndexInitial = SG_Grabable.initialIndexFlexions[1];
        SG_Grabable.DIPFlexionIndexInitial = SG_Grabable.initialIndexFlexions[2];

        //Middle Initialization
        SG_Grabable.initialMiddleFlexions = SG_HandPose.getMiddle;
        SG_Grabable.MCPFlexionMiddleInitial = SG_Grabable.initialMiddleFlexions[0];
        SG_Grabable.PIPFlexionMiddleInitial = SG_Grabable.initialMiddleFlexions[1];
        SG_Grabable.DIPFlexionMiddleInitial = SG_Grabable.initialMiddleFlexions[2];

        //Ring Initialization
        SG_Grabable.initialRingFlexions = SG_HandPose.getRing;
        SG_Grabable.MCPFlexionRingInitial = SG_Grabable.initialRingFlexions[0];
        SG_Grabable.PIPFlexionRingInitial = SG_Grabable.initialRingFlexions[1];
        SG_Grabable.DIPFlexionRingInitial = SG_Grabable.initialRingFlexions[2];

        //Pinky Initialization
        SG_Grabable.initialPinkyFlexions = SG_HandPose.getPinky;
        SG_Grabable.MCPFlexionPinkyInitial = SG_Grabable.initialPinkyFlexions[0];
        SG_Grabable.PIPFlexionPinkyInitial = SG_Grabable.initialPinkyFlexions[1];
        SG_Grabable.DIPFlexionPinkyInitial = SG_Grabable.initialPinkyFlexions[2];

        finished = false;//didn't get the scores required yet
        SG_Grabable.Grabbed = false;//no Interaction occurs to begin timer
        ticksource = GetComponent<AudioSource>();//get mp3 file from unity
        //calibrationObject.SetActive(true);//pop up message disabled at the beginning
        levelWon.SetActive(false);//pop up message disabled at the beginning
        levelFailed.SetActive(false);//pop up message disabled at the beginning
        alertNext.SetActive(false);//pop up message disabled at the beginning
    }

    // Update is called once per frame
    void Update()
    {

        if (CountDownTimer.lose == true && !finished)//check flag in countDownTimer class if true therefore lost,should be displayed only when not won before
        {
            levelFailed.SetActive(true);//pop up message for failure
            timertext.color = Color.red;//alert the patient that he lost
        }

        if (SG_Grabable.Grabbed == true && finished != true) //if the patient interacted with the object
        {
            //calibrationObject.SetActive(false); //disable popup message of calibration
            //Invoke("getIntermediate", 1.02f); //get position and time at time step =1 after picking object

            if (firstGrab == false) //to start time only once upon interaction
            {
                startTime = Time.time;//start timer
                firstGrab = true;//flag to only start timer if false
            }

            float time = Time.time - startTime; //calculate time
            //Debug.Log(time);
            string minutes = ((int)time / 60).ToString();//convert minutes to string
            minutess = (int)time / 60;//get minutes
            Debug.Log("THE MINUTES IS " + minutess);
            beginSeconds = time % 60;//get seconds at beginning
            beginMinutes = minutess;
            endSeconds = time % 60;
            endMinutes = minutess;
            string seconds = (time % 60).ToString("f2");//convert seconds to string
            timertext.text = minutes + ":" + seconds;//display time in the 3D inspector

            if (SG_FingerFeedback.contact == true)//flag is true when being in contact with the object
            {
                SG_Grabable.beginSeconds = beginMinutes * 60 + beginSeconds; //capture when in contact
                Debug.Log("THE START INTERACTION TIME IS " + SG_Grabable.beginSeconds);
                SG_FingerFeedback.contact = false;
                SG_Grabable.beforeTotalFlexions = SG_HandPose.TotalFlexions; //capture total flexions of each finger before grasp
                SG_Grabable.beforeThumbFlexions = SG_HandPose.getThumb;//get joint angles of Thumb 
                SG_Grabable.beforeIndexFlexions = SG_HandPose.getIndex;//get joint angles of Index
                SG_Grabable.beforeMiddleFlexions = SG_HandPose.getMiddle;//get joint angles of Middle
                SG_Grabable.beforeRingFlexions = SG_HandPose.getRing;
                SG_Grabable.beforePinkyFlexions = SG_HandPose.getPinky;
            }

            if (SG_Grabable.grasp == true)//flag is true when grasping the object
            {
                SG_Grabable.endSeconds = endMinutes * 60 + endSeconds; //capture time when grasping the object
                endSeconds = endMinutes * 60 + endSeconds;
                Debug.Log("THE END INTERACTION TIME IS " + endSeconds);
                SG_Grabable.grasp = false;
                SG_Grabable.interactionSeconds = SG_Grabable.endSeconds - SG_Grabable.beginSeconds; //calculate the time difference
                SG_Grabable.interactionSeconds = Mathf.Round(SG_Grabable.interactionSeconds * 100.0f) * 0.01f; //round 2 decimal places

                if (SG_Grabable.interactionSeconds < 0) //only at the beginning
                {
                    SG_Grabable.interactionSeconds = SG_Grabable.interactionSeconds * -1;
                    SG_Grabable.interactionSeconds = Mathf.Round(SG_Grabable.interactionSeconds * 100000.0f) * 0.00001f;//round 5 decimal places
                }

                Debug.Log("The time in seconds is " + SG_Grabable.interactionSeconds.ToString("F5"));
                SG_Grabable.afterTotalFlexions = SG_HandPose.TotalFlexions;//capture total flexions of each finger after grasp
                SG_Grabable.afterThumbFlexions = SG_HandPose.getThumb;//capture flexion of thumb after grasping
                SG_Grabable.afterIndexFlexions = SG_HandPose.getIndex;//capture flexion of index after grasping
                SG_Grabable.afterMiddleFlexions = SG_HandPose.getMiddle;//capture flexion of index after grasping
                SG_Grabable.afterRingFlexions = SG_HandPose.getRing;//capture flexion of index after grasping
                SG_Grabable.afterPinkyFlexions = SG_HandPose.getPinky;
            }

        }

        //if the game is finished exit the function and close the timer
        if (finished == true)
        {
            Invoke(nameof(isLevelFinished), restartdelay);//delay before next scene

            timertext.color = Color.black;
            return;
        }
        else
        {
            isLevelFinished();//check if score is achieved every frame 
        }

    }

    public bool isLevelFinished()
    {
        if (ScoringSystem.theScore == 4)//if score is achieved
        {
            levelWon.SetActive(true);//pop up score message
            alertNext.SetActive(true);//pop up message to alert the patient to press cube for going to next level
            ticksource.Play();//sound effect
            averageVelocity = SG_Grabable.totalThumbAngle / SG_Grabable.totalInteractionSeconds;//calculate average velocity
            averageVelocity = Mathf.Round(averageVelocity * 100.0f) * 0.01f;//two decimal places
            Debug.Log(averageVelocity);
            averageAcceleration = SG_Grabable.totalthumbAcceleration / 4;//calculate average acceleration
            averageAcceleration = Mathf.Round(averageAcceleration * 100.0f) * 0.01f;//two decimal places
            Debug.Log(averageAcceleration);
            ScoringSystem.theScore = 0;//reset score
            finished = true;//set flag to true
            timertext.color = Color.yellow;//alert the patient that time is reached
            FindObjectOfType<GameManager>().CompleteLevel();//visual 2Deffect to alert the patient that level is completed

            return true;
        }

        return false;
    }

}
