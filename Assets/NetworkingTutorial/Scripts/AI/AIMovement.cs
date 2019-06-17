using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    #region References
    public NavMeshAgent nav;
    public Rigidbody player;
    #endregion

    void Start()
    {
        //Get navmesh agent
        nav = GetComponent<NavMeshAgent>();
        //Find player
        player = FindObjectOfType<Rigidbody>();
    }

    void Update()
    {
        UpdateLocation();
    }

    public virtual void UpdateLocation()
    {
       
        //If player isnt assigned
        if (!player)
        {
            //Find player with ridgid body
            player = FindObjectOfType<Rigidbody>();
        }
        //If there is a player
        else if (player)
        {
            //Move AI towards player
            nav.SetDestination(player.transform.position);
        }
    }
}
