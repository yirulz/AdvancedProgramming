using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class LookScript : NetworkBehaviour
{
    //Sensitivity of mouse
    public float mouseSensitivity = 2.0f;

    //Min and Max Y Axis
    public float minimumY = -90f;
    public float maximumY = 90f;

    //Yaw of the camera
    private float yaw = 0f;
    //Pitch for the camera
    private float pitch = 0f;

    private GameObject mainCamera;
    [HideInInspector]
    //Rotation variable
    public float rotationX , rotationY;


    // Use this for initialization
    void Start()
    {
        //Lock cursor and diable mouse visiblity
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //Set camera componet
        Camera cam = GetComponentInChildren<Camera>();
        if(cam != null)
        {
            mainCamera = cam.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If instance of local player
        if (isLocalPlayer)
        {
            //Handle input
            HandleInput();
        }
    }

    void LateUpdate()
    {
        if (isLocalPlayer)
        {
            mainCamera.transform.localEulerAngles = new Vector3(-pitch, 0, 0);
        }
    }

    private void OnDestroy()
    {
        //Unlocks mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void HandleInput()
    {
        //Set yaw to rotation Y + MouseX * mouseSensitivity
        yaw = rotationY + Input.GetAxis("Mouse X") * mouseSensitivity;

        //Set pitch to pitch + mouseY * mouseSensitivity
        pitch += Input.GetAxis("Mouse Y") * mouseSensitivity;
        //clamp pitch
        pitch = Mathf.Clamp(pitch, minimumY, maximumY);
        //set rotationY to yaw
        rotationY = yaw;
        //rotate player camera left and right
        mainCamera.transform.parent.rotation = Quaternion.Euler(0, yaw * mouseSensitivity, 0);
    }
}
