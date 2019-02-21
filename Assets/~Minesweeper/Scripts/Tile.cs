using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper
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

        public void Reveal(int adjacentMines, int mineState = 0, int flagState = 2)
        {
            isRevealed = true;

            if(isMine)
            {
                rend.sprite = mineSprites[mineState];
                if (isFlagged)
                {
                    rend.sprite = flags[flagState];
                }
            }
            else
            {
                rend.sprite = emptySprites[adjacentMines];
            }

           
        }

        public void Flag()
        {
            isFlagged = !isFlagged;
            if (isFlagged)
            {
                rend.sprite = flags[0];
            }
            else
            {
                rend.sprite = flags[1];
            }
        }
    }
}