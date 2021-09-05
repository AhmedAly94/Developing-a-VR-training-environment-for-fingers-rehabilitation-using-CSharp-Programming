using SG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class acceleration : MonoBehaviour
{
    public GameObject acceler;
    public static float accel;

    //game object component for acceleration
    void Update()
    {
        accel = SG_Grabable.thumbAcceleration;
        acceler.GetComponent<Text>().text = "Acceleration: " + accel;
    }

}
