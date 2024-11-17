using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthFirstSearch : MonoBehaviour
{
    public float speed = 1f;
    private Vector3 nextPosition;
    private GridManager gridManager;
    private List<Vector3> playerPath = new List<Vector3>();

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>(); //finds the gridmanager in the scene

    }
    private void Update()
    {
        if (playerPath.Count == 0)
        {
            
            Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;
            playerPath = FindPath(transform.position, playerPosition);
        }

        if (playerPath.Count > 0)
        {
            //move the enemy to the next position
            nextPosition = playerPath[0];
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
            // checks if we've reached the next position
            if (Vector3.Distance(transform.position, nextPosition) < 0.01f)
            {
                playerPath.RemoveAt(0); //remove the reached position

            }
        }
    }
    private List<Vector3> FindPath(Vector3 start, Vector3 goal)
    {
        List<Vector3> path = new List<Vector3>();

        Tile startTile = gridManager.GetTileAtPosition(start);
        Tile goalTile = gridManager.GetTileAtPosition(goal);

        if (startTile == null || goalTile == null || !startTile.walkable || !goalTile.walkable)
        {
            return path; // return an empty path if either the start or goal is invalid
        }
        Stack<Vector3> stack = new Stack<Vector3>();
        stack.Push(start);

        HashSet<Vector3> visited = new HashSet<Vector3>();
        visited.Add(start);

        Dictionary<Vector3, Vector3> cameFrom = new Dictionary<Vector3, Vector3>();

        while (stack.Count > 0)
        {
            Vector3 current = stack.Pop();

            if (current == goal)
            {
                //reconstruct the path when goal is met
                while (cameFrom.ContainsKey(current))
                {
                    path.Insert(0, current); 
                    current = cameFrom[current];
                }
                path.Insert(0, start); // includes start position
                break;
            }
            //checks each direction
            foreach (Vector3 direction in new Vector3[] { Vector3.up, Vector3.down, Vector3.left, Vector3.right })
            {
                Vector3 neighbor = current + direction;

                Tile neighborTile = gridManager.GetTileAtPosition(neighbor);
                if (neighborTile != null && neighborTile.walkable && !visited.Contains(neighbor))
                {
                    stack.Push(neighbor);
                    visited.Add(neighbor);
                    cameFrom[neighbor] = current; //tracks path
                }
            }
        }
        return path; // returns the reconstructec path
    }
}