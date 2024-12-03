using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class LinearTimer : MonoBehaviour
{
    public TMP_Text timerText;
    private float time;
    //private bool running = true;
    //int sec;

    // Start is called before the first frame update
    void Start()
    {
        timerText = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = "Linear Search Timer: "+time.ToString("F2");
    }

    public void StopTime(float timegone){
        time = timegone;
        //Debug.log(timegone);
    }
}
