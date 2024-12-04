using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class BlinkyTimer : MonoBehaviour
{
    private bool timerActive;
    private float currentTime;
    [SerializeField] private TMP_Text _text;
    void Start()
    {
        currentTime = 0;
    }

    void FixedUpdate()
    {
        if (timerActive)
        {
            currentTime = currentTime + Time.fixedTime;
        }

        TimeSpan time  = TimeSpan.FromSeconds(currentTime);
        //add text to our TMP text
        _text.text = "A* Time: " + time.Seconds.ToString() + ":"  + time.Milliseconds.ToString();
    }
    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }
}
