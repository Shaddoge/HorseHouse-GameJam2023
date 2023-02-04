using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stopwatch : MonoBehaviour
{
    bool stopwatchActive = true;
    float currTime;
    [SerializeField] TextMeshProUGUI timerText;
    // Start is called before the first frame update
    void Start()
    {
        currTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopwatchActive)
        {
            currTime = currTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currTime);
        timerText.text = time.ToString(@"mm\:ss\:ff");

    }
    //Add Handler for Pausing Time
    public void SetStopWatch(bool state)
    {
        stopwatchActive= state;
    }
}
