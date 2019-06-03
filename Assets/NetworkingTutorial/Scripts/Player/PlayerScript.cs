using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace NetworkGame
{
    public class PlayerScript : NetworkBehaviour
    {
        #region Variables
        public float movementSpeed = 10f;
        public float rotationSpeed = 10f;
        public float jumpHeight = 2f;
        private bool isGrounded = false;
        #endregion

        private Rigidbody rigid;
        // Use this for initialization
        void Start()
        {
            //Set rigid as RigidBody component
            rigid = GetComponent<Rigidbody>();  
            //Get Audio Listener from Camera
            AudioListener audioListener = GetComponentInChildren<AudioListener>();
            //Get camera from scene
            Camera camera = GetComponentInChildren<Camera>();

            //If instance is local player
            if(isLocalPlayer)
            {
                //enable camera and audio listener
                camera.enabled = true;
                audioListener.enabled = true;
            }

            else
            {
                //disable camera and audio listener
                camera.enabled = false;
                audioListener.enabled = false;
            }
        }

        // Update is called once per frame
        void Update()
        {
            //Check if player is local player (prevents other people controlling your character)
            if (isLocalPlayer)
            {
                //Allows for key press to have effect via keycodes
                HandleInput();
            }
        }

        void Move(KeyCode _key)
        {
            //Vector 3 position as rigid's position
            Vector3 position = rigid.position;
            //Quaternion rotation as rigid's rotation
            Quaternion rotation = rigid.rotation;

            switch (_key)
            {
                //If W is pressed, position moves forward at the movement speed
                case KeyCode.W:
                    position += transform.forward * movementSpeed * Time.deltaTime;
                    break;
                //If S is pressed, position moves backwards at the movement speed
                case KeyCode.S:
                    position -= transform.forward * movementSpeed * Time.deltaTime;
                    break;
                //If A is pressed, rotate with Quaternion.AngleAxis at rotation speed
                case KeyCode.A:
                    position += -transform.right * movementSpeed * Time.deltaTime;
                    break;
                //If D is pressed, rotate with Quaternion.AngleAxis at rotation speed
                case KeyCode.D:
                    position += transform.right * movementSpeed * Time.deltaTime;
                    break;
                //If space is press
                case KeyCode.Space:
                    //Check if grounded
                    if(isGrounded)
                    {
                        //Add force to rigid making it go up at the rate of jump speed. Forcemode.impluse to reach max jump speed
                        rigid.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                        isGrounded = false;
                    }
                    break;
            }
            
            //Move rigid position
            rigid.MovePosition(position);
            //Move rigid rotation
            rigid.MoveRotation(rotation);
        }

        void HandleInput()
        {
            KeyCode[] keys =
            {
                KeyCode.W,
                KeyCode.S,
                KeyCode.A,
                KeyCode.D,
                KeyCode.Space
            };

            foreach (var key in keys)
            {
                //If any key with a KeyCode is pressed
                if (Input.GetKey(key))
                {
                    //Move according to Key you pressed 
                    Move(key);
                }
            }
        }

        //Set grounded to true when entering any collision
        void OnCollisionEnter(Collision _col)
        {
            isGrounded = true;
        }
    }
}