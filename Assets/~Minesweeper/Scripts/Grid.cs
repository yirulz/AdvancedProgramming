using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper
{

    public class Grid : MonoBehaviour
    {
        public GameObject tilePrefab;
        public int width = 10, height = 10;
        public float spacing = .155f;

        //[,] Two dimentional array
        private Tile[,] tiles;
       

        // Use this for initialization
        void Start()
        {
            //Run the generate tiles function as soon as you start
            GenerateTiles();
        }

        // Update is called once per frame
        void Update()
        {
            //If you left click
            if(Input.GetMouseButtonDown(0))
            {
                //Cast a ray to where camera is pointing at the screen in the position where the mouse is
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //Return things hit with raycast on 2D objects
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                //If raycast hit a 2D object with a collider
                if(hit.collider != null)
                {
                    //Refering to the Tile script, get the information of that tile
                    Tile hitTile = hit.collider.GetComponent<Tile>();
                    //If that tile the raycast hit is a tile
                    if(hitTile != null)
                    {
                        //Run the SelectTile function with the hitTile (Tile we just hit)
                        SelectTile(hitTile);
                    }
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit.collider != null)
                {
                    Tile hitTile = hit.collider.GetComponent<Tile>();
                    if (hitTile != null)
                    {
                        hitTile.Flag();
                    }
                }
            }
        }


        //Creating a function into the Tile script called spawnTile that takes in a Vector3 pos
        Tile SpawnTile(Vector3 pos)
        {

            //Set a Gameobject called clone to instantiate from the tilePreFab from unity
            GameObject clone = Instantiate(tilePrefab);

            //Set the instantiated prefab's(clone)postition to pos
            clone.transform.position = pos;

            //
            Tile currentTile = clone.GetComponent<Tile>();

            //return currentTile
            return currentTile;
        }

        void GenerateTiles()
        {
            //Create a new Tile with the 3D array taking in width and height
            tiles = new Tile[width, height];

            //forward loop for x which will be used for width
            for (int x = 0; x < width; x++)
            {
                //forward loop for y which will be used for height
                for (int y = 0;  y < height;  y++)
                {
                    //Create a new vector2 called halfsize, it will half the width and height
                    Vector2 halfsize = new Vector2(width * 0.5f, height * 0.5f);
                    //Create a new Vector2 called pos and it will be x(width) - halfsize.x (half of width), y(height) - halfsize.y (half of height)
                    Vector2 pos = new Vector2(x - halfsize.x, y - halfsize.y);
                    //New Vector2 called offset at 0.5f, 0.5f, this will offset the tiles pos
                    Vector2 offset = new Vector2(0.5f, 0.5f);
                    
                    pos += offset;

                    pos *= spacing;
                    //Calling on the Tile script (tile variable name) will be assigned the SpawnTile function (made in this script) taking in the pos set above
                    Tile tile = SpawnTile(pos);
                    //Spawntile will set parent to grid
                    tile.transform.SetParent(transform);
                    //Store the tile x and y coordinates shortered to (x, y)
                    tile.x = x;
                    tile.y = y;
                    //Store tile in array with the coordinates or tile.x and tile.y
                    tiles[x, y] = tile;

                }
            }
        }

        public int GetAdjacentMinecount(Tile tile)
        {
            int count = 0;

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int desiredX = tile.x + x;
                    int desiredY = tile.y + y;

                    if(desiredX < 0 || desiredX >= width || desiredY < 0 || desiredY >= height)
                    {
                        continue;
                    }

                    Tile currentTile = tiles[desiredX, desiredY];

                    if(currentTile.isMine)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        void SelectATile()
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);

            if(hit.collider != null)
            {
                Tile hitTile = hit.collider.GetComponent<Tile>();

                if(hitTile != null)
                {
                    int adjacentMines = GetAdjacentMinecount(hitTile);

                    hitTile.Reveal(adjacentMines);
                }
            }
        }

        void FFuncover(int x, int y, bool [,] visited)
        {
            if(x >= 0 && y >= 0 && x < width && y < height)
            {
                if(visited [x,y])
                
                    return;
                

                Tile tile = tiles[x, y];

                int adjacentMines = GetAdjacentMinecount(tile);

                tile.Reveal(adjacentMines);

                if(adjacentMines == 0)
                {
                    visited[x, y] = true;

                    FFuncover(x - 1, y, visited);
                    FFuncover(x + 1, y, visited);
                    FFuncover(x, y - 1, visited);
                    FFuncover(x, y + 1, visited);
                }
            }
        }

        void UncoverMines(int mineState = 0)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Tile tile = tiles[x, y];

                    if(tile.isMine)
                    {
                        int adjacentMines = GetAdjacentMinecount(tile);
                        tile.Reveal(adjacentMines, mineState);
;
                    }
                }
            }
        }

        bool NoMoreEmptyTiles()
        {
            int emptyTileCount = 0;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Tile tile = tiles[x, y];

                    if (!tile.isRevealed && !tile.isMine)
                    {
                        emptyTileCount += 1;
                    }
                }
            }

            return emptyTileCount == 0;

        }

        void SelectTile(Tile selected)
        {
            int adjacentMines = GetAdjacentMinecount(selected);
            selected.Reveal(adjacentMines);

            if (selected.isMine)
            {
                UncoverMines();
            }

            else if (adjacentMines == 0)
            {
                int x = selected.x;
                int y = selected.y;

                FFuncover(x, y, new bool[width, height]);
            }

            if(NoMoreEmptyTiles())
            {
                UncoverMines(1);
            }
        }
    }
}