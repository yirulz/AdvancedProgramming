using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace NetworkGame
{
    public class SyncTransform : NetworkBehaviour
    {
        //Speed of lerping rotation and position
        public float lerpRate = 15;

        //Threshold for when to send commands
        public float positionThreshold = 0.5f;
        public float rotationThreshold = 5.0f;

        //Records the previous position and rotation that was sent to the server
        private Vector3 lastPosition;
        private Quaternion lastRotation;

        //Vars to be synced across the network
        [SyncVar] private Vector3 syncPosition;
        [SyncVar] private Quaternion syncRotation;

        //Set RigidBody to rigid
        private Rigidbody rigid;


        void Start()
        {
            rigid = GetComponent<Rigidbody>();
        }

   
        void Update()
        {

        }

        void FixedUpdate()
        {
            TransmitPosition();
            LerpPosition();

            TransmitRotation();
            LerpRotation();
        }

        void LerpPosition()
        {
            if(!isLocalPlayer)
            {
                rigid.position = Vector3.Lerp(rigid.position, syncPosition, Time.deltaTime * lerpRate);
            }
        }

        void LerpRotation()
        {
            if(!isLocalPlayer)
            {
                rigid.rotation = Quaternion.Lerp(rigid.rotation, syncRotation, Time.deltaTime * lerpRate);
            }
        }
        

        [Command] void CmdSendPositionToServer(Vector3 _position)
        {
            syncPosition = _position;
            Debug.Log("Position Command");
        }

        [Command] void CmdSendRotationToServer(Quaternion _rotation)
        {
            syncRotation = _rotation;
            Debug.Log("Rotation Command");
        }

        [ClientCallback] void TransmitPosition()
        {
            if(isLocalPlayer && Vector3.Distance(rigid.position, lastPosition) > positionThreshold)
            {
                CmdSendPositionToServer(rigid.position);
                lastPosition = rigid.position;
            }
        }

        [ClientCallback] void TransmitRotation()
        {
            if(isLocalPlayer && Quaternion.Angle(rigid.rotation, lastRotation) > rotationThreshold)
            {
                CmdSendRotationToServer(rigid.rotation);
                lastRotation = rigid.rotation;
                
            }
        }
    }
}