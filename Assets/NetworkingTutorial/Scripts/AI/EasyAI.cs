using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyAI : BaseAI
{
    //Override update as its virtual 
    public override void Update()
    {
        base.Update();

        if (timer <= 0)
        {
            if (range == 2)
            {
                //create instance of dog
                Humanoids.Orc orc = new Humanoids.Orc();
                //Set spawn dog parameters
                Cmd_SpawnOrc(orc.name, orc.health, orc.color);
                //reset timer
                timer = 2f;
            }
        }
    }

    public void Cmd_SpawnOrc(string aiName, int aiHealth, Color aiColor)
    {
        GameObject clone;
        //Create instance of orc varaibles
        Humanoids.Orc orc = new Humanoids.Orc();
        clone = Instantiate(easyAIPrefab, transform.position, transform.rotation);
        //Set clones script variables
        clone.transform.GetComponent<BaseAI>().aiName = aiName;
        clone.transform.GetComponent<BaseAI>().health = aiHealth;
        clone.transform.GetComponent<BaseAI>().color = aiColor;
        //Set clones name
        clone.name = aiName;
        //Set clones color
        clone.GetComponent<Renderer>().material.color = aiColor;
    }

    class Humanoids
    {
        public class Orc
        {
            public string name = "Orc";
            public int health = 125;
            public Color color = Color.red;
        }
    }
}
