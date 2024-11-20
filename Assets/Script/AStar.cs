using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class AStarNode //creating a node for each tile that the A* algoritmn will loop through
{
    //list to know the nodes adjacent to each node
    public List <int> adjacentPointIDs = new List <int> ();

    public AStarNode predecessorNode;

    //Distance to nearest block
    public float g;

    //Predicted distance to final goal
    public float h;

    //Both added togther
    public float f => g + h;

    public Vector3 position = new Vector3 (0, 0, 0);

}

public class AStar: MonoBehaviour //search algorthmn. Mono Behavior allows this to be a component
{
    public Transform player; //player object

    private void Start()
    {
        GridManager gridManager = GameObject.FindObjectOfType<GridManager>();

        foreach(Tile tile in gridManager.grid) //For loop, but instead of iterating through a varible 'x', you iterate through the tiles in the grid array
        {
            if (tile.walkable == true) 
            {
                AddPoint(tile.coords);
             }
        }

        float z = 0.0f;
        foreach(AStarNode aStarNode in graph) /// <-- I dont think that this is right. X, Y and Z are not properly defined.
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
        //uint s = GetPointByPosition(new Vector3(transform.position.x, transform.position.y, 0.0f)); //s for spawn
        //uint p = GetPointByPosition(new Vector3(player.position.x, player.position.y, 0.0f)); //p is for player 
    }

    public uint AddPoint(Vector3 position) //unsigned ints can't be negative! Cool!
    { 
        AStarNode node = new AStarNode();
        node.position = position;
        graph.Add(node);
        return (uint)graph.Count - 1; //do not cast a negative number as uint, it'll become a massive number and hurt you

    } 
    public  uint GetClosestPoint(Vector3 position) //allow each point to see the closest point to them
    {
        uint id = 0;
        float minDistance = float.MaxValue; //set float to its largest value
        float distance = 0.0f;

        for(int i = 1; i < graph.Count; i++)
        {
            distance = Vector3.Distance(position, graph[i].position); 

            if (minDistance < distance)
            {
                id = 1;
                minDistance = distance;
            }

        }
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
        
       /* if (idFrom <= graph.Count)
        {
           
        }
        if (idTo <- graph.Count)
       */
    }

    bool ArePointsConnected(uint idFrom, uint idTo) //here for debugging
    {
        if(graph.Count <= idFrom)
        {
            Debug.Log("ERROR: AStar, ArePointsConnected idFrom has not been added to the graph!");
        }
        if(graph.Count <= idTo)
        {
            Debug.Log("ERROR: AStar, ArePointsConnected idTo has not been added to the graph!");
        }

        for(int i = 0; i < graph[(int)idFrom].adjacentPointIDs.Count; i++)
        {
            if (graph[(int)idFrom].adjacentPointIDs[i] == idTo)
            {
                return true;
            }
        }
        return false;
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

    /*List<Vector3> GetPath(uint idFrom, uint idTo)
    {
        List<uint> searchingSet = new List<uint>();
        List<uint> hasSearchedSet = new List<uint>();
        uint graphNodeIndex; 

        searchingSet.Add(idFrom);

        while (searchingSet.Count > 0)
        {
            uint lowestPath = 0;

            for (uint i = 0; i <searchingSet.Count; i++)
            {
                if (graph)
            }
        }

    }*/
        

    public List<AStarNode> graph;

}
