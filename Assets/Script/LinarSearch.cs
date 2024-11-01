using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinarSearch : MonoBehaviour
{
    public float speed = 1.0f;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindPlayer();
        //ignor unless some code for staring postion
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null){
            MoveTowards(player.transform.position);
        }

    }

    GameObject FindPlayer(){
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach(GameObject obj in allObjects){
            if(obj.CompareTag("Player")){   //tag "player"
                return obj;
            }
        }
        //if player not found
        return null;
    }

    void MoveTowards(Vector3 targetPosition){
        float step = speed*Time.deltaTime;
        //move towards player
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }
}
