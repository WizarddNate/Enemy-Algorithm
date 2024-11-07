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

    private void Awake()
    {
        pathfinder = GetComponent<BreadthSearch>();
    }

    void FixedUpdate()
    {
        if (pathfinder != null && self.transform.position != target.transform.position)
        {
            StartCoroutine(FollowPath());
        }
    }

    IEnumerator FollowPath()
    {
        //Stopwatch stopwatch = new Stopwatch();
        //stopwatch.Start();

        Vector2Int currentpos = new Vector2Int((int)Mathf.Round(self.transform.position.x), (int)Mathf.Round(self.transform.position.y));
        Vector2Int targetpos = new Vector2Int((int)Mathf.Round(target.transform.position.x), (int)Mathf.Round(target.transform.position.y));

        pathfinder.SetNewDestination(currentpos, targetpos);
        List<Vector2Int> path = pathfinder.GetNewPath(currentpos);

        //stopwatch.Stop();
        //UnityEngine.Debug.Log("Pathfinding time: " + stopwatch.ElapsedMilliseconds + " ms");

        //stopwatch.Restart();
        for (int i = 0; i < path.Count; i++)
        {
            Vector2Int coords = path[i];
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(coords.x, coords.y, 0), Time.deltaTime * speed);
            currentpos = coords;
            yield return null;
        }
        //stopwatch.Stop();

        //UnityEngine.Debug.Log("Movement time: " + stopwatch.ElapsedMilliseconds + " ms");
    }
}
