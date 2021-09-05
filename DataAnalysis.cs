using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SG;

public class DataAnalysis : MonoBehaviour
{
    public static string fileName = "";
    public static bool writeOnce = false;
    [System.Serializable]

    public class Patient
    {
        public float distance; //define distance
        public float velocity; //define velocity
        public float acceleration;//define acceleration
    }
    [System.Serializable]

    public class PatientList
    {
        public Patient[] patient;
    }

    public static PatientList myPatientList = new PatientList();

    // Start is called before the first frame update
    void Start()
    {
        fileName = Application.dataPath + "/DataAnalysis.csv"; //create excel sheet to write 
        //fileName = Application.dataPath + "/DataAnalysis.dat"; 
    }

    // Update is called once per frame
    void Update()
    {
        if (writeOnce == false) //write first row of definitions only once
        {
            writeCsv();
            writeOnce = true;
        }

        if (SG_Grabable.canWrite) //flag after placing the object in its correct place 
        {
            TextWriter every = new StreamWriter(fileName, true); //create object every time for new line
            every.WriteLine(SG_Grabable.interactionSeconds + "," + SG_Grabable.thumbAngle + "," + SG_Grabable.thumbVelocity + "," + SG_Grabable.thumbAcceleration 
                + "," + SG_Grabable.indexAngle + "," + SG_Grabable.indexVelocity + "," + SG_Grabable.indexAcceleration 
                + "," + SG_Grabable.middleAngle + "," + SG_Grabable.middleVelocity + "," + SG_Grabable.middleAcceleration 
                + "," + SG_Grabable.ringAngle + "," + SG_Grabable.ringVelocity + "," + SG_Grabable.ringAcceleration 
                + "," + SG_Grabable.pinkyAngle + "," + SG_Grabable.pinkyVelocity + "," + SG_Grabable.pinkyAcceleration
                + "," + SG_Grabable.initialCMCthumbAngle + "," + SG_Grabable.CMCthumbVelocity + "," + SG_Grabable.CMCthumbAcceleration
                + "," + SG_Grabable.initialMCPthumbAngle + "," + SG_Grabable.MCPthumbVelocity + "," + SG_Grabable.MCPthumbAcceleration
                + "," + SG_Grabable.initialIPthumbAngle + "," + SG_Grabable.IPthumbVelocity + "," + SG_Grabable.IPthumbAcceleration
                + "," + SG_Grabable.initialMCPindexAngle + "," + SG_Grabable.MCPindexVelocity + "," + SG_Grabable.MCPindexAcceleration
                + "," + SG_Grabable.initialPIPindexAngle + "," + SG_Grabable.PIPindexVelocity + "," + SG_Grabable.PIPindexAcceleration 
                + "," + SG_Grabable.initialDIPindexAngle + "," + SG_Grabable.DIPindexVelocity + "," + SG_Grabable.DIPindexAcceleration
                + "," + SG_Grabable.initialMCPmiddleAngle + "," + SG_Grabable.MCPmiddleVelocity + "," + SG_Grabable.MCPmiddleAcceleration
                + "," + SG_Grabable.initialPIPmiddleAngle + "," + SG_Grabable.PIPmiddleVelocity + "," + SG_Grabable.PIPmiddleAcceleration
                + "," + SG_Grabable.initialDIPmiddleAngle + "," + SG_Grabable.DIPmiddleVelocity + "," + SG_Grabable.DIPmiddleAcceleration
                + "," + SG_Grabable.initialMCPRingAngle + "," + SG_Grabable.MCPringVelocity + "," + SG_Grabable.MCPringAcceleration
                + "," + SG_Grabable.initialPIPRingAngle + "," + SG_Grabable.PIPringVelocity + "," + SG_Grabable.PIPringAcceleration
                + "," + SG_Grabable.initialDIPRingAngle + "," + SG_Grabable.DIPringVelocity + "," + SG_Grabable.DIPringAcceleration
                + "," + SG_Grabable.initialMCPPinkyAngle + "," + SG_Grabable.MCPpinkyVelocity + "," + SG_Grabable.MCPpinkyAcceleration
                + "," + SG_Grabable.initialPIPPinkyAngle + "," + SG_Grabable.PIPpinkyVelocity + "," + SG_Grabable.PIPpinkyAcceleration
                + "," + SG_Grabable.initialDIPPinkyAngle + "," + SG_Grabable.DIPpinkyVelocity + "," + SG_Grabable.DIPpinkyAcceleration);
            // + "," + SG_Grabable.CMCFlexionThumbInitial + "," + SG_Grabable.MCPFlexionThumbInitial + "," + SG_Grabable.IPFlexionThumbInitial
            // + "," + SG_Grabable.MCPFlexionIndexInitial + "," + SG_Grabable.PIPFlexionIndexInitial + "," + SG_Grabable.DIPFlexionIndexInitial
            // + "," + SG_Grabable.MCPFlexionMiddleInitial + "," + SG_Grabable.PIPFlexionMiddleInitial + "," + SG_Grabable.DIPFlexionMiddleInitial
            // + "," + SG_Grabable.MCPFlexionRingInitial + "," + SG_Grabable.PIPFlexionRingInitial + "," + SG_Grabable.DIPFlexionRingInitial
            // + "," + SG_Grabable.MCPFlexionPinkyInitial + "," + SG_Grabable.PIPFlexionPinkyInitial + "," + SG_Grabable.DIPFlexionPinkyInitial); //write to the next row
            every.Close();
            SG_Grabable.canWrite = false;//to avoid duplicates

        }

    }

    //function to write the first row once of distance,velocity,acceleration
    public static void writeCsv()
    {
        TextWriter tw = new StreamWriter(fileName, false);
        tw.WriteLine("Time,Thumb Angles,Thumb Velocity,Thumb Acceleration," +
            "Index Angles,Index Velocity,Index Acceleration," +
            "Middle Angles,Middle Velocity,Middle Acceleration,Ring Angles,Ring Velocity,Ring Acceleration," +
            "Pinky Angles,Pinky Velocity,Pinky Acceleration," +
            "CMC Thumb Angle,CMC Thumb Velocity,CMC Thumb Acceleration," +
            "MCP Thumb Angle,MCP Thumb Velocity,MCP Thumb Acceleration," +
            "IP Thumb Angle,IP Thumb Velocity,IPThumb Acceleration," +
            "MCP Index Angle,MCP Index Velocity,MCP Index Acceleration," +
            "PIP index Angle,PIP Index Velocity,PIP index Acceleration," +
            "DIP index Angle,DIP Index Velocity,DIP index acceleration," +
            "Middle MCP angle,Middle MCP Velocity,Middle MCP Acceleration," +
            "Middle PIP angle,Middle PIP Velocity,Middle PIP Acceleration," +
            "Middle DIP angle,Middle DIP Velocity,Middle DIP Acceleration," +
            "Ring MCP angle,Ring MCP Velocity,Ring MCP Acceleration," +
            "Ring PIP angle,Ring PIP Velocity,Ring PIP Acceleration," +
            "Ring DIP angle,Ring DIP Velocity,Ring DIP Acceleration," +
            "Pinky MCP angle,Pinky MCP Velocity,Pinky MCP Acceleration," +
            "Pinky PIP angle,Pinky PIP Velocity,Pinky PIP Acceleration, " +
            "Pinky DIP angle,Pinky DIP Velocity,Pinky DIP Acceleration");
            //"Thumb CMC angle,Thumb MCP initial,Thumb IP initial," +
            //"Index MCP initial,Index PIP initial,Index DIP initial," +
            //"Middle MCP initial,Middle PIP initial,Middle DIP initial," +
            //"Ring MCP initial,Ring PIP initial,Ring DIP initial," +
            //"Pinky MCP initial,Pinky PIP initial,Pinky DIP initial");
        tw.Close();

    }

}
