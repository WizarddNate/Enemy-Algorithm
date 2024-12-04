using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class BlinkyTimer : MonoBehaviour
{
    public TMP_Text timerText;
    public float timeElapsed;
    void Start()
    {
        timerText = gameObject.GetComponent<TMP_Text>();
    }

    void Update()
    {
        timerText.text = "Inky Time: " + timeElapsed.ToString("F2");
    }
    public void ChangeTime(float time)
    {
        timeElapsed = time;
    }
}
