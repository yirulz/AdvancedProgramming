﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper3D
{
    public class Grid : MonoBehaviour
    {
        public GameObject tilePrefab;
        public int width = 10, height = 10, depth = 10;
        public float spacing = 0.15f;
        //3D array
        private Tile[,,] tiles;

        // Use this for initialization
        void Start()
        {
            GenerateTiles();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void GenerateTiles()
        {
            //Instantiate the new 3D array of size width x height x depth
            tiles = new Tile[width, height, depth];
            //Store half the size of the grid
            Vector3 halfSize = new Vector3(width * 0.5f, height * 0.5f, depth * 0.5f);
            //Offset
            Vector3 offset = new Vector3(0.5f, 0.5f, 0.5f);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        //Generate position for current tile
                        Vector3 position = new Vector3(x - halfSize.x, y - halfSize.y, z - halfSize.z);
                        //Apply offset
                        position += offset;
                        //Apply spacing
                        position *= spacing;

                        //Spawn tile
                        Tile newTile = SpawnTile(position);

                        //Store coordinates
                        newTile.x = x;
                        newTile.y = y;
                        newTile.z = z;

                        //Store tile in array at those coordinates
                        tiles[x, y, z] = newTile;
                    }
                }
            }
        }

        Tile SpawnTile(Vector3 position)
        {
            //Clone tile prefab
            GameObject clone = Instantiate(tilePrefab);
            //Set its positoin
            clone.transform.position = position;
            //Return the Tile component of the clone
            return clone.GetComponent<Tile>();
        }
    }
}