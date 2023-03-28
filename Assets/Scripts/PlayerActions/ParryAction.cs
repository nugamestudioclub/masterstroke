using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryAction : PlayerAction
{

    Hitbox hitbox;

    public override void OnStart(params System.Object[] objs)
    {
        if (objs.Length < 1) {
            throw new System.ArgumentOutOfRangeException();
        }
        else if (objs[0].GetType() != typeof(Hitbox))
        {
            throw new System.InvalidCastException();
        }
        hitbox = (Hitbox)objs[0];
    }
}
