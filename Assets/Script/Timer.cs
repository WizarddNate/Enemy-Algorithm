using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    private float time;
    private bool running = true;
    int sec;

    // Start is called before the first frame update
    void Start()
    {
        timerText = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (running){
            time += Time.deltaTime;
            float sec = (float)time;
            timerText.text = "Linear Search Timer: "+sec.ToString("F2");
        }
    }

    public void StopTime(){
        running = false;
        float sec = (float)time;
        timerText.text = "Linear Search Timer: "+time.ToString("F2");
    }
}
