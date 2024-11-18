using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BreadthSearch : MonoBehaviour
{
    Queue<Vector2> frontier = new Queue<Vector2>();
    HashSet<Tile> visited = new HashSet<Tile>();
    Dictionary<Vector2, List<Vector2>> parent = new Dictionary<Vector2, List<Vector2>>();
    Vector2 currentpos;
    Vector2 targetpos;
    GridManager grid;

    List<Vector2> directions = new List<Vector2>
    {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right,
    };

    // sets the player pos and target pos
    public void Awake()
    {
        grid = FindObjectOfType<GridManager>();
    }

    public void SetNewDestination(Vector2 startcoordinates, Vector2 targetcoordinates)
    {
        currentpos = startcoordinates;
        targetpos = targetcoordinates;
    }

    // Generates the path
    public Dictionary<Vector2, List<Vector2>> GetNewPath(Vector2 current)
    {
        BreadthFirstSearch(current);
        return BuildPath();
    }

    // the breadth search algorithm
    public void BreadthFirstSearch(Vector2 current)
    {
        // clear the queue, visited, and path
        frontier.Clear();
        visited.Clear();
        parent.Clear();

        // queue the current position and mark as visited
        frontier.Enqueue(current);
        visited.Add(grid.GetTileAtPosition(current));

        // Initialize the path for the start position
        parent[current] = new List<Vector2> { current };

        while (frontier.Count > 0)
        {
            // dequeue a neighbor
            currentpos = frontier.Dequeue();
            ExploreNeighbors();
        }
    }

    public void ExploreNeighbors()
    {
        List<Tile> neighbors = new List<Tile>();

        // loop through each direction
        foreach (Vector2 direction in directions)
        {
            // create a neighbor by adding a direction to it
            Vector2 neighbor = currentpos + direction;
            Tile neighborTile = grid.GetTileAtPosition(neighbor);

            if (neighborTile == null || !neighborTile.walkable)
            {
                continue;
            }

            neighbors.Add(neighborTile);
        }

        foreach (Tile neighbor in neighbors)
        {
            // If not visited, add to visited, enqueue and update the parent
            if (!visited.Contains(neighbor))
            {
                visited.Add(neighbor);
                frontier.Enqueue(neighbor.coords);

                // Record the full path to the current Tile
                List<Vector2> pathToNeighbor = new List<Vector2>(parent[currentpos]);
                pathToNeighbor.Add(neighbor.coords);
                parent[neighbor.coords] = pathToNeighbor;
            }
        }
    }

    // Builds the full path to the target, if it exists
    Dictionary<Vector2, List<Vector2>> BuildPath()
    {
        Dictionary<Vector2, List<Vector2>> result = new Dictionary<Vector2, List<Vector2>>();

        // If the target exists in the parent dictionary, return the path
        if (parent.ContainsKey(targetpos))
        {
            List<Vector2> fullPath = parent[targetpos];
            result[targetpos] = fullPath;
        }
        else
        {
            // If no path found, return an empty dictionary
            result[targetpos] = new List<Vector2>();
        }

        return result;
    }
}

