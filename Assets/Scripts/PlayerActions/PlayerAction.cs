using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class PlayerAction
{
    protected PlayerController player { get; private set; }
    private bool active = false;

    // Runs at the start of the DoAction call
    virtual public void OnStart(params System.Object[] objs)
    {
        OnStart();
    }

    virtual public void OnStart()
    {

    }

    // Runs during the DoAction call
    public virtual IEnumerator Apply()
    {
        yield break;
    }

    // Runs once Apply has finished
    public virtual void OnFinish()
    {

    }

    public IEnumerator DoAction(PlayerController player, params System.Object[] objs)
    {
        if (!active)
        {
            active = true;
            this.player = player;
            OnStart(objs);
        }
        yield return player.StartCoroutine(Apply());
        active = false;
        OnFinish();
    }
}

