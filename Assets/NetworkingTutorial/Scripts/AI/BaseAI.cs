using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseAI : MonoBehaviour
{
    #region Variables

    public string aiName;
    public int health;
    public Color color;
    public int range;
    [HideInInspector]
    public float timer = 2;
    #endregion

    #region Refernces
    public GameObject baseAIPrefab;
    public GameObject easyAIPrefab;
    public Transform spawnLocation;

    #endregion

    public virtual void Update()
    {
        SpawnerFunction();
    }

    public void SpawnerFunction()
    {
        //Only allow spawning if object is a spawner
        //If object is named spawner
        if (gameObject.name.Contains("Spawner"))
        {
            //run down spawn timer
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                //randomise spawn
                range = Random.Range(0, 3);
                Debug.Log(range);
                if (range == 0)
                {
                    //create instance of dog
                    Animals.Dog dog = new Animals.Dog();
                    //Set spawn dog parameters
                    Cmd_SpawnDog(dog.name, dog.health, dog.color);
                    //reset timer
                    timer = 2f;
                }
                else if (range == 1)
                {
                    //create instance of cat
                    Animals.Cat cat = new Animals.Cat();
                    //Set spawn cat parameters
                    Cmd_SpawnCat(cat.name, cat.health, cat.color);
                    //reset timer
                    timer = 2f;
                }
            }
        }
    }
    //Use Cmd to allow server side spawning
    public void Cmd_SpawnDog(string aiName, int aiHealth, Color aiColor)
    {
        GameObject clone;
        //create instance of dog variable, allows access to variables
        Animals.Dog dog = new Animals.Dog();
        clone = Instantiate(baseAIPrefab, transform.position, transform.rotation);
        //Set clone varaibles to that of a dogs for script
        clone.transform.GetComponent<BaseAI>().aiName = aiName;
        clone.transform.GetComponent<BaseAI>().health = aiHealth;
        clone.transform.GetComponent<BaseAI>().color = aiColor;
        //Set clones name
        clone.name = aiName;
        //set clone color
        clone.GetComponent<Renderer>().material.color = dog.color;
    }

    public void Cmd_SpawnCat(string aiName, int aiHealth, Color aiColor)
    {
        GameObject clone;
        Animals.Cat cat = new Animals.Cat();
        clone = Instantiate(baseAIPrefab, transform.position, transform.rotation);

        clone.transform.GetComponent<BaseAI>().aiName = aiName;
        clone.transform.GetComponent<BaseAI>().health = aiHealth;
        clone.transform.GetComponent<BaseAI>().color = aiColor;

        clone.name = aiName;
        clone.GetComponent<Renderer>().material.color = aiColor;
    }
    class Animals
    {
        public float speed;

        public class Dog:Animals
        {
            //Set dogs variables
            public string name = "Dog";
            public int health = 25;
            public Color color = Color.green;
        }
        public class Cat:Animals
        {
            //set cats variables
            public string name = "Cat";
            public int health = 15;
            public Color color = Color.blue;
        }
    }


}
