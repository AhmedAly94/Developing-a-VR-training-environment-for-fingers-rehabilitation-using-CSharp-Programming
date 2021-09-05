using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SG;
public class AverageVelocity: MonoBehaviour
{
    public GameObject AverageVelocityText;
    public static int theScore;
    public float restartdelay = 30f;
    public TextMesh text;

    //calculate Average Velocity
    void Update()
    {
        AverageVelocityText.GetComponent<Text>().text = "AverageVelocity: " +Timer.averageVelocity;
        text.text = "AverageVelocity: " + Timer.averageVelocity;
    }

}
