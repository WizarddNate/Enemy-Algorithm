using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Inky : MonoBehaviour
{
    public GameObject self;
    public GameObject target;
    public float speed = 2.0f;
    BreadthSearch pathfinder;

    // grabbing the breadth search as it starts
    private void Awake()
    {
        pathfinder = GetComponent<BreadthSearch>();
    }

    // call it at a consistent frame rate
    void FixedUpdate()
    {
        // start following the generated path
        if (pathfinder != null)
        {
            StartCoroutine(FollowPath());
        }
    }

    IEnumerator FollowPath()
    {
        //measure the processing speed to generate a path
        //Stopwatch stopwatch = new Stopwatch();
        //stopwatch.Start();

        // grabbing the current pos and the player pos
        Vector2 currentpos = new Vector2((float)Mathf.Round(self.transform.position.x), (float)Mathf.Round(self.transform.position.y));
        Vector2 targetpos = new Vector2((float)Mathf.Round(target.transform.position.x), (float)Mathf.Round(target.transform.position.y));

        // setting the current pos and target pos
        pathfinder.SetNewDestination(currentpos, targetpos);

        // grabbing the new path from current pos
        List<Vector2> path = pathfinder.GetNewPath(currentpos);

        // total time to generate
        //stopwatch.Stop();
        //UnityEngine.Debug.Log("Pathfinding time: " + stopwatch.ElapsedMilliseconds + " ms");

        for (int i = 0; i < path.Count; i++)
        {
            // looping throught the coords and moving towards the player 
            Vector2 coords = path[i];
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(coords.x, coords.y, 0), Time.deltaTime * speed);
            //UnityEngine.Debug.Log($"Path: {string.Join(", ", path)}");


            yield return null;
        }
    }
}
