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

    HashSet<Vector2> visited = new HashSet<Vector2>();
    
    Dictionary<Vector2, List<Vector2>> parent = new Dictionary<Vector2, List<Vector2>>();
    Vector2 currentpos;
    Vector2 targetpos;


    // sets the player pos and target pos
    public void SetNewDestination(Vector2 startcoordinates, Vector2 targetcoordinates)
    {
        currentpos = startcoordinates;
        targetpos = targetcoordinates;
    }
    // Generates the path
    public List<Vector2> GetNewPath(Vector2 current)
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
        
        bool isRunning = true;

        // queue the current position and mark as visited
        frontier.Enqueue(current);
        visited.Add(current);

        // explore all neighbors (it looks bad but I get an error otherwise)
        while (isRunning)
        {
            // dequeue a neighbor
            currentpos = frontier.Dequeue();
            ExploreNeighbors();
            if (currentpos == targetpos)
            {

                isRunning = false;
            }
        }

    }
    public void ExploreNeighbors()
    {
        // list of directions
        List<Vector2> directions = new List<Vector2>
        {
            new Vector2(0, 0.5f),  // Up
            new Vector2(0.25f, 0),  // Right
            new Vector2(0, -0.5f),  // Down
            new Vector2(-0.25f, 0), // Left
        };
        
        // loop through each direction
        foreach (Vector2 direction in directions)
        {
            // create a list to hold all neighbors
            List<Vector2> neighbors = new List<Vector2>();

            // create a neighbor by adding a direction to it
            Vector2 neighbor = currentpos + direction;
            
            // if not visited add to neighbors, visited, the queue, and update the path
            if (!visited.Contains(neighbor))
            {

                neighbors.Add(neighbor);
                visited.Add(neighbor);
                frontier.Enqueue(neighbor);
                parent[currentpos] = neighbors;

            }
        }

    }

    List<Vector2> BuildPath()
    { 
        List<Vector2> path = new List<Vector2>();

        // grab the path and reverse it beacuase it builds it backwards
        path = parent[targetpos];   
        path.Reverse();
        return path;
    }
}
