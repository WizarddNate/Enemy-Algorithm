using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTimer : MonoBehaviour
{

    public Timer timer;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ghost"))
        {
            //Debug.Log("stop");
            timer.StopTime();
            
        }
    }
}