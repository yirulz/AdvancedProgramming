using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseAI : MonoBehaviour
{
    public NavMeshAgent nav;
    public Rigidbody player;

    public AI red;

    [SerializeField]
    public GameObject aiPrefab;

    private float timer = 5;

    // Use this for initialization
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Rigidbody>();
        red = new AI(Color.red);
    }

    public virtual void Update()
    {
        UpdateLocation();
        SpawnEnemy();
    }

    public virtual void UpdateLocation()
    {
        if (!player)
        {
            player = FindObjectOfType<Rigidbody>();
        }
        else if (player)
        {
            nav.SetDestination(player.transform.position);
        }
    }

    void Cmd_SpawnEnemy()
    {
        Instantiate(aiPrefab, transform.position, transform.rotation);
    }

    void SpawnEnemy()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Cmd_SpawnEnemy();
            timer = 5f;
        }
    }


    #region Classes
    public class AI : BaseAI
    {
        public Color rend;


        public AI(Color color)
        {
            if (rend == null)
            {
                rend = GetComponent<Renderer>().material.color;
                rend = color;
            }
        }

    }

    #endregion

}

