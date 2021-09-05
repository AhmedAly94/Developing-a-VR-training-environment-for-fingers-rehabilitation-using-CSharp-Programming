using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touched : MonoBehaviour
{
    public static bool contact = false;
    //public static bool once = false;
    
    private void OnTriggerEnter(Collider other)
    {
        //if (once == false)
       // {
            contact = true;
            
       // once = true;
        //}

    }
}
