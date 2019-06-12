using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory: MonoBehaviour
{
    [SerializeField]
    int bullets;
    [SerializeField]
    int granades;

    public StartingBackpack bp;

    public void Start()
    {
        bp = new StartingBackpack(100, 5, 5);
    }

    public void Update()
    {
        Debug.Log(bp.bullet + " " + bp.shield + " " + bp.granades);
    }

    public class StartingBackpack
    {

        public int bullet;
        public int shield;
        public int granades;

        public StartingBackpack(int bull, int shi, int nade)
        {
            bullet = bull;
            shield = shi;
            granades = nade;
        }

    }

}
