using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Minesweeper3D
{
    public class Tile : MonoBehaviour
    {
        public int x, y, z;
        public bool isMine = false;
        public bool isRevealed = false;
        //Limits the range between two numbers
        [Range(0, 1)]
        public float mineChance = 0.15f;
        public GameObject minePrefab;
        public GameObject textPrefab;

        private Animator anim;
        private GameObject mine;
        private GameObject text;
        private Collider col;

        private TextMeshPro textElement;

        private void Awake()
        {
            //Get reference for animator
            anim = GetComponent<Animator>();

            col = GetComponent<Collider>();
        }
        // Use this for initialization
        void Start()
        {
            //Set mine chance to a random value lower than mineChance
            isMine = Random.value < mineChance;
            if(isMine)
            {
                //create instance of mine object
                mine = Instantiate(minePrefab, transform);
                mine.SetActive(false);
            }
            else
            {
                text = Instantiate(textPrefab, transform);
                text.SetActive(false);
            }
        }

        public void Reveal(int adjacentMines, int mineState = 0)
        {
            //Flags the tile as being releaved
            isRevealed = true;
            //Run reveal animation
            anim.SetTrigger("Reveal");

            col.enabled = false;
            //Check if tile is mine
            if (isMine)
            {
                mine.SetActive(true);
            }
            else
            {
                //Enabling text
                text.SetActive(true);
                //Setting text
                text.GetComponent<TextMeshPro>().text = adjacentMines.ToString();
            }
        }

        private void OnMouseDown()
        {
            Reveal(10);
        }

    }
}