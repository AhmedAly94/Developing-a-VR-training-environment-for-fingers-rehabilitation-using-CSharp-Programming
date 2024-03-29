﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{

    /// <summary> Data class containing all variables needed to draw or analyze a hand. Converted into Unity Coordinates from the internal SenseGlove coordinates. </summary>
    public class SG_HandPose
    {
        //----------------------------------------------------------------------------------------------
        // Member Variables

        /// <summary> The angles of each joint, in degrees, where flexing the finger creates a negative z-rotation. 
        /// The first index [0..4] determines the finger (thumb..pinky), while the second [0..2]  determines joint (CMC, MCP, IP for thumb. MCP, PIP, DIP for fingers.) </summary>
        public static Vector3[][] jointAngles;

        /// <summary> The quaternion rotation of each joint, relative to a Wrist Transform: JointRotation * WristRotation = 3D Rotation. 
        /// The first index [0..4] determines the finger (thumb..pinky), while the second [0..2]  determines joint (CMC, MCP, IP for thumb. MCP, PIP, DIP for fingers.) </summary>
        public Quaternion[][] jointRotations;

        /// <summary> The position of each joint, in meters, relative to a Wrist Transform: (JointPosition * WristRotation) + WristPosition = 3D Position. 
        /// The first index [0..4] determines the finger (thumb..pinky), while the second [0..2]  determines joint (CMC, MCP, IP for thumb. MCP, PIP, DIP for fingers.) </summary>
        public Vector3[][] jointPositions;

        /// <summary> The total flexion of each finger, normalized to values between 0 (fingers fully extended) and 1 (fingers fully flexed). 
        /// The index [0..4] determines the finger (thumb..pinky). </summary>
        public float[] normalizedFlexion;


        //----------------------------------------------------------------------------------------------
        // Construction

        /// <summary> Create a new SG_HandPose from an internal SenseGlove Pose. </summary>
        /// <param name="handPose"></param>
        public SG_HandPose(SGCore.HandPose handPose)
        {
            jointAngles = Util.SG_Conversions.ToUnityEulers(handPose.handAngles);
            jointRotations = Util.SG_Conversions.ToUnityQuaternions(handPose.jointRotations);
            jointPositions = Util.SG_Conversions.ToUnityPositions(handPose.jointPositions, true);
            normalizedFlexion = handPose.GetNormalizedFlexion();
        }

        /// <summary> Manually create a new SG_HandPose. </summary>
        /// <param name="handAngles"></param>
        /// <param name="handRotations"></param>
        /// <param name="handPositions"></param>
        /// <param name="normalFlex"></param>
        public SG_HandPose(Vector3[][] handAngles, Quaternion[][] handRotations, Vector3[][] handPositions, float[] normalFlex = null)
        {
            jointAngles = handAngles;
            jointRotations = handRotations;
            jointPositions = handPositions;
            normalizedFlexion = normalFlex == null ? new float[5] : normalFlex;
        }


        /// <summary> Generates an 'idle' handPose for a left or right hand, where the fingers are fully extended, and the thumb is slightly abducted. </summary>
        /// <param name="right"></param>
        /// <returns></returns>
        public static SG_HandPose Idle(bool right)
        {
            return new SG_HandPose(SGCore.HandPose.DefaultIdle(right));
        }




        /// <summary> Mirrors this hand pose around the z-axis, to that of the opposite hand </summary>
        /// <returns></returns>
        public SG_HandPose Mirror() //todo: make this part SGCore.HandPose as well.
        {
            Quaternion[][] mirrorJoints = new Quaternion[this.jointRotations.Length][];
            for (int f = 0; f < jointRotations.Length; f++)
            {
                mirrorJoints[f] = new Quaternion[jointRotations[f].Length];
                for (int j = 0; j < jointRotations[f].Length; j++)
                {
                    mirrorJoints[f][j] = SG.Util.SG_Conversions.MirrorZ(jointRotations[f][j]);
                }
            }
            Vector3[][] mirrorPos = new Vector3[this.jointPositions.Length][];
            for (int f = 0; f < jointPositions.Length; f++)
            {
                mirrorPos[f] = new Vector3[jointPositions[f].Length];
                for (int j = 0; j < jointPositions[f].Length; j++)
                {
                    mirrorPos[f][j] = new Vector3(jointPositions[f][j].x, jointPositions[f][j].y, -jointPositions[f][j].z);
                }
            }
            Vector3[][] mirrorAngles = new Vector3[jointAngles.Length][];
            for (int f = 0; f < jointAngles.Length; f++)
            {
                mirrorAngles[f] = new Vector3[jointAngles[f].Length];
                for (int j = 0; j < jointAngles[f].Length; j++)
                {
                    mirrorAngles[f][j] = new Vector3(-jointAngles[f][j].x, -jointAngles[f][j].y, jointAngles[f][j].z);
                }
            }
            return new SG_HandPose(mirrorAngles, mirrorJoints, mirrorPos);
        }


        //----------------------------------------------------------------------------------------------
        // Utility Functions

        /// <summary> Returns the total flexion of each finger in degrees. In Unity, flexion movements create a negative value. The index [0..4] determines the finger (thumb..pinky). </summary>
        public static float[] TotalFlexions
        {
            get
            {
                float[] res = new float[jointAngles.Length]; 
                for (int f=0; f<jointAngles.Length; f++) //array of 5 elements
                {
                    for (int j = 0; j < jointAngles[f].Length; j++) //array of 3 elements
                    {
                        res[f] += jointAngles[f][j].z; 
                    }
                }
                return res;
            }
        }

        public static float[] getThumb
        {
            get
            {
                float[] res = new float[jointAngles.Length];
                int f = 0;
                    for (int j = 0; j < jointAngles[f].Length; j++)
                    {
                        res[j] = jointAngles[0][j].z;
                    }
                
                return res;
            }
        }

        public static float[] getIndex
        {
            get
            {
                float[] res = new float[jointAngles.Length];
                int f = 1;
                for (int j = 0; j < jointAngles[f].Length; j++)
                {
                    res[j] = jointAngles[1][j].z;
                }

                return res;
            }
        }

        public static float[] getMiddle
        {
            get
            {
                float[] res = new float[jointAngles.Length];
                int f = 2;
                for (int j = 0; j < jointAngles[f].Length; j++)
                {
                    res[j] = jointAngles[2][j].z;
                }

                return res;
            }
        }

        public static float[] getRing
        {
            get
            {
                float[] res = new float[jointAngles.Length];
                int f = 3;
                for (int j = 0; j < jointAngles[f].Length; j++)
                {
                    res[j] = jointAngles[3][j].z;
                }

                return res;
            }
        }

        public static float[] getPinky
        {
            get
            {
                float[] res = new float[jointAngles.Length];
                int f = 4;
                for (int j = 0; j < jointAngles[f].Length; j++)
                {
                    res[j] = jointAngles[4][j].z;
                }

                return res;
            }
        }
    }

}