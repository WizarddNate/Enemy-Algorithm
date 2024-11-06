using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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
        StartCoroutine(FollowPath());

    }
    IEnumerator FollowPath()
    {
        Vector2Int currentpos = new Vector2Int((int)Mathf.Round(this.transform.position.x), (int)Mathf.Round(this.transform.position.y));
        Vector2Int targetpos = new Vector2Int((int)Mathf.Round(target.transform.position.x), (int)Mathf.Round(target.transform.position.y));
        pathfinder.SetNewDestination(currentpos, targetpos);
        
        List<Vector2Int> path = pathfinder.GetNewPath(currentpos);
        for (int i = 0; i < path.Count; i++)
        {
            Vector2Int coords = path[i];
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(coords.x, 0, 0), Time.deltaTime * speed);
            currentpos = coords;
            //Debug.Log($"Path: {string.Join(", ", path)}");

        }
        yield return null;
    }
}
