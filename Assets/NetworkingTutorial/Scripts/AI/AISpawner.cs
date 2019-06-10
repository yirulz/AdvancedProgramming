using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AISpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject aiPrefab;

    private float timer = 5;

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Cmd_SpawnEnemy();
            timer = 5f;
        }
    }
    void Cmd_SpawnEnemy()
    {
        Instantiate(aiPrefab, transform.position, transform.rotation);
    }


}
