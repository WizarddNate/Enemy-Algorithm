using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinarSearch : MonoBehaviour
{
    public float speed = 1.0f;
    private GameObject player;
    private GameObject moveTo;

    int[] array = new int[] {1,2,3,4,5};
    // Start is called before the first frame update
    void Start()
    {
       player = FindPlayer(); //find the player game object
       //moveTo = Linear();
       Debug.Log((Linear(array, 3)).ToString());

    }

    void Update(){
        if(player != null){
            //Linear(player.transform.position);

        }

    }

    GameObject FindPlayer(){ //find the player game object (and take its position)
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach(GameObject obj in allObjects){
            if(obj.CompareTag("Player")){   //tag "player"
                return obj;
            }
        }
        //if player not found
        return null;
    }

    bool Linear(int[] array, int key){
        for(int x = 0; x<array.Length; x++){
            if(array[x] == key){
                return true;
            }
        }
        return false;
    }

    // void Linear(Vector3 targetPosition){
    //     float step = speed*time.deltaTime
    //     for(int y = 1; y<=9; y++){ //search the y valuses

    //         for(int x = 1; x<=14; x++){ //search the x values

    //             if(x = targetPosition.x and y = targetPosition.y){
    //                 transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    //             }
    //         }

    //     }
    // }

}
