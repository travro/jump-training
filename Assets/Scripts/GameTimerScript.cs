using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerScript : MonoBehaviour
{

    private Stopwatch watch;
    public Text TimerText = null;


    public TimeSpan TimeElapsed
    {
        get
        {
            return watch.Elapsed;
        }
    }
    // Use this for initialization
    void Awake()
    {
        watch = new Stopwatch();
    }
    void Start()
    {
        watch.Start();
    }
    public void Stop()
    {
        watch.Stop();
    }

    public void Reset()
    {
        watch.Reset();
    }


    // Update is called once per frame
    void Update()
    {
        if (TimerText != null)
        {
            TimerText.text = TimeElapsed.ToString();
        }
    }
}
