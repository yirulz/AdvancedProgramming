using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShootScript : NetworkBehaviour
{


    public float fireRate = 1f;

    public float range = 100f;

    public LayerMask mask;


    private float fireFactor = 0f;

    private GameObject mainCamera;

    // Use this for initialization
    void Start()
    {
        mainCamera = GetComponentInChildren<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer)
        {
            HandleInput();
        }
    }

    [Command]
    void Cmd_PlayerShot(string _id)
    {
        Debug.Log("Player" + _id + "has been shot!");
    }

    [Client]
    void Shoot()
    {
        RaycastHit hit;

        Debug.Log("Shooting");

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1000))
        {
            if(hit.transform.CompareTag("player"))
            {
                Cmd_PlayerShot(hit.transform.name);
            }
        }
    }

    void HandleInput()
    {
        fireFactor += Time.deltaTime;
        fireRate = 1 / fireRate;

        if(fireFactor >= fireRate)
        {
            if(Input.GetMouseButton(0))
            {
                Shoot();
            }
        }
    }

}
