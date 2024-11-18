using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;

    [SerializeField] private Tile _tilePrefab;

    [SerializeField] private Transform _cam;
    private Tile[,] _grid;

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        _grid = new Tile[_width, _height]; // this creates a 2d array to store tile objects
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.coords = new Vector3(x,y);
                bool walkable = Random.value > 0.2f; //randomly determines if a tile is walkable
                var isOffset = ((x + y) % 2 == 1);
                spawnedTile.Init(isOffset,walkable); //call function in the Tile script
                _grid[x,y] = spawnedTile; // stores tiles in the array
            }
        }

        _cam.transform.position = new Vector3 ((float)_width/2 -0.5f, (float)_height / 2 - 0.5f, -10);

    }   
    //gets a tile at a specific position
    public Tile GetTileAtPosition(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.y);

        if (x>=0 && x<_width && y>=0 && y < _height)
        {
            return _grid[x, y];

        }
        //Debug.Log($"Out of bounds: ({x}, {y})");
        return null; // will return null if its out of bounds
    }

}
