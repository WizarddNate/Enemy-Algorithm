using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinarSearch : MonoBehaviour
{
    public float speed = 1.0f;
    private bool LinBool;
    public GameObject target;
    private int playerx;
    private int playery;
    private Timer linearTime;
    //Stopwatch stopwatch = new Stopwatch();

    int[] xarray = new int[] {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14}; //array for all x values on the grid
    int[] yarray = new int[] {0,1,2,3,4,5,6,7,8,9};  //array for all y valuse on the grid

    // Start is called before the first frame update
    void Start()
    {
       //Debug.Log((Linear(xarray,yarray,5, 8)).ToString()); //just to check that the search works
    }

    void Update(){
        playerx = Mathf.RoundToInt(target.transform.position.x);
        playery = Mathf.RoundToInt(target.transform.position.y);
        //x = FindPlayerPosX(target);
        LinBool = Linear(xarray, yarray, playerx, playery); //use the linear search to check if the player is within grid
        MoveTo(LinBool, playerx,playery); // if linbool = true; move to the position

    }


    bool Linear(int[] xarray, int[]yarray, int xkey, int ykey){  //the linear search
        //stopwatch.Start();
        for(int x = 0; x<xarray.Length; x++){
            if(xarray[x] == xkey){
                for(int y = 0; y<yarray.Length; y++){
                    if(yarray[y] == ykey){
                        return true;
                       
                    }
                }
            }
        }
        //stopwatch.Stop();
        //float pathFindingTime = stopwatch.ElapsedMilliseconds;
        //linearTime.StopTime();
        //topwatch.Reset();
        return false;
    }

    void MoveTo(bool go, int xpos, int ypos){  //method to move towords the player
        float step = speed*Time.deltaTime;
        Vector2 targetPosition = new Vector2(xpos, ypos);
        if(go == true){
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
          
        }
    }

}
