using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class DepthFirstSearch : MonoBehaviour
{
    public float speed = 1f;
    public Vector3 nextPosition;
    public GridManager gridManager;
    public List<Vector3> playerPath = new List<Vector3>();
    private PinkieTimer timerText;



    public void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        timerText = FindObjectOfType<PinkieTimer>();
    }

    public void Update()
    {

        if (playerPath.Count == 0)
        {

            Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;
            
            playerPath = FindPath(transform.position, playerPosition);
            
        }

        if (playerPath.Count > 0)
        {
            nextPosition = playerPath[0];
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, nextPosition) < 0.1f)
            {
                playerPath.RemoveAt(0);
            }
        }
    }

    public List<Vector3> FindPath(Vector3 start, Vector3 goal)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        List<Vector3> path = new List<Vector3>();

        Tile startTile = gridManager.GetTileAtPosition(start);
        Tile goalTile = gridManager.GetTileAtPosition(goal);

        if (startTile == null || goalTile == null || !startTile.walkable || !goalTile.walkable)
        {
            return path;
        }

        Stack<Vector3> stack = new Stack<Vector3>();
        stack.Push(start);

        HashSet<Vector3> visited = new HashSet<Vector3>(new Vector3EqualityComparer());
        visited.Add(start);

        Dictionary<Vector3, Vector3> cameFrom = new Dictionary<Vector3, Vector3>();

        while (stack.Count > 0)
        {
            Vector3 current = stack.Pop();

            if (Vector3.Distance(current, goal) < 0.1f) // Goal check with tolerance
            {
                while (cameFrom.ContainsKey(current))
                {
                    path.Insert(0, current);
                    current = cameFrom[current];
                }
                path.Insert(0, start);
                break;
            }

            foreach (Vector3 direction in new Vector3[]
            {
                new Vector3(0, 1, 0), // Up
                new Vector3(0, -1, 0), // Down
                new Vector3(-1, 0, 0), // Left
                new Vector3(1, 0, 0) // Right
            })
            {
                Vector3 neighbor = current + direction;

                Tile neighborTile = gridManager.GetTileAtPosition(neighbor);
                if (neighborTile != null && neighborTile.walkable && !visited.Contains(neighbor))
                {
                    stack.Push(neighbor);
                    visited.Add(neighbor);
                    cameFrom[neighbor] = current;
                }
            }
        }
        stopwatch.Stop();
        float pathFindingTime = stopwatch.ElapsedMilliseconds;
        timerText.ChangeTime(pathFindingTime);
        return path;
    }

    // Equality comparer for Vector3 to handle floating-point inaccuracies
    public class Vector3EqualityComparer : IEqualityComparer<Vector3>
    {
        public bool Equals(Vector3 a, Vector3 b)
        {
            return Vector3.Distance(a, b) < 0.1f;
        }

        public int GetHashCode(Vector3 obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}