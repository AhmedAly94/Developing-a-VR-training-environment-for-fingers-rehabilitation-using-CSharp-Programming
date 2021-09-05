using HTC.UnityPlugin.Vive;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace SG
{

    /// <summary> The way in which this Grabscript picks up SG_Interactable objects. </summary>
    public enum GrabType
    {
        /// <summary> The grabbed object's transform follows that of the GrabReference through world coordinates. Does not interfere with VRTK scripts. </summary>
        Follow = 0,
        /// <summary> A FixedJoint is created between the grabbed object and the GrabReference, which stops it from passing through rigidbodies. </summary>
        FixedJoint,
        /// <summary> The object becomes a child of the Grabreference. Its original parent is restored upon release. </summary>
        Parent
    }

    /// <summary> The way that this SG_Grabable attaches to a GrabScript that tries to pick it up. </summary>
    public enum AttachType
    {
        /// <summary> Default. The object keeps its current position. </summary>
        Default = 0,
        /// <summary> The object snaps to the Grabscript in a predefined position and orientation; useful for tools etc. </summary>
        SnapToAnchor,
    }

    /// <summary> An object that can be picked up and dropped by the SenseGlove. </summary>
    public class SG_Grabable : SG_Interactable
    {

        static Vector3 spawnPos = new Vector3(0.0f, 0.0f, 0.0f);
        static float radius = 0.75f;

        public static bool Grabbed = false;//flag to indicate start of interaction
        public static bool grasp = false;//flag to indicate end of interaction
        public static float totalFlexionThumbBefore;//total flexion of digits of the thumb before grasp
        public static float totalFlexionThumbAfter;//total flexion of digits of the thumb after grasp

        /// <summary>
        /// ///////////
        /// </summary>
        /// 
        //Initial flexion of each digit of the thumb
        public static float CMCFlexionThumbInitial;//Initial flexion of CMC digit of the thumb before grasp  
        public static float MCPFlexionThumbInitial;//Initial flexion of MCP digit of the thumb before grasp
        public static float IPFlexionThumbInitial;//Initial flexion of IPdigit of the thumb before grasp

        public static float CMCFlexionThumbBefore;//flexion of CMC digit of the thumb before grasp
        public static float CMCFlexionThumbAfter;//flexion of CMC digit of the thumb after grasp
        public static float MCPFlexionThumbBefore;//flexion of MCP digit of the thumb before grasp
        public static float MCPFlexionThumbAfter;//flexion of MCP digit of the thumb after grasp
        public static float IPFlexionThumbBefore;//flexion of IPdigit of the thumb before grasp
        public static float IPFlexionThumbAfter;//flexion of IP digit of the thumb after grasp

        //Initial flexion of each digit of the index
        public static float MCPFlexionIndexInitial;//Initial flexion of CMC digit of the index before grasp  
        public static float PIPFlexionIndexInitial;//Initial flexion of MCP digit of the index before grasp
        public static float DIPFlexionIndexInitial;//Initial flexion of IPdigit of the index before grasp

        public static float MCPFlexionIndexBefore;//flexion of MCP digit of the thumb before grasp
        public static float MCPFlexionIndexAfter;//flexion of MCP digit of the thumb after grasp
        public static float PIPFlexionIndexBefore;//flexion of PIP digit of the thumb before grasp
        public static float PIPFlexionIndexAfter;//flexion of PIP digit of the thumb after grasp
        public static float DIPFlexionIndexBefore;//flexion of DIP digit of the thumb before grasp
        public static float DIPFlexionIndexAfter;//flexion of DIP digit of the thumb after grasp

        //Initial flexion of each digit of the Middle
        public static float MCPFlexionMiddleInitial;
        public static float PIPFlexionMiddleInitial;
        public static float DIPFlexionMiddleInitial;

        public static float MCPFlexionMiddleBefore;//flexion of MCP digit of the thumb before grasp
        public static float MCPFlexionMiddleAfter;//flexion of MCP digit of the thumb after grasp
        public static float PIPFlexionMiddleBefore;//flexion of PIP digit of the thumb before grasp
        public static float PIPFlexionMiddleAfter;//flexion of PIP digit of the thumb after grasp
        public static float DIPFlexionMiddleBefore;//flexion of DIP digit of the thumb before grasp
        public static float DIPFlexionMiddleAfter;//flexion of DIP digit of the thumb after grasp

        //Initial flexion of each digit of the Ring
        public static float MCPFlexionRingInitial;
        public static float PIPFlexionRingInitial;
        public static float DIPFlexionRingInitial;

        public static float MCPFlexionRingBefore;//flexion of MCP digit of the thumb before grasp
        public static float MCPFlexionRingAfter;//flexion of MCP digit of the thumb after grasp
        public static float PIPFlexionRingBefore;//flexion of PIP digit of the thumb before grasp
        public static float PIPFlexionRingAfter;//flexion of PIP digit of the thumb after grasp
        public static float DIPFlexionRingBefore;//flexion of DIP digit of the thumb before grasp
        public static float DIPFlexionRingAfter;//flexion of DIP digit of the thumb after grasp

        //Initial flexion of each digit of the pinky
        public static float MCPFlexionPinkyInitial;
        public static float PIPFlexionPinkyInitial;
        public static float DIPFlexionPinkyInitial;

        public static float MCPFlexionPinkyBefore;//flexion of MCP digit of the thumb before grasp
        public static float MCPFlexionPinkyAfter;//flexion of MCP digit of the thumb after grasp
        public static float PIPFlexionPinkyBefore;//flexion of PIP digit of the thumb before grasp
        public static float PIPFlexionPinkyAfter;//flexion of PIP digit of the thumb after grasp
        public static float DIPFlexionPinkyBefore;//flexion of DIP digit of the thumb before grasp
        public static float DIPFlexionPinkyAfter;//flexion of DIP digit of the thumb after grasp

        public static float totalFlexionIndexBefore;//total flexion of index before grasping
        public static float totalFlexionIndexAfter;//total flexion of index after grasping
        public static float totalFlexionMiddleBefore;//total flexion of middle before grasping
        public static float totalFlexionMiddleAfter;//total flexion of middle after grasping
        public static float totalFlexionRingBefore;//total flexion of ring before grasping
        public static float totalFlexionRingAfter;//total flexion of ring after grasping
        public static float totalFlexionPinkyBefore;//total flexion of pinky before grasping
        public static float totalFlexionPinkyAfter;//total flexion of pinky after grasping

        /// <summary>
        /// /////////// Calculate velocity and acceleration for each joint of each finger
        /// </summary>
        /// 
        //Each joint of thumb Velocity calculation
        public static float CMCthumbVelocity;
        public static float MCPthumbVelocity;
        public static float IPthumbVelocity;

        //Each joint of thumb acceleration calculation
        public static float CMCthumbAcceleration;
        public static float MCPthumbAcceleration;
        public static float IPthumbAcceleration;

        //Each joint of index Velocity calculation
        public static float MCPindexVelocity;
        public static float PIPindexVelocity;
        public static float DIPindexVelocity;

        //Each joint of index acceleration calculation
        public static float MCPindexAcceleration;
        public static float PIPindexAcceleration;
        public static float DIPindexAcceleration;

        //Each joint of middle Velocity calculation
        public static float MCPmiddleVelocity;
        public static float PIPmiddleVelocity;
        public static float DIPmiddleVelocity;

        //Each joint of middle acceleration calculation
        public static float MCPmiddleAcceleration;
        public static float PIPmiddleAcceleration;
        public static float DIPmiddleAcceleration;

        //Each joint of ring Velocity calculation
        public static float MCPringVelocity;
        public static float PIPringVelocity;
        public static float DIPringVelocity;

        //Each joint of ring acceleration calculation
        public static float MCPringAcceleration;
        public static float PIPringAcceleration;
        public static float DIPringAcceleration;

        //Each joint of pinky Velocity calculation
        public static float MCPpinkyVelocity;
        public static float PIPpinkyVelocity;
        public static float DIPpinkyVelocity;

        //Each joint of pinky acceleration calculation
        public static float MCPpinkyAcceleration;
        public static float PIPpinkyAcceleration;
        public static float DIPpinkyAcceleration;

        /// <summary>
        /// ///////////Calculate velocity and acceleration for all fingers 
        /// </summary>
        /// 
        //Thumb Velocity and acceleration calculation
        public static float thumbVelocity;//thumb Velocity calculation
        public static float thumbAcceleration;//thumb Acceleration calculation
        public static float totalthumbAcceleration;//total thumb Acceleration calculation

        //index Velocity and acceleration calculation
        public static float indexVelocity;//index Velocity calculation
        public static float indexAcceleration;//index Acceleration calculation
        public static float totalindexAcceleration;//total index Acceleration calculationv

        //Middle Velocity and acceleration calculation
        public static float middleVelocity;//middle Velocity calculation
        public static float middleAcceleration;//middle Acceleration calculation
        public static float totalmiddleAcceleration;//total middle Acceleration calculation

        //Ring Velocity and acceleration calculation
        public static float ringVelocity;//ring Velocity calculation
        public static float ringAcceleration;//ring Acceleration calculation
        public static float totalRingAcceleration;//total ringA cceleration calculation

        //Pinky Velocity and acceleration calculation
        public static float pinkyVelocity;//pinky Velocity calculation
        public static float pinkyAcceleration;//pinky Acceleration calculation
        public static float totalpinkyAcceleration;//total pinky Acceleration calculation

        /// <summary>
        /// ///////////
        /// </summary>
        public static float beginSeconds;//capture seconds upon interaction
        public static float endSeconds;//capture seconds upon releasing the object
        public static float endMinutes;//capture minutes upon releasing the object
        public static float interactionSeconds = 0; //timeSpan of interaction
        public static float totalInteractionSeconds;//total time of interaction

        /// <summary>
        /// ///////////For each joint of each finger 
        /// </summary>
        /// 
        /////Thumb Angles
        public static float initialCMCthumbAngle; // initial CMC Thumb Angle 
        public static float initialMCPthumbAngle; //initial MCP Thumb Angle 
        public static float initialIPthumbAngle; //initial IP Thumb Angle 

        public static float CMCthumbAngle; //CMC Thumb Angle 
        public static float MCPthumbAngle; //MCP Thumb Angle 
        public static float IPthumbAngle; //MCP Thumb Angle 

        ///// Index Angles
        public static float initialMCPindexAngle; // initial MCP index Angle 
        public static float initialPIPindexAngle; //initial PIP index Angle 
        public static float initialDIPindexAngle; //initial DIP index Angle 

        public static float MCPIndexAngle; //MCP IndexAngle 
        public static float PIPIndexAngle; //PIP Index Angle 
        public static float DIPIndexAngle; //DIP Index Angle 

        ///// Middle Angles
        public static float initialMCPmiddleAngle; // initial MCP index Angle 
        public static float initialPIPmiddleAngle; //initial PIP index Angle 
        public static float initialDIPmiddleAngle; //initial DIP index Angle 

        public static float MCPmiddleAngle; //MCP IndexAngle 
        public static float PIPmiddleAngle; //PIP Index Angle 
        public static float DIPmiddleAngle; //DIP Index Angle

        ///// Ring Angles
        public static float initialMCPRingAngle; // initial MCP index Angle 
        public static float initialPIPRingAngle; //initial PIP index Angle 
        public static float initialDIPRingAngle; //initial DIP index Angle 

        public static float MCPRingAngle; //MCP IndexAngle 
        public static float PIPRingAngle; //PIP Index Angle 
        public static float DIPRingAngle; //DIP Index Angle

        ///// Pinky Angles
        public static float initialMCPPinkyAngle; // initial MCP index Angle 
        public static float initialPIPPinkyAngle; //initial PIP index Angle 
        public static float initialDIPPinkyAngle; //initial DIP index Angle 

        public static float MCPPinkyAngle; //MCP IndexAngle 
        public static float PIPPinkyAngle; //PIP Index Angle 
        public static float DIPPinkyAngle; //DIP Index Angle

        /// <summary>
        /// ///////////For each finger
        /// </summary>
        public static float thumbAngle; //Thumb Angle 
        public static float indexAngle; //index Angle 
        public static float middleAngle; //middle Angle 
        public static float ringAngle; //ring Angle 
        public static float pinkyAngle; //pinky Angle 

        public static float totalThumbAngle;//total thumb angle moved
        public static float totalIndexAngle;//total Index angle moved
        public static float totalMiddleAngle;//total Middle angle moved
        public static float totalRingAngle;//total Ring angle moved
        public static float totalpinkyAngle;//total pinky angle moved

        public static bool canWrite = false;//flag to write only when placed the object in the target
        public AudioSource[] sounds;//array of sounds effect
        public AudioSource noise1;//sound when succeed in puting in the target
        public AudioSource noise2;//sound when failed in puting in the target

        public static float[] beforeTotalFlexions;//get total flexions of hand before grasping from SG_HandPose.TotalFlexions
        public static float[] afterTotalFlexions;//get total flexions of hand after grasping from SG_HandPose.TotalFlexions

        //Thumb
        public static float[] initialThumbFlexions;//get initial flexion of thumb before grasping from SG_HandPose.TotalFlexions
        public static float[] beforeThumbFlexions;//get flexions of all digits of thumb before grasping from SG_HandPose.TotalFlexions
        public static float[] afterThumbFlexions;//get flexions of all digits of thumb after grasping from SG_HandPose.TotalFlexions
        //Index
        public static float[] initialIndexFlexions;//get initial flexion of thumb before grasping from SG_HandPose.TotalFlexions
        public static float[] beforeIndexFlexions;//get flexions of all digits of thumb before grasping from SG_HandPose.TotalFlexions
        public static float[] afterIndexFlexions;//get flexions of all digits of thumb after grasping from SG_HandPose.TotalFlexions
        //Middle
        public static float[] initialMiddleFlexions;
        public static float[] beforeMiddleFlexions;//get flexions of all digits of Middle before grasping from SG_HandPose.TotalFlexions
        public static float[] afterMiddleFlexions;//get flexions of all digits of Middle after grasping from SG_HandPose.TotalFlexions
        //Ring
        public static float[] initialRingFlexions;
        public static float[] beforeRingFlexions;//get flexions of all digits of Middle before grasping from SG_HandPose.TotalFlexions
        public static float[] afterRingFlexions;//get flexions of all digits of Middle after grasping from SG_HandPose.TotalFlexions
        //Pinky
        public static float[] initialPinkyFlexions;
        public static float[] beforePinkyFlexions;//get flexions of all digits of Middle before grasping from SG_HandPose.TotalFlexions
        public static float[] afterPinkyFlexions;//get flexions of all digits of Middle after grasping from SG_HandPose.TotalFlexions
        //--------------------------------------------------------------------------------------------------------------------------
        // Properties

        #region Properties

        /// <summary> The way that this object is be picked up by a GrabScript. </summary>
        [Header("Grabable Options")]
        [Tooltip("The way that this object is be picked up by a GrabScript.")]
        public GrabType pickupMethod = GrabType.Parent;

        /// <summary> The way this object connects itself to the grabscript. </summary>
        [Tooltip("The way this object connects itself to the grabscript")]
        public AttachType attachMethod = AttachType.Default;

        /// <summary> The actual snapReference, based on if the hand is left or right. </summary>
        protected Transform snapReference = null;

        /// <summary> If this object has an attachType of SnapToAnchor, this transform is used as a refrence. </summary>
        [Tooltip("If this object has an attachType of SnapToAnchor, this transform is used as a refrence.")]
        public Transform leftSnapPoint = null, rightSnapPoint = null;


        /// <summary> Whether or not this object can be picked up by another Grabscript while it is being held. </summary>
        [Tooltip("Whether or not this object can be picked up from the Sense Glove by another Grabscript.")]
        public bool canTransfer = true;

        /// <summary> The transform that is grabbed instead of this object. Useful when dealing with a grabable that is a child of another grabable. </summary>
        [Tooltip("The transform that is grabbed instead of this object. Useful when dealing with a grabable that is a child of another grabable.")]
        public Transform pickupReference;

        /// <summary> The gameObject used as a reference for the Grabable's transform updates. </summary>
        protected GameObject grabReference;

        //Folllow GrabType Variables

        /// <summary> The xyz offset of this Grabable's transform to the grabReference, on the moment it was picked up. </summary>
        protected Vector3 grabOffset = Vector3.zero;
        /// <summary> The quaternion offset of this Grabable's transform to the grabReference, on the moment it was picked up.  </summary>
        protected Quaternion grabRotation = Quaternion.identity;

        //Parent GrabType Variables

        protected Transform originalParent;

        //PhysicsJoint GrabType Variables

        protected Joint connection;

        //Object RigidBody Variables

        /// <summary> The rigidBody to which thumbVelocity, gravity and kinematic options are applied. </summary>
        [Tooltip("The rigidBody to which thumbVelocity, gravity and kinematic options are applied. The script automatically connects to the Rigidbody attached to this GameObject.")]
        public Rigidbody physicsBody;

        /// <summary> Whether this grabable's physicsBody was kinematic before it was picked up. </summary>
        protected bool wasKinematic;
        /// <summary> Whether this grabable's physicsBody was used gravity before it was picked up. </summary>
        protected bool usedGravity;

        public const float defaultBreakForce = 4000;

        #endregion Properties


        //--------------------------------------------------------------------------------------------------------------------------
        // Class methods

        #region ClassMethods

        /// <summary> Called when a SG_GrabScript initiates an interaction with this grabable. </summary>
        /// <param name="grabScript"></param>
        /// <param name="fromExternal"></param>
        /// 


        private void Start()
        {
            sounds = GetComponents<AudioSource>(); //get components in unity
            noise1 = sounds[0]; //save it in first index
            noise2 = sounds[1];//save it in second index
        }

        protected override bool InteractionBegin(SG_GrabScript grabScript, bool fromExternal = false)
        {
            grasp = true; //flag to check if properly grasped
            Grabbed = true; //flag to indicate start of interaction  

            ///CMC,MCP,PIP Of Thumb
            CMCFlexionThumbAfter = afterThumbFlexions[0];
            MCPFlexionThumbAfter = afterThumbFlexions[1];
            IPFlexionThumbAfter = afterThumbFlexions[2];

            ///MCP,PIP,DIP Of Index
            MCPFlexionIndexAfter = afterIndexFlexions[0];
            PIPFlexionIndexAfter = afterIndexFlexions[1];
            DIPFlexionIndexAfter = afterIndexFlexions[2];

            //////MCP,PIP,DIP Of Middle
            MCPFlexionMiddleAfter = afterMiddleFlexions[0];
            PIPFlexionMiddleAfter = afterMiddleFlexions[1];
            DIPFlexionMiddleAfter = afterMiddleFlexions[2];

            //////MCP,PIP,DIP Of Ring
            MCPFlexionRingAfter = afterRingFlexions[0];
            PIPFlexionRingAfter = afterRingFlexions[1];
            DIPFlexionRingAfter = afterRingFlexions[2];

            //////MCP,PIP,DIP Of Pinky
            MCPFlexionPinkyAfter = afterPinkyFlexions[0];
            PIPFlexionPinkyAfter = afterPinkyFlexions[1];
            DIPFlexionPinkyAfter = afterPinkyFlexions[2];

            ///Total flexion for each finger
            totalFlexionThumbAfter = afterTotalFlexions[0]; //total flexion of thumb after grasp
            totalFlexionIndexAfter = afterTotalFlexions[1]; //total flexion of index after grasp
            totalFlexionMiddleAfter = afterTotalFlexions[2]; //total flexion of middle after grasp
            totalFlexionRingAfter = afterTotalFlexions[3]; //total flexion of ring after grasp
            totalFlexionPinkyAfter = afterTotalFlexions[4]; //total flexion of pinky after grasp

            ////////////////////////////////////////////////////////////////////////////////
            if (this.isInteractable) //never interact twice with the same grabscript before EndInteraction is called.
            {
                // SenseGlove_Debugger.Log("Begin Interaction with " + grabScript.name);
                //points++;
                bool alreadyBeingHeld = this.IsInteracting();

                //if the object was actually grabbed.
                if (!alreadyBeingHeld || (alreadyBeingHeld && this.canTransfer))
                {
                    //todo release?
                    if (GrabScript != null)
                    {

                        this.GrabScript.EndInteraction(this, false);

                    }

                    this.grabReference = grabScript.grabReference;
                    this._grabScript = grabScript;

                    this.originalDist = (grabScript.grabReference.transform.position - this.pickupReference.transform.position).magnitude;

                    if (this.attachMethod != AttachType.Default)
                    {
                        if (this.attachMethod != AttachType.Default)
                        {
                            if (this.attachMethod == AttachType.SnapToAnchor)
                            {
                                this.SnapMeTo(grabScript.grabAnchor.transform, grabScript.TrackedHand.TracksRightHand);
                            }
                            //other attachmethods.
                        }
                    }


                    //Apply proper pickup 
                    if (this.pickupMethod == GrabType.Parent)
                    {
                        
                        this.pickupReference.parent = grabScript.grabReference.transform;
                    }
                    else if (this.pickupMethod == GrabType.Follow)
                    {
                        //Quaternion.Inverse(QT) * (vT - vO);
                        this.grabOffset = Quaternion.Inverse(this.grabReference.transform.rotation) * (this.grabReference.transform.position - this.pickupReference.position);

                        //Quaternion.Inverse(QT) * (Qo);
                        this.grabRotation = Quaternion.Inverse(this.grabReference.transform.rotation) * this.pickupReference.rotation;
                    }
                    else if (this.pickupMethod == GrabType.FixedJoint)
                    {
                        this.ConnectJoint(grabScript.grabAnchor);
                    }


                    //apply physicsBody settings.
                    if (this.physicsBody)
                    {
                        this.physicsBody.velocity = new Vector3(0, 0, 0);
                        this.physicsBody.velocity= new Vector3(0, 0, 0);
                        if (this.pickupMethod != GrabType.FixedJoint)
                        {
                            this.physicsBody.useGravity = false;
                            this.physicsBody.isKinematic = true;
                        }
                    }
                    return true;
                }

            }

            return false;
        }

        /// <summary> Called when this object is being held and the GrabReference is updated. </summary>
        public override void UpdateInteraction()
        {
            if (this.grabReference != null)
            {
                if (this.pickupMethod == GrabType.Follow)
                {
                    this.pickupReference.rotation = this.grabReference.transform.rotation * this.grabRotation;
                    this.pickupReference.position = this.grabReference.transform.position - (this.grabReference.transform.rotation * grabOffset);
                }
            }
        }

        /// <summary> Called when a SG_GrabScript no longer wishes to interact with this grabable. </summary>
        /// <param name="grabScript"></param>
        /// <param name="fromExternal"></param>
        protected override bool InteractionEnd(SG_GrabScript grabScript, bool fromExternal = false)
        {
           
            /// <summary>
            /// /////////// Thumb //////////////
            /// </summary>
            ///
            //calculate initial angle difference for thumb
            initialCMCthumbAngle = CMCFlexionThumbAfter - CMCFlexionThumbInitial;
            initialMCPthumbAngle = MCPFlexionThumbAfter - MCPFlexionThumbInitial;
            initialIPthumbAngle = IPFlexionThumbAfter - IPFlexionThumbInitial;

            //Flexion of thumb before grasping
            CMCFlexionThumbBefore = beforeThumbFlexions[0];
            MCPFlexionThumbBefore = beforeThumbFlexions[1];
            IPFlexionThumbBefore = beforeThumbFlexions[2];

            //calculate angle difference for thumb
            CMCthumbAngle = CMCFlexionThumbAfter - CMCFlexionThumbBefore;
            MCPthumbAngle = MCPFlexionThumbAfter - MCPFlexionThumbBefore;
            IPthumbAngle = IPFlexionThumbAfter - IPFlexionThumbBefore;

            /// <summary>
            /// /////////// Index //////////////
            /// </summary>
            ///
            //calculate initial angle difference for index
            initialMCPindexAngle = MCPFlexionIndexAfter - MCPFlexionIndexInitial;
            initialPIPindexAngle = PIPFlexionIndexAfter - PIPFlexionIndexInitial;
            initialDIPindexAngle = DIPFlexionIndexAfter - DIPFlexionIndexInitial;

            //Flexion of index before grasping
            MCPFlexionIndexBefore = beforeIndexFlexions[0];
            PIPFlexionIndexBefore = beforeIndexFlexions[1];
            DIPFlexionIndexBefore = beforeIndexFlexions[2];

            //calculate angle difference for index
            MCPIndexAngle = MCPFlexionIndexAfter - MCPFlexionIndexBefore;
            PIPIndexAngle = PIPFlexionIndexAfter - PIPFlexionIndexBefore;
            DIPIndexAngle = DIPFlexionIndexAfter - DIPFlexionIndexBefore;

            /// <summary>
            /// /////////// Middle //////////////
            /// </summary>

            //calculate initial angle difference for middle
            initialMCPmiddleAngle = MCPFlexionMiddleAfter - MCPFlexionMiddleInitial;
            initialPIPmiddleAngle = PIPFlexionMiddleAfter - PIPFlexionMiddleInitial;
            initialDIPmiddleAngle = DIPFlexionMiddleAfter - DIPFlexionMiddleInitial;

            //Flexion of  middle before grasping
            MCPFlexionMiddleBefore = beforeMiddleFlexions[0];
            PIPFlexionMiddleBefore = beforeMiddleFlexions[1];
            DIPFlexionMiddleBefore = beforeMiddleFlexions[2];

            //calculate angle difference for  middle
            MCPmiddleAngle = MCPFlexionMiddleAfter - MCPFlexionMiddleBefore;
            PIPmiddleAngle = PIPFlexionMiddleAfter - PIPFlexionMiddleBefore;
            DIPmiddleAngle = DIPFlexionMiddleAfter - DIPFlexionMiddleBefore;

            /// <summary>
            /// /////////// Ring //////////////
            /// </summary>

            //calculate initial angle difference for ring
            initialMCPRingAngle = MCPFlexionRingAfter - MCPFlexionRingInitial;
            initialPIPRingAngle = PIPFlexionRingAfter - PIPFlexionRingInitial;
            initialDIPRingAngle = DIPFlexionRingAfter - DIPFlexionRingInitial;

            //Flexion of  Ring before grasping
            MCPFlexionRingBefore = beforeRingFlexions[0];
            PIPFlexionRingBefore = beforeRingFlexions[1];
            DIPFlexionRingBefore = beforeRingFlexions[2];

            //calculate angle difference for ring
            MCPRingAngle = MCPFlexionRingAfter - MCPFlexionRingBefore;
            PIPRingAngle = PIPFlexionRingAfter - PIPFlexionRingBefore;
            DIPRingAngle = DIPFlexionRingAfter - DIPFlexionRingBefore;

            /// <summary>
            /// /////////// Pinky //////////////
            /// </summary>

            //calculate initial angle difference for pinky
            initialMCPPinkyAngle = MCPFlexionPinkyAfter - MCPFlexionPinkyInitial;
            initialPIPPinkyAngle = PIPFlexionPinkyAfter - PIPFlexionPinkyInitial;
            initialDIPPinkyAngle = DIPFlexionPinkyAfter - DIPFlexionPinkyInitial;

            //Flexion of  pinky before grasping
            MCPFlexionPinkyBefore = beforePinkyFlexions[0];
            PIPFlexionPinkyBefore = beforePinkyFlexions[1];
            DIPFlexionPinkyBefore = beforePinkyFlexions[2];

            //calculate angle difference for pinky
            MCPPinkyAngle = MCPFlexionPinkyAfter - MCPFlexionPinkyBefore;
            PIPPinkyAngle = PIPFlexionPinkyAfter - PIPFlexionPinkyBefore;
            DIPPinkyAngle = DIPFlexionPinkyAfter - DIPFlexionPinkyBefore;

            /// <summary>
            /// /////////// Fingers //////////////
            /// </summary>
            ///
            //store total flexion angle in degrees for each finger
            totalFlexionThumbBefore = beforeTotalFlexions[0];
            totalFlexionIndexBefore = beforeTotalFlexions[1];
            totalFlexionMiddleBefore = beforeTotalFlexions[2];
            totalFlexionRingBefore = beforeTotalFlexions[3];
            totalFlexionPinkyBefore = beforeTotalFlexions[4];
 
            //calculate angle difference for each finger
            thumbAngle = totalFlexionThumbAfter - totalFlexionThumbBefore;
            indexAngle = totalFlexionIndexAfter - totalFlexionIndexBefore;
            middleAngle = totalFlexionMiddleAfter - totalFlexionMiddleBefore;
            ringAngle = totalFlexionRingAfter - totalFlexionRingBefore;
            pinkyAngle = totalFlexionPinkyAfter - totalFlexionPinkyBefore;


            /// <summary>
            /// /////////// Thumb //////////////
            /// </summary>
            ///

            if (initialCMCthumbAngle < 0)
            {
                initialCMCthumbAngle = initialCMCthumbAngle * -1;
            }

            if (initialMCPthumbAngle <0)
            {
                initialMCPthumbAngle = initialMCPthumbAngle * -1;
            }

            if (initialIPthumbAngle < 0)
            {
                initialIPthumbAngle = initialIPthumbAngle * -1;
            }

            if (CMCthumbAngle < 0)
            {
                CMCthumbAngle = CMCthumbAngle * -1;
            }

            if (MCPthumbAngle < 0)
            {
                MCPthumbAngle = MCPthumbAngle * -1;
            }

            if (IPthumbAngle < 0)
            {
                IPthumbAngle = IPthumbAngle * -1;
            }

            /// <summary>
            /// /////////// Index //////////////
            /// </summary>
            ///
            //if negative value of index
            if (initialMCPindexAngle < 0)
            {
                initialMCPindexAngle = initialMCPindexAngle * -1;
            }

            if (initialPIPindexAngle < 0)
            {
                initialPIPindexAngle = initialPIPindexAngle * -1;
            }

            if (initialDIPindexAngle < 0)
            {
                initialDIPindexAngle = initialDIPindexAngle * -1;
            }

            if (MCPIndexAngle < 0)
            {
                MCPIndexAngle = MCPIndexAngle * -1;
            }

            if (PIPIndexAngle < 0)
            {
                PIPIndexAngle = PIPIndexAngle * -1;
            }

            if (DIPIndexAngle < 0)
            {
                DIPIndexAngle = DIPIndexAngle * -1;
            }

            /// <summary>
            /// /////////// Middle //////////////
            /// </summary>
            /// 

            if (initialMCPmiddleAngle < 0)
            {
                initialMCPmiddleAngle = initialMCPmiddleAngle * -1;
            }

            if (initialPIPmiddleAngle < 0)
            {
                initialPIPmiddleAngle = initialPIPmiddleAngle * -1;
            }

            if (initialDIPmiddleAngle < 0)
            {
                initialDIPmiddleAngle = initialDIPmiddleAngle * -1;
            }

            if (MCPmiddleAngle < 0)
            {
                MCPmiddleAngle = MCPmiddleAngle * -1;
            }

            if (PIPmiddleAngle < 0)
            {
                PIPmiddleAngle = PIPmiddleAngle * -1;
            }

            if (DIPmiddleAngle < 0)
            {
                DIPmiddleAngle = DIPmiddleAngle * -1;
            }

            /// <summary>
            /// /////////// Ring //////////////
            /// </summary>
            ///

            if (initialMCPRingAngle < 0)
            {
                initialMCPRingAngle = initialMCPRingAngle * -1;
            }

            if (initialPIPRingAngle < 0)
            {
                initialPIPRingAngle = initialPIPRingAngle * -1;
            }

            if (initialDIPRingAngle < 0)
            {
                initialDIPRingAngle = initialDIPRingAngle * -1;
            }

            if (MCPRingAngle < 0)
            {
                MCPRingAngle = MCPRingAngle * -1;
            }

            if (PIPRingAngle < 0)
            {
                PIPRingAngle = PIPRingAngle * -1;
            }

            if (DIPRingAngle < 0)
            {
                DIPRingAngle = DIPRingAngle * -1;
            }

            /// <summary>
            /// /////////// pinky //////////////
            /// </summary>
            ///

            if (initialMCPPinkyAngle < 0)
            {
                initialMCPPinkyAngle = initialMCPPinkyAngle * -1;
            }

            if (initialPIPPinkyAngle < 0)
            {
                initialPIPPinkyAngle = initialPIPPinkyAngle * -1;
            }

            if (initialDIPPinkyAngle < 0)
            {
                initialDIPPinkyAngle = initialDIPPinkyAngle * -1;
            }

            if (MCPPinkyAngle < 0)
            {
                MCPPinkyAngle = MCPPinkyAngle * -1;
            }

            if (PIPPinkyAngle < 0)
            {
                PIPPinkyAngle = PIPPinkyAngle * -1;
            }

            if (DIPPinkyAngle < 0)
            {
                DIPPinkyAngle = DIPPinkyAngle * -1;
            }

            /// <summary>
            /// /////////// Finger //////////////
            /// </summary>
            /// 
            if (thumbAngle < 0)
            {
                thumbAngle = thumbAngle * -1;
            }

            if (indexAngle < 0)
            {
                indexAngle = indexAngle * -1;
            }

            if (middleAngle < 0)
            {
                middleAngle = middleAngle * -1;
            }

            if (ringAngle < 0)
            {
                ringAngle = ringAngle * -1;
            }

            if (pinkyAngle < 0)
            {
                pinkyAngle = pinkyAngle * -1;
            }

            /// <summary>
            /// /////////// Thumb //////////////
            /// </summary>
            /// 
            //Rounding to two digits of thumb
            initialCMCthumbAngle = Mathf.Round(initialCMCthumbAngle * 100.0f) * 0.01f;
            initialMCPthumbAngle = Mathf.Round(initialMCPthumbAngle * 100.0f) * 0.01f;
            initialIPthumbAngle = Mathf.Round(initialIPthumbAngle * 100.0f) * 0.01f;
            MCPthumbAngle = Mathf.Round(MCPthumbAngle * 100.0f) * 0.01f;
            CMCthumbAngle = Mathf.Round(CMCthumbAngle * 100.0f) * 0.01f;
            IPthumbAngle = Mathf.Round(IPthumbAngle * 100.0f) * 0.01f;

            //calculate thumb Velocity and thumb Acceleration for each joint
            CMCthumbVelocity = CMCthumbAngle / interactionSeconds;
            CMCthumbVelocity = Mathf.Round(CMCthumbVelocity * 100.0f) * 0.01f;//round 2 decimal places
            MCPthumbVelocity = MCPthumbAngle / interactionSeconds;
            MCPthumbVelocity = Mathf.Round(MCPthumbVelocity * 100.0f) * 0.01f;//round 2 decimal places
            IPthumbVelocity = IPthumbAngle / interactionSeconds;
            IPthumbVelocity = Mathf.Round(IPthumbVelocity * 100.0f) * 0.01f;//round 2 decimal places
            CMCthumbAcceleration = CMCthumbVelocity / interactionSeconds;
            CMCthumbAcceleration = Mathf.Round(CMCthumbAcceleration * 100.0f) * 0.01f;//round 2 decimal places
            MCPthumbAcceleration = MCPthumbVelocity / interactionSeconds;
            MCPthumbAcceleration = Mathf.Round(MCPthumbAcceleration * 100.0f) * 0.01f;//round 2 decimal places
            IPthumbAcceleration = IPthumbVelocity / interactionSeconds;
            IPthumbAcceleration = Mathf.Round(IPthumbAcceleration * 100.0f) * 0.01f;//round 2 decimal places

            /// <summary>
            /// /////////// Index //////////////
            /// </summary>
            /// 
            //Rounding to two digits of Index
            initialMCPindexAngle = Mathf.Round(initialMCPindexAngle * 100.0f) * 0.01f;
            initialPIPindexAngle = Mathf.Round(initialPIPindexAngle * 100.0f) * 0.01f;
            initialDIPindexAngle = Mathf.Round(initialDIPindexAngle * 100.0f) * 0.01f;
            MCPIndexAngle = Mathf.Round(MCPIndexAngle * 100.0f) * 0.01f;
            PIPIndexAngle = Mathf.Round(PIPIndexAngle * 100.0f) * 0.01f;
            DIPIndexAngle = Mathf.Round(DIPIndexAngle * 100.0f) * 0.01f;

            //calculate index Velocity and index Acceleration for each joint
            MCPindexVelocity = MCPIndexAngle/interactionSeconds;
            MCPindexVelocity = Mathf.Round(MCPindexVelocity * 100.0f) * 0.01f;//round 2 decimal places
            PIPindexVelocity = PIPIndexAngle/ interactionSeconds;
            PIPindexVelocity = Mathf.Round(PIPindexVelocity * 100.0f) * 0.01f;//round 2 decimal places
            DIPindexVelocity = DIPIndexAngle / interactionSeconds;
            DIPindexVelocity = Mathf.Round(DIPindexVelocity * 100.0f) * 0.01f;//round 2 decimal places
            MCPindexAcceleration = MCPindexVelocity/ interactionSeconds;
            MCPindexAcceleration = Mathf.Round(MCPindexAcceleration * 100.0f) * 0.01f;//round 2 decimal places
            PIPindexAcceleration = PIPindexVelocity / interactionSeconds;
            PIPindexAcceleration = Mathf.Round(PIPindexAcceleration * 100.0f) * 0.01f;//round 2 decimal places
            DIPindexAcceleration = DIPindexVelocity / interactionSeconds;
            DIPindexAcceleration = Mathf.Round(DIPindexAcceleration * 100.0f) * 0.01f;//round 2 decimal places

            /// <summary>
            /// /////////// Middle //////////////
            /// </summary>
            /// 
             //Rounding to two digits of Middle
            initialMCPmiddleAngle = Mathf.Round(initialMCPmiddleAngle * 100.0f) * 0.01f;
            initialPIPmiddleAngle = Mathf.Round(initialPIPmiddleAngle * 100.0f) * 0.01f;
            initialDIPmiddleAngle = Mathf.Round(initialDIPmiddleAngle * 100.0f) * 0.01f;
            MCPmiddleAngle = Mathf.Round(MCPmiddleAngle * 100.0f) * 0.01f;
            PIPmiddleAngle = Mathf.Round(PIPmiddleAngle * 100.0f) * 0.01f;
            DIPmiddleAngle = Mathf.Round(DIPmiddleAngle * 100.0f) * 0.01f;

            //calculate middle Velocity and index Acceleration for each joint
            MCPmiddleVelocity = MCPmiddleAngle / interactionSeconds;
            MCPmiddleVelocity = Mathf.Round(MCPmiddleVelocity * 100.0f) * 0.01f;//round 2 decimal places
            PIPmiddleVelocity = PIPmiddleAngle / interactionSeconds;
            PIPmiddleVelocity = Mathf.Round(PIPmiddleVelocity * 100.0f) * 0.01f;//round 2 decimal places
            DIPmiddleVelocity = DIPmiddleAngle / interactionSeconds;
            DIPmiddleVelocity = Mathf.Round(DIPmiddleVelocity * 100.0f) * 0.01f;//round 2 decimal places
            MCPmiddleAcceleration = MCPmiddleVelocity / interactionSeconds;
            MCPmiddleAcceleration = Mathf.Round(MCPmiddleAcceleration * 100.0f) * 0.01f;//round 2 decimal places
            PIPmiddleAcceleration = PIPmiddleVelocity / interactionSeconds;
            PIPmiddleAcceleration = Mathf.Round(PIPmiddleAcceleration * 100.0f) * 0.01f;//round 2 decimal places
            DIPmiddleAcceleration = DIPmiddleVelocity / interactionSeconds;
            DIPmiddleAcceleration = Mathf.Round(DIPmiddleAcceleration * 100.0f) * 0.01f;//round 2 decimal places

            /// <summary>
            /// /////////// Ring //////////////
            /// </summary>
            /// 

            //Rounding to two digits of Ring
            initialMCPRingAngle = Mathf.Round(initialMCPRingAngle * 100.0f) * 0.01f;
            initialPIPRingAngle = Mathf.Round(initialPIPRingAngle * 100.0f) * 0.01f;
            initialDIPRingAngle = Mathf.Round(initialDIPRingAngle * 100.0f) * 0.01f;
            MCPRingAngle = Mathf.Round(MCPRingAngle * 100.0f) * 0.01f;
            PIPRingAngle = Mathf.Round(PIPRingAngle * 100.0f) * 0.01f;
            DIPRingAngle = Mathf.Round(DIPRingAngle * 100.0f) * 0.01f;

            //calculate Ring Velocity and Acceleration for each joint
            MCPringVelocity = MCPRingAngle / interactionSeconds;
            MCPringVelocity = Mathf.Round(MCPringVelocity * 100.0f) * 0.01f;//round 2 decimal places
            PIPringVelocity = PIPRingAngle / interactionSeconds;
            PIPringVelocity = Mathf.Round(PIPringVelocity * 100.0f) * 0.01f;//round 2 decimal places
            DIPringVelocity = DIPRingAngle / interactionSeconds;
            DIPringVelocity = Mathf.Round(DIPringVelocity * 100.0f) * 0.01f;//round 2 decimal places
            MCPringAcceleration = MCPringVelocity / interactionSeconds;
            MCPringAcceleration = Mathf.Round(MCPringAcceleration * 100.0f) * 0.01f;//round 2 decimal places
            PIPringAcceleration = PIPringVelocity / interactionSeconds;
            PIPringAcceleration = Mathf.Round(PIPringAcceleration * 100.0f) * 0.01f;//round 2 decimal places
            DIPringAcceleration = DIPringVelocity / interactionSeconds;
            DIPringAcceleration = Mathf.Round(DIPringAcceleration * 100.0f) * 0.01f;//round 2 decimal places

            /// <summary>
            /// ///////////////////// Pinky //////////////
            /// </summary>

            //Rounding to two digits of pinky
            initialMCPPinkyAngle = Mathf.Round(initialMCPPinkyAngle * 100.0f) * 0.01f;
            initialPIPPinkyAngle = Mathf.Round(initialPIPPinkyAngle * 100.0f) * 0.01f;
            initialDIPPinkyAngle = Mathf.Round(initialDIPPinkyAngle * 100.0f) * 0.01f;
            MCPPinkyAngle = Mathf.Round(MCPPinkyAngle * 100.0f) * 0.01f;
            PIPPinkyAngle = Mathf.Round(PIPPinkyAngle * 100.0f) * 0.01f;
            DIPPinkyAngle = Mathf.Round(DIPPinkyAngle * 100.0f) * 0.01f;

            //calculate pinky Velocity and pinky Acceleration for each joint
            MCPpinkyVelocity = MCPPinkyAngle / interactionSeconds;
            MCPpinkyVelocity = Mathf.Round(MCPpinkyVelocity * 100.0f) * 0.01f;//round 2 decimal places
            PIPpinkyVelocity = PIPPinkyAngle / interactionSeconds;
            PIPpinkyVelocity = Mathf.Round(PIPpinkyVelocity * 100.0f) * 0.01f;//round 2 decimal places
            DIPpinkyVelocity = DIPPinkyAngle / interactionSeconds;
            DIPpinkyVelocity = Mathf.Round(DIPpinkyVelocity * 100.0f) * 0.01f;//round 2 decimal places
            MCPpinkyAcceleration = MCPpinkyVelocity / interactionSeconds;
            MCPpinkyAcceleration = Mathf.Round(MCPpinkyAcceleration * 100.0f) * 0.01f;//round 2 decimal places
            PIPpinkyAcceleration = PIPpinkyVelocity / interactionSeconds;
            PIPpinkyAcceleration = Mathf.Round(PIPpinkyAcceleration * 100.0f) * 0.01f;//round 2 decimal places
            DIPpinkyAcceleration = DIPpinkyVelocity / interactionSeconds;
            DIPpinkyAcceleration = Mathf.Round(DIPpinkyAcceleration * 100.0f) * 0.01f;//round 2 decimal places

            /// <summary>
            /// ///////////////////// Finger //////////////
            /// </summary>
            /// 
            //Rounding to two digits of each finger
            thumbAngle = Mathf.Round(thumbAngle * 100.0f) * 0.01f;//round 2 decimal places
            indexAngle = Mathf.Round(indexAngle * 100.0f) * 0.01f;//round 2 decimal places
            middleAngle = Mathf.Round(middleAngle * 100.0f) * 0.01f;//round 2 decimal places
            ringAngle = Mathf.Round(ringAngle * 100.0f) * 0.01f;//round 2 decimal places
            pinkyAngle = Mathf.Round(pinkyAngle * 100.0f) * 0.01f;//round 2 decimal places

            //calculate thumb Velocity and thumb Acceleration
            thumbVelocity = thumbAngle / interactionSeconds;//calculate thumb Velocity
            thumbVelocity = Mathf.Round(thumbVelocity * 100.0f) * 0.01f;//round 2 decimal places
            thumbAcceleration = thumbVelocity / interactionSeconds; //calculate thumb Acceleration
            thumbAcceleration = Mathf.Round(thumbAcceleration * 100.0f) * 0.01f;//round 2 decimal places

            //calculate indexVelocity and indexAcceleration
            indexVelocity = indexAngle / interactionSeconds;//calculate thumbVelocity
            indexVelocity = Mathf.Round(indexVelocity * 100.0f) * 0.01f;//round 2 decimal places
            indexAcceleration = indexVelocity / interactionSeconds; //calculate thumbAcceleration
            indexAcceleration = Mathf.Round(indexAcceleration * 100.0f) * 0.01f;//round 2 decimal places

            //calculate middleVelocity and middleAcceleration
            middleVelocity = middleAngle / interactionSeconds;//calculate thumbVelocity
            middleVelocity = Mathf.Round(middleVelocity * 100.0f) * 0.01f;//round 2 decimal places
            middleAcceleration = middleVelocity / interactionSeconds; //calculate thumbAcceleration
            middleAcceleration = Mathf.Round(middleAcceleration * 100.0f) * 0.01f;//round 2 decimal places

            //calculate ringVelocity and middleAcceleration
            ringVelocity = ringAngle / interactionSeconds;//calculate thumbVelocity
            ringVelocity = Mathf.Round(middleVelocity * 100.0f) * 0.01f;//round 2 decimal places
            ringAcceleration = ringVelocity / interactionSeconds; //calculate thumbAcceleration
            ringAcceleration = Mathf.Round(ringAcceleration * 100.0f) * 0.01f;//round 2 decimal places

            //calculate pinkyVelocity and pinkyAcceleration
            pinkyVelocity = pinkyAngle / interactionSeconds;//calculate thumbVelocity
            pinkyVelocity = Mathf.Round(pinkyVelocity * 100.0f) * 0.01f;//round 2 decimal places
            pinkyAcceleration = pinkyVelocity / interactionSeconds; //calculate thumbAcceleration
            pinkyAcceleration = Mathf.Round(pinkyAcceleration * 100.0f) * 0.01f;//round 2 decimal places

            //calculate score on specific place in the environment
            if (transform.position.x >= 1.1 && transform.position.x <= 2.1)
            {
                if (transform.position.z >= 0.1 && transform.position.z <= 1.1)
                {
                    if (interactionSeconds > 0)
                    {
                        ScoringSystem.theScore += 1;//increment score
                        noise1.Play();//soundEffect for correct place
                        totalThumbAngle += thumbAngle;//sum all angles to calculate average thumbVelocity
                        totalthumbAcceleration += thumbAcceleration;//sum all thumbAcceleration to calculate average accceleration
                        totalInteractionSeconds += interactionSeconds;//sum interactionSeconds
                        canWrite = true;//flag to write in the excel
                    }       
                }
            }
            else
            {
                noise2.Play();//play failure sound
            }
            ///////////////////////////////////////////////////////////////////////
            if (this.InteractingWith(grabScript) || fromExternal)
            {
                if (this.IsInteracting())
                {   //break every possible instance that could connect this interactable to the grabscript.
                    if (this.pickupReference != null)
                        this.pickupReference.parent = this.originalParent;

                    this.BreakJoint();

                    if (this.physicsBody != null)
                    {
                        this.physicsBody.useGravity = this.usedGravity;
                        this.physicsBody.isKinematic = this.wasKinematic;
                        if (grabScript != null)
                        {
                            this.physicsBody.velocity = grabScript.GetVelocity();
                            this.physicsBody.angularVelocity = grabScript.GetAngularVelocity();
                        }
                    }
                }

                this._grabScript = null;
                this.grabReference = null;

                return true;
            }

            return false;
        }

        /// <summary> Moves this Grbabale such that its snapRefrence matches the rotation and position of the originToMatch. </summary>
        /// <param name="originToMatch"></param>
        public void SnapMeTo(Transform originToMatch, bool rightHand)
        {
            if (rightSnapPoint == null || leftSnapPoint == null) { CheckPickupRef(); }
            this.snapReference = rightHand ? rightSnapPoint : leftSnapPoint;

            Quaternion Qto = originToMatch.rotation;
            Quaternion Qmain = this.pickupReference.transform.rotation;
            Quaternion Qsub = this.snapReference.transform.rotation;

            Quaternion QMS = Quaternion.Inverse(Qmain) * Qsub;
            this.pickupReference.rotation = (Qto) * Quaternion.Inverse(QMS);

            //calculate diff between my snapanchor and the glove's grabAnchor. 
            Vector3 dPos = this.snapReference.position - originToMatch.transform.position;
            this.pickupReference.transform.position = this.pickupReference.transform.position - dPos;
        }

        /// <summary> Save this object's position and orientation, in case the ResetObject function is called. </summary>
        public override void SaveTransform()
        {
            this.CheckPickupRef();
            this.originalParent = this.pickupReference.parent;
            this.originalPos = this.pickupReference.position;
            this.originalRot = this.pickupReference.rotation;
        }

        /// <summary> Reset this object back to its original position. Removes all connections between this and grabscripts. </summary>
        public override void ResetObject()
        {
            this.OnObjectReset();
            this.CheckPickupRef();

            this.BreakJoint();

            this.pickupReference.parent = originalParent;

            this.pickupReference.position = this.originalPos;
            this.pickupReference.rotation = this.originalRot;

            if (this.physicsBody)
            {
                this.physicsBody.velocity = Vector3.zero;
                this.physicsBody.angularVelocity = Vector3.zero;
                this.physicsBody.isKinematic = this.wasKinematic;
                this.physicsBody.useGravity = this.usedGravity;
            }
        }

        /// <summary>
        /// Check if this Interactable is currently being held by a SenseGlove GrabScript.
        /// </summary>
        /// <returns></returns>
        public bool IsGrabbed()
        {
            //Grabbed = true;
            return this.grabReference != null;
        }


        #endregion ClassMethods


        //----------------------------------------------------------------------------------------------------------------------------------
        // Utility Methods

        #region Utility

        /// <summary> The original parent of this Grabable, before any GrabScripts picked it up. </summary>
        public Transform OriginalParent
        {
            get { return this.originalParent; }
            set { this.originalParent = value; }
        }

        /// <summary> Whether this Grabable used gravity before it was picked up </summary>
        public bool UsedGravity
        {
            get { return this.usedGravity; }
            set { this.usedGravity = value; }
        }

        /// <summary> Whether this Grabable was marked as Kinematic before it was picked up </summary>
        public bool WasKinematic
        {
            get { return this.wasKinematic; }
            set { this.wasKinematic = value; }
        }


        /// <summary> Set the Velocities of this script to 0. Stops the grabable from rotating / flying away. </summary>
        public void ZerothumbVelocity()
        {
            if (this.physicsBody)
            {
                this.physicsBody.velocity = Vector3.zero;
                this.physicsBody.velocity = Vector3.zero;
            }
        }

        /// <summary> Connect this Grabable's rigidBody to another using a FixedJoint </summary>
        /// <param name="other"></param>
        /// <returns>True, if the connection was sucesfully made.</returns>
        public bool ConnectJoint(Rigidbody other, float breakForce = SG_Grabable.defaultBreakForce)
        {
            if (other != null)
            {
                if (this.physicsBody)
                {
                    this.connection = this.physicsBody.gameObject.AddComponent<FixedJoint>();
                    this.connection.connectedBody = other;
                    this.connection.enableCollision = false;
                    this.connection.breakForce = breakForce;
                    return true;
                }
                else
                {
                    SG_Debugger.Log("Using a FixedJoint connection requires a Rigidbody.");
                }
            }
            else
            {
                SG_Debugger.Log("No rigidbody to connect to " + other.name);
            }
            return false;
        }

        /// <summary> Remove a fixedJoint connection between this object and another. </summary>
        public void BreakJoint()
        {
            if (this.connection != null)
            {
                GameObject.Destroy(this.connection);
                this.connection = null;
            }
        }



        /// <summary> Enable/Disable rigidbody collision of this Grabable. </summary>
        /// <param name="active"></param>
        public void SetCollision(bool active)
        {
            if (this.physicsBody)
            {
                this.physicsBody.detectCollisions = active;
            }
        }

        /// <summary> Check the PickupReference of this Grabable </summary>
        public virtual void CheckPickupRef()
        {
            if (this.pickupReference == null)
            {
                this.pickupReference = this.transform;
            }

            if (leftSnapPoint == null && rightSnapPoint == null)
            {
                this.rightSnapPoint = this.transform;
                this.leftSnapPoint = this.transform;
            }
            if (leftSnapPoint == null && rightSnapPoint != null)
            {
                leftSnapPoint = rightSnapPoint;
            }
            else if (leftSnapPoint != null && rightSnapPoint == null)
            {
                rightSnapPoint = leftSnapPoint;
            }
        }

        /// <summary> Store the RigidBody parameters of this Grabable </summary>
        public virtual void SaveRBParameters()
        {
            if (!this.physicsBody) { this.physicsBody = this.pickupReference.GetComponent<Rigidbody>(); }

            //Verify the kinematic variables
            if (this.physicsBody)
            {
                this.wasKinematic = this.physicsBody.isKinematic;
                this.usedGravity = this.physicsBody.useGravity;
            }
        }

        #endregion Utility


        //--------------------------------------------------------------------------------------------------------------------------
        // Monobehaviour

        #region Monobehaviour

        public virtual void Awake()
        {
            this.CheckPickupRef();

            //Temp fix where a grabable is registered twice when it doesn't have a RigidBody...
            if (!this.physicsBody) { this.physicsBody = this.pickupReference.GetComponent<Rigidbody>(); }
            if (this.pickupMethod == GrabType.Parent && physicsBody == null)
            {
                this.physicsBody = this.pickupReference.gameObject.AddComponent<Rigidbody>();
                this.physicsBody.isKinematic = true;
                this.physicsBody.useGravity = false;
            }

            this.SaveRBParameters();
            this.SaveTransform();
        }

        protected virtual void Update()
        {
            if (!this.isInteractable && this.grabReference != null) { this.EndInteraction(null); } //end the interaction if the object is no longer interactable with.
        }


        #endregion Monobehaviour

    }



}