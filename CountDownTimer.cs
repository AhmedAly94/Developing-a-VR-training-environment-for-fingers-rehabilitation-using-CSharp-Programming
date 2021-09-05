using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using SG;

public class CountDownTimer : MonoBehaviour
{
    public float timeLeft = 10.0f;
    public Text startText; // used for showing countdown
    public static bool lose = false;
    public AudioSource ticksource;

    void Start()
    {
        lose = false;//flag not lost yet
        ticksource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (SG_Grabable.Grabbed == true)
        {

            if (timeLeft > 0.0f)
            {
                timeLeft -= Time.deltaTime; //decrease time every frame
            }
            else
            {
                lose = true; //flag to indicate timer is 0 and now lost
            }
        }

        startText.text = (timeLeft).ToString("0");
        if(lose == true)
        {
            ticksource.Play(); 
        }

    }
  }
