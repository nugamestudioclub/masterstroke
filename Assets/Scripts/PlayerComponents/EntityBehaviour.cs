using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBehaviour : MonoBehaviour
{

    private Timer _localTimer;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void StartTimer(float time)
    {
        GameObject gameObject = new GameObject("Timer");
        gameObject.transform.parent = transform;
        // GameObject gameObject2 Instantiate()
        gameObject.AddComponent<Timer>();
        _localTimer = gameObject.GetComponent<Timer>();
        _localTimer.SetDuration(time);
        _localTimer.TimerStart();
    }

    protected bool TimerFinished()
    {
        if (_localTimer != null)
        {
            return _localTimer.TimerFinished();
        }
        return false;
    }

    private void OnDestroy()
    {
        DoOnDestroy();
    }

    protected virtual void DoOnDestroy()
    {
        if (_localTimer != null)
        {
            Destroy(_localTimer.gameObject);
        }
    }

}
