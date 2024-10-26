using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class BreadthSearch : MonoBehaviour
{
    
    public GameObject inky;
    public GameObject target;
    public int length;
    public int width;

    Queue<Vector2Int> frontier = new Queue<Vector2Int>();
 
    HashSet<Vector2Int> visited = new HashSet<Vector2Int>();

    public void BreadthFirstSearch()
    {
        frontier.Clear();
        visited.Clear();
        Vector2Int currentpos = new Vector2Int((int)Mathf.Round(inky.transform.position.x), (int)Mathf.Round(inky.transform.position.y));
        Vector2Int targetpos = new Vector2Int((int)Mathf.Round(target.transform.position.x), (int)Mathf.Round(target.transform.position.y));

        while (currentpos != targetpos)
        {
            ExploreNeighbors(currentpos);
        }

    }
    public void ExploreNeighbors(Vector2Int start)
    {
        frontier.Enqueue(start);
        visited.Add(start);

        List<Vector2Int> directions = new List<Vector2Int>
        {
            new Vector2Int(0, 1),  // Up
            new Vector2Int(1, 0),  // Right
            new Vector2Int(0, -1),  // Down
            new Vector2Int(-1, 0), // Left
        };


        while (frontier.Count <= length*width)
        {
            Vector2Int current = frontier.Dequeue();
            Debug.Log($"{current.x}, {current.y}");
            foreach (var direction in directions)
            {
                Vector2Int neighbor = current + direction;
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    frontier.Enqueue(neighbor);
                }
            }
        }
    }
}
