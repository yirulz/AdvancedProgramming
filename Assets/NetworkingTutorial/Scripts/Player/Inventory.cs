using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    int bullets;
    int granades;

    [Serializable]
    public class StartingBackpack
    {
        [SerializeField]
        int bullet;
        [SerializeField]
        int shield;
        [SerializeField]
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
