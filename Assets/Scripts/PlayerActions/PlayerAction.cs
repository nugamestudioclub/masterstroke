using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class PlayerAction
{
    protected PlayerController player;
    private bool active = false;

    virtual public void OnStart()
    {

    }

    public virtual IEnumerator Apply()
    {
        yield break;
    }

    public virtual void OnFinish()
    {

    }

    public IEnumerator DoAction(PlayerController player)
    {
        if (!active)
        {
            active = true;
            this.player = player;
            OnStart();
        }
        yield return player.StartCoroutine(Apply());
        active = false;
        OnFinish();
    }
}

