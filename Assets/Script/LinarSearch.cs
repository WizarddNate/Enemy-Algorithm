using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinarSearch : MonoBehaviour
{
    public float speed;

    int playerX = 8;
    //int playerY = 4;
    // Start is called before the first frame update
    void Start()
    {
        //ignor unless some code for staring postion
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newPos = transform.position;
        newPos.x += speed;
        //transform.position = newPos;

        for(int i = 0; i<14; i++){
            if( i == playerX){
                transform.position = newPos;
                 if (newPos.x >= 14){
                  transform.position = new Vector3(0,0,-0.5f);
                 }
            }
            
        }

        // make a function to find playerlocation
        // for(int i = 0; < arr.Length; i++){
        //     #if (arr[i] == target){
        //         #return i;
        //     }
        //     #if it cannot find player just dont move
        // }

        // #once location found, move twords player
        // #check player location => move one space colser
    }
}
