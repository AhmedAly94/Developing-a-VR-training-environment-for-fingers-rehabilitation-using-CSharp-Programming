using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SG;
public class AverageAcceleration : MonoBehaviour
{
    public GameObject AverageAccelerationText;
    public static int theScore;
    public float restartdelay = 30f;
    public TextMesh text;

    //get average Acceleration
    void Update()
    {
        AverageAccelerationText.GetComponent<Text>().text = "AverageAcceleration: " + Timer.averageAcceleration;
        text.text= "AverageAcceleration: " + Timer.averageAcceleration;
    }

}
