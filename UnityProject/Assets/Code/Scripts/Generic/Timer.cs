using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public float timer = 0.0f;
    public float finalTime = 10.0f;
    private Delegate callback;
    private object[] args;
    private bool isRunning = false;

 
    void Update()
    {
        if (!isRunning) return;
        timer += Time.deltaTime;
        if (timer >= finalTime) {
            timer = 0.0f;
            callback.DynamicInvoke(args);
            isRunning = false;
        }
    }

    public void SetTimer(float time, Delegate callback, params object[] args) {
        finalTime = time;
        timer = 0.0f;
        this.callback = callback;
        this.args = args;
        isRunning = true;
    }

    public void SetTimer(float time, Action callback) {
        finalTime = time;
        timer = 0.0f;
        this.callback = callback;
        this.args = null;
        isRunning = true;
    }


}
