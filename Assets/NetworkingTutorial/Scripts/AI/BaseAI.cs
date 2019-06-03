using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseAI : MonoBehaviour
{
    public NavMeshAgent nav;
    public Rigidbody player;

    // Use this for initialization
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Rigidbody>();
    }

    public void Update()
    {
        UpdateLocation();
    }

    public virtual void UpdateLocation()
    {
        if (!player)
        {
            player = FindObjectOfType<Rigidbody>();
        }
        else if(player)
        {
            nav.SetDestination(player.transform.position);
        }
    }

    public class FindPlayer : BaseAI
    {
        public override void UpdateLocation()
        {
            
        }
    }
}
