using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper2D
{

    [RequireComponent(typeof(SpriteRenderer))]
    public class Tile : MonoBehaviour
    {
        public int x, y;
        public bool isMine = false;
        public bool isRevealed = false;
        public bool isFlagged = false;


        [Header("References")]
        public Sprite[] emptySprites;
        public Sprite[] mineSprites;
        public Sprite[] flags;

        private SpriteRenderer rend;

        void Awake()
        {
            rend = GetComponent<SpriteRenderer>();
           
        }
        // Use this for initialization
        void Start()
        {
            isMine = Random.value < 0.2f;
        }

        // Update is called once per frame
        void Update()
        {
           
        }
        //Create a function called Reveal for when game ends, it will take in an int, set minestate to 0 for later setting it for the minesprite array
        public void Reveal(int adjacentMines, int mineState = 0, int flagState = 2)
        {
            //Set bool of isRevealed to true
            isRevealed = true;
            //If the tile is a mine
            if(isMine)
            {
                //set sprite to mine sprite using mineState as 0
                rend.sprite = mineSprites[mineState];
                //If isFlagged bool is true
                if (isFlagged)
                {
                    //set rend's (sprite renderer) sprite selection to flags array of sprites with flagState (2) 
                    rend.sprite = flags[flagState];
                }
            }
            //else if not a mine
            else
            {
                //set rend's (sprite renderer) sprite selection to emptySprites array with adjacentMines (?) This number comes from the Grid script inputs
                rend.sprite = emptySprites[adjacentMines];
            }

           
        }
        //Flagged function
        public void Flag()
        {
            //If isFlagged bool = false
            isFlagged = !isFlagged;
            if (isFlagged)
            {
                //Sprite Renderer will be set to flags array of 0
                rend.sprite = flags[0];
            }
            //If isFlagged bool is true
            else
            {
                //Sprite Renderer will be set to flags array of 1
                rend.sprite = flags[1];
            }
        }
    }
}