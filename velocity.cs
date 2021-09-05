using SG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class velocity : MonoBehaviour
{
    public GameObject velocityy;
    public static float vel;

    //game object for the velocity
    void Update()
    {
        vel = SG_Grabable.thumbVelocity;
        velocityy.GetComponent<Text>().text = "Velocity: " + vel;
    }

}
