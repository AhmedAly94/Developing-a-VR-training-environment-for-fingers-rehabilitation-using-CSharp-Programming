using SG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Position: MonoBehaviour
{
    public GameObject distance;
    public static float dis;
    
    //get distance from Grabable class and display it in the text
    void Update()
    {
        dis = SG_Grabable.thumbAngle;
        distance.GetComponent<Text>().text = "Angle: " + dis;
    }
}
