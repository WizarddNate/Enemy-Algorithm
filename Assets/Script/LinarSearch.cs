using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinarSearch : MonoBehaviour
{
    public float speed = 1.0f;
    private bool LinBool;

    int[] xarray = new int[] {1,2,3,4,5,6,7,8,9,10,11,12,13,14}; //array for all x values on the grid
    int[] yarray = new int[] {1,2,3,4,5,6,7,8,9};  //array for all y valuse on the grid

    // Start is called before the first frame update
    void Start()
    {

       //Debug.Log((Linear(xarray,yarray,5, 8)).ToString()); //just to check that the search works

    }

    void Update(){
        LinBool = Linear(xarray, yarray, 7, 6); //use the linear search to check if the player is within grid
        MoveTo(LinBool, 7,6); // if linbool = true; move to the position

    }

    bool Linear(int[] xarray, int[]yarray, int xkey, int ykey){
        for(int x = 0; x<xarray.Length; x++){
            if(xarray[x] == xkey){
                for(int y = 0; y<yarray.Length; y++){
                    if(yarray[y] == ykey){
                        return true;
                    }
                }
            }
        }
        return false;
    }

    void MoveTo(bool go, int xpos, int ypos){
        float step = speed*Time.deltaTime;
        Vector2 targetPosition = new Vector2(xpos, ypos);
        if(go == true){
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
        }
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
