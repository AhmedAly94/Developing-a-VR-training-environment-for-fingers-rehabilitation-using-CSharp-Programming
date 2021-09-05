using SG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DIP : MonoBehaviour
{
    //public GameObject mcp;
    public static float mcpJoint;
    public TextMesh text;

    //get the Score updated in Timer file and display it in the text in unity
    void Update()
    {
        //mcp.GetComponent<Text>().text = "MCP: " + mcpJoint;

        SG_Grabable.beforeIndexFlexions = SG_HandPose.getIndex;///////////////////////
        mcpJoint = SG_Grabable.beforeIndexFlexions[2];
        text.text = "DIP: " + mcpJoint;
    }

}
