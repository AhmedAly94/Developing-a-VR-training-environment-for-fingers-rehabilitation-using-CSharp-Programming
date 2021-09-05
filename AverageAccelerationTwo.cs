using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SG;
public class AverageAccelerationTwo : MonoBehaviour
{
    public GameObject AverageAccelerationText;
    public static int theScore;
    public float restartdelay = 30f;
    public TextMesh text;

    //get average Acceleration
    void Update()
    {
        AverageAccelerationText.GetComponent<Text>().text = "AverageAcceleration: " + TimerTwo.averageAcceleration;
        text.text = "AverageAcceleration: " + TimerTwo.averageAcceleration;
    }

}
