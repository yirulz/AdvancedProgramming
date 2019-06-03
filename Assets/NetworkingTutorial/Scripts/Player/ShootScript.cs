using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShootScript : NetworkBehaviour
{
    //Set fire rate and range
    public float fireRate = 1f;
    public float range = 100f;
    //Layermask
    public LayerMask mask;
    //Timer for shooting
    private float fireFactor = 0f;

    // Update is called once per frame
    void Update()
    {
        //If is local player
        if(isLocalPlayer)
        {
            //Allow shooting
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
        //Ray cast forward
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1000))
        {
            //If raycast hit tag with Player
            if(hit.transform.CompareTag("Player") || hit.transform.CompareTag("Enemy"))
            {
                //Tell server name of player shot
                Cmd_PlayerShot(hit.transform.name);
            }
        }
    }

    void HandleInput()
    {
        //Count up fireFactor
        fireFactor += Time.deltaTime;
        fireRate = 1 / fireRate;

        if(fireFactor >= fireRate)
        {
            //If you left click
            if(Input.GetMouseButton(0))
            {
                //Shoot
                Shoot();
            }
        }
    }
}
