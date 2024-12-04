using System.Collections;
using UnityEngine;
using TMPro;  

public class PinkieTimer : MonoBehaviour
{
    public TMP_Text timerText;
    public float timeElapsed;

    void Start()
    {
        timerText = gameObject.GetComponent<TMP_Text>();
        
    }

    void Update()
    {
        timerText.text = "DFS Time: " + timeElapsed.ToString("F2");
    }
    public void ChangeTime(float time)
    {
        timeElapsed = time;
    }
}
