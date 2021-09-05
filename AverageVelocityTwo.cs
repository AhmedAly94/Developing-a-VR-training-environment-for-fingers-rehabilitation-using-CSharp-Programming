using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SG;
public class AverageVelocityTwo : MonoBehaviour
{
    public GameObject AverageVelocityText;
    public static int theScore;
    public float restartdelay = 30f;
    public TextMesh text;

    //calculate average Velocity
    void Update()
    {
        AverageVelocityText.GetComponent<Text>().text = "AverageVelocity: " + TimerTwo.averageVelocity;
        text.text = "AverageVelocity: " + TimerTwo.averageVelocity;
    }

}
