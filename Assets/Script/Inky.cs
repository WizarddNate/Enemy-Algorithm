using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;
public class Inky : MonoBehaviour
{
    public GameObject target;
    public float speed = 2.0f;
    BreadthSearch pathfinder;
    bool running = false;
    private InkyTimer timerText;
    
    private void Start()
    {
        pathfinder = GetComponent<BreadthSearch>();
        timerText = FindObjectOfType<InkyTimer>();
    }


    void FixedUpdate()
    {
        if (pathfinder != null && timerText!= null && !running)
        {
            StartCoroutine(FollowPath());
        }
        
    }

    IEnumerator FollowPath()
    {
        running = true;
        //measure the processing speed to generate a path
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        
        // Grabbing the current position and target position
        Vector3 currentPos = transform.position;
        Vector3 targetPos = target.transform.position;

        // Setting the current position and target position for the pathfinder
        pathfinder.SetNewDestination(currentPos, targetPos);

        // Grabbing the new path from the current position
        Dictionary<Vector2, List<Vector2>> pathDict = pathfinder.GetNewPath(currentPos);
        
        // total time to generate
        stopwatch.Stop();
        float pathFindingTime = stopwatch.ElapsedMilliseconds;
        timerText.ChangeTime(pathFindingTime);
        //UnityEngine.Debug.Log($"Pathfinding time: {stopwatch.ElapsedMilliseconds} ms");
        stopwatch.Reset();

        // If the path dictionary contains the target get the path
        if (pathDict.ContainsKey(targetPos))
        {
            List<Vector2> path = pathDict[targetPos];

            // Move along the path
            for (int i = 0; i < path.Count; i++)
            {
                Vector3 nextPos = new Vector3(path[i].x, path[i].y, transform.position.z);
                while (Vector3.Distance(transform.position, nextPos) > 0.1f) 
                {
                    transform.position = Vector3.MoveTowards(transform.position, nextPos, Time.deltaTime * speed);
                    yield return null;
                }
            }
        }
        running = false;
        yield return null;
    }
}
