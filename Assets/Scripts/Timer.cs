using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public float timerDuration;
    public float currentTime;

    bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            currentTime += Time.deltaTime;
        }

        if (TimerFinished())
        {
            active = false;
        }
    }

    public void TimerStart()
    {
        active = true;
    }

    public void Reset()
    {
        currentTime = 0;
        TimerStart();
    }

    public void SetDuration(float f)
    {
        timerDuration = f;
    }

    public bool TimerFinished()
    {
        return currentTime >= timerDuration;
    }
}
