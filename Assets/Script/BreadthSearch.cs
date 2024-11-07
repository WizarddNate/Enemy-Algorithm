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
    Queue<Vector2Int> frontier = new Queue<Vector2Int>();

    HashSet<Vector2Int> visited = new HashSet<Vector2Int>();

    Dictionary<Vector2Int, List<Vector2Int>> parent = new Dictionary<Vector2Int, List<Vector2Int>>();
    Vector2Int currentpos;
    Vector2Int targetpos;

    public void SetNewDestination(Vector2Int startcoordinates, Vector2Int targetcoordinates)
    {
        currentpos = startcoordinates;
        targetpos = targetcoordinates;
    }
    public List<Vector2Int> GetNewPath(Vector2Int current)
    {
        BreadthFirstSearch(current);
        return BuildPath();
        
    }

    public void BreadthFirstSearch(Vector2Int current)
    {
 
        frontier.Clear();
        visited.Clear();
        
        bool isRunning = true;

        frontier.Enqueue(current);
        visited.Add(current);

        while (isRunning)
        {
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

        List<Vector2Int> directions = new List<Vector2Int>
        {
            new Vector2Int(0, -1),  // Down
            new Vector2Int(-1, 0), // Left
            new Vector2Int(0, 1),  // Up
            new Vector2Int(1, 0),  // Right
            
        };
        
        foreach (Vector2Int direction in directions)
        {
            List<Vector2Int> neighbors = new List<Vector2Int>();
            Vector2Int neighbor = currentpos + direction;
            if (!visited.Contains(neighbor))
            {

                neighbors.Add(neighbor);
                visited.Add(neighbor);
                frontier.Enqueue(neighbor);
                parent[currentpos] = neighbors;

            }
        }

    }

    List<Vector2Int> BuildPath()
    { 
        List<Vector2Int> path = new List<Vector2Int>();
        path = parent[targetpos];   
        path.Reverse();
        return path;
    }
}
