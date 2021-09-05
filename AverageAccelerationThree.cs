using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SG;
public class AverageAccelerationThree : MonoBehaviour
{
    public GameObject AverageAccelerationText;
    public static int theScore;
    public float restartdelay = 30f;
    public TextMesh text;

    //get average acceleration
    void Update()
    {
        AverageAccelerationText.GetComponent<Text>().text = "AverageAcceleration: " + TimerThree.averageAcceleration;
        text.text = "AverageAcceleration: " + TimerThree.averageAcceleration;
    }

}
