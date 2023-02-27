using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public float timerDuration;

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
            timerDuration -= Time.deltaTime;
        }
    }

    public void TimerStart()
    {
        active = true;
    }

    public void SetDuration(float f)
    {
        timerDuration = f;
    }

    public bool TimerFinished()
    {
        return timerDuration <= 0;
    }
}
