using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.PlayerLoop;

[System.Serializable]
public class AStarNode //creating a node for each tile that the A* algoritmn will loop through
{
    //list to know the nodes adjacent to each node
    public List<int> adjacentPointIDs = new List<int>();

    public AStarNode predecessorNode = null;

    //Distance to nearest block
    public float g;

    //Predicted distance to final goal
    public float h;

    //Both added togther
    public float f => g + h;

    public Vector3 position = new Vector3(0, 0, 0);

}

public class AStar : MonoBehaviour //search algorthmn. Mono Behavior allows this to be a component
{
    public Transform player; //player object
    public List<AStarNode> graph; //graph 
    public float speed = 1.0f;

    public List<Vector3> points;// = new List<Vector3>();

    private BlinkyTimer timerUpdate; //stopwatch

    private void Start()
    {
        timerUpdate = FindObjectOfType<BlinkyTimer>(); //???

        GridManager gridManager = GameObject.FindObjectOfType<GridManager>();

        //start stopwatch 


        foreach (Tile tile in gridManager.grid) //For loop, but instead of iterating through a varible 'x', you iterate through the tiles in the grid array
        {
            if (tile.walkable == true)
            {
                AddPoint(tile.coords);
            }
        }
        
        //looks big and scary, but this is just the process of making a node for each point on the graph
        float z = 0.0f; //z should not be involved in the process as this is a 2D grid
        foreach (AStarNode aStarNode in graph)
        {
            float x = aStarNode.position.x;
            float y = aStarNode.position.y;
            if (ValidPoint(new Vector3(x, y, z)))
            {
                for (int xOffset = -1; xOffset < 2; xOffset++)
                {
                    for (int yOffset = -1; yOffset < 2; yOffset++)
                    {
                        if (ValidPoint(new Vector3(x + xOffset, y + yOffset, z)))
                        {
                            if (new Vector3(x + xOffset, y + yOffset, z) == Vector3.zero)
                                continue; //keeps the for loop from looping on the origin
                            if (xOffset == 0.0f || yOffset == 0.0f)
                            {
                                ConnectPoints(
                                    GetPointByPosition(new Vector3(x + xOffset, y + yOffset, z)),
                                    GetPointByPosition(new Vector3(x, y, z)));
                            }
                        }
                    }
                }
            }

        }
    }

    void FixedUpdate()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        foreach (AStarNode aStarNode in graph)
        {
            aStarNode.g = 0;
            aStarNode.h = 0;
            aStarNode.predecessorNode = null;
        }

        uint c = GetClosestPoint(new Vector3(transform.position.x, transform.position.y, 0.0f)); //c for current index 
        uint p = GetClosestPoint(new Vector3(player.position.x, player.position.y, 0.0f)); //p is for player 

        
        
        points = GetPath(c, p);

        stopwatch.Stop();
        float pathFindingTime = stopwatch.ElapsedMilliseconds;
        timerUpdate.ChangeTime(pathFindingTime);
        //UnityEngine.Debug.Log($"A {pathFindingTime:}");

        if (points.Count > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[points.Count-1], Time.fixedDeltaTime * speed);
        }
    }

    public uint AddPoint(Vector3 position) //unsigned ints can't be negative! Cool!
    {
        AStarNode node = new AStarNode();
        node.position = position;
        graph.Add(node);
        return (uint)graph.Count - 1; //do not cast a negative number as uint, it'll become a massive number and hurt you

    }
    public uint GetClosestPoint(Vector3 position) //allow each point to see the closest point to them
    {
        uint id = 0;
        float minDistance = float.MaxValue; //set float to its largest value
        float distance = 0.0f;

        for (int i = 0; i < graph.Count; i++)
        {
            distance = Vector3.Distance(position, graph[i].position);

            if (minDistance > distance)
            {
                id = (uint)i;
                minDistance = distance;
            }

        }
        //end stopwatch

        
        return id;
    }

    public uint GetPointByPosition(Vector3 position)
    {
        for (int i = 1; i < graph.Count; i++)
        {
            if (graph[i].position == position)
                return (uint)i;
        }
        return 0;
    }

    void ConnectPoints(uint idFrom, uint idTo)
    {
        if (idTo == idFrom)
        {
            return;
        }

        if (graph[(int)idFrom].adjacentPointIDs.Contains((int)idTo) == false)
        {
            graph[(int)idFrom].adjacentPointIDs.Add((int)idTo);
        }
        if (graph[(int)idTo].adjacentPointIDs.Contains((int)idFrom) == false)
        {
            graph[(int)idTo].adjacentPointIDs.Add((int)idFrom);
        }
    }

    bool ValidPoint(Vector3 position)
    {
        for (int i = 1; i < graph.Count; i++)
        {
            if (graph[i].position == position)
                return true;
        }
        return false;
    }

    List<Vector3> BuildPath(AStarNode node) //builds a path out of each node
    {
        List<Vector3> path = new List<Vector3>();
        AStarNode currentNode = node;

        while (currentNode.predecessorNode != null)
        {
            path.Add(currentNode.position);
            currentNode = currentNode.predecessorNode;
        }
        return path;
    }


    List<Vector3> GetPath(uint idFrom, uint idTo)
    {
        

        List<uint> searchingSet = new List<uint>(); //stores indexes into the graph
        List<uint> hasSearchedSet = new List<uint>();
        uint graphNodeIndex;

        searchingSet.Add(idFrom);

        int MAXCOUNTER = 500; //keeps the while loop from infinetly searching. This shouldnt ever be needed, it's just a safeguard. 
        int counter = 0;

        while (searchingSet.Count > 0 && counter < MAXCOUNTER)
        {
            counter++;
            uint lowestPath = 0;

            for (uint i = 0; i < searchingSet.Count; i++)
            {
                if (graph[(int)searchingSet[(int)lowestPath]].f > graph[(int)searchingSet[(int)i]].f)
                {
                    lowestPath = i; //lowest path = index
                }
            }

            AStarNode node = graph[(int)searchingSet[(int)lowestPath]];
            graphNodeIndex = searchingSet[(int)lowestPath];

            if (idTo == searchingSet[(int)lowestPath])
            {
                return BuildPath(node);
            }

            hasSearchedSet.Add(searchingSet[(int)lowestPath]);
            searchingSet.RemoveAt((int)lowestPath);

            List<int> neighborIDs = node.adjacentPointIDs;

            for (int i = 0; i < neighborIDs.Count; i++)
            {
                AStarNode neighborNode = graph[neighborIDs[i]];


                if (hasSearchedSet.Contains((uint)neighborIDs[i]) == false)
                {
                    if (graphNodeIndex == neighborIDs[i])
                    {
                        continue;
                    }

                    float currentG = node.g + Vector3.Distance(node.position, neighborNode.position);
                    bool isNewPath = false;

                    if (searchingSet.Contains((uint)neighborIDs[i]))
                    {
                        if (currentG < neighborNode.g)
                        {
                            isNewPath = true;
                            neighborNode.g = currentG;
                        }
                    }
                    else
                    {
                        isNewPath = true;
                        neighborNode.g = currentG;
                        searchingSet.Add((uint)neighborIDs[i]);
                    }

                    if (isNewPath)
                    {
                        neighborNode.h = Vector3.Distance(neighborNode.position, graph[(int)idTo].position);
                        neighborNode.predecessorNode = graph[(int)graphNodeIndex];
                    }
                }
            }
        }
        
        
        return new List<Vector3>(); //empty list incase the ghost is right next to the target
    }

}
