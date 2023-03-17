using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class HitboxClickAction : HitboxAction
{
    public override void OnStart()
    {
        Debug.Log("Dash Click Start");
        direction = player.transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        base.OnStart();
    }

}
