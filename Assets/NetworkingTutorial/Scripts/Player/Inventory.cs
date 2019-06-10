using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    int bullets;
    [SerializeField]
    int granades;

    [Serializable]
    public class StartingBackpack
    {
        public static
        int bullet;
        public static
       int shield;
        public static
        int granades;

        public StartingBackpack(int bull, int shi, int nade)
        {
            bullet = bull;
            shield = shi;
            granades = nade;
        }

        public StartingBackpack bp = new StartingBackpack(100, 5, 5);
    }
}
