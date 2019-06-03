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

        //SyncVar to be synced across the network
        [SyncVar] private Vector3 syncPosition;
        [SyncVar] private Quaternion syncRotation;

        //Set RigidBody to rigid
        private Rigidbody rigid;

        void Start()
        {
            //Get rigidbody component
            rigid = GetComponent<Rigidbody>();
        }
  
        void FixedUpdate()
        {
            //Transmit position and lerp position
            TransmitPosition();
            LerpPosition();
            //transmit rotation and lerp rotation
            TransmitRotation();
            LerpRotation();
        }

        void LerpPosition()
        {
            //If not local player
            if(!isLocalPlayer)
            {
                //Update position with lerp
                rigid.position = Vector3.Lerp(rigid.position, syncPosition, Time.deltaTime * lerpRate);
            }
        }

        void LerpRotation()
        {
            //If not local player
            if(!isLocalPlayer)
            {
                //Update rotation with lerp
                rigid.rotation = Quaternion.Lerp(rigid.rotation, syncRotation, Time.deltaTime * lerpRate);
            }
        }
        //Allow for server to receive player position
        [Command] void CmdSendPositionToServer(Vector3 _position)
        {
            syncPosition = _position;
            Debug.Log("Position Command");
        }
        //Allow for server to receive player rotation
        [Command] void CmdSendRotationToServer(Quaternion _rotation)
        {
            syncRotation = _rotation;
            Debug.Log("Rotation Command");
        }
        //Send position to server
        [ClientCallback] void TransmitPosition()
        {
            //Check if isLocalPlayer and if distance is greater than positionThreshold
            if(isLocalPlayer && Vector3.Distance(rigid.position, lastPosition) > positionThreshold)
            {
                //Send your position
                CmdSendPositionToServer(rigid.position);
                //Update your last position
                lastPosition = rigid.position;
            }
        }
        //Send rotation to server
        [ClientCallback] void TransmitRotation()
        {
            //Check if isLocalPlayer and if your rotation is greater than rotationThreshold
            if (isLocalPlayer && Quaternion.Angle(rigid.rotation, lastRotation) > rotationThreshold)
            {
                //Send your rotation
                CmdSendRotationToServer(rigid.rotation);
                //Update your last rotation
                lastRotation = rigid.rotation;
                
            }
        }
    }
}