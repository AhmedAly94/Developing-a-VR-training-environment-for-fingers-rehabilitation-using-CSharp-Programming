using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SG;
public class AverageVelocityThree : MonoBehaviour
{
    public GameObject AverageVelocityText;
    public static int theScore;
    public float restartdelay = 30f;
    public TextMesh text;

    //calculate AverageVelocity
    void Update()
    {
        AverageVelocityText.GetComponent<Text>().text = "AverageVelocity: " + TimerThree.averageVelocity;
        text.text = "AverageVelocity: " + TimerThree.averageVelocity;
    }

}
