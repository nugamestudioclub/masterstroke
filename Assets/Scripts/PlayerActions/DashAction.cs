using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class DashAction : PlayerAction
{
    [SerializeField]
    protected Vector2 direction;
    [SerializeField]
    float _force = 25.0f;
    [SerializeField]
    float _duration = .10f;

    public override void OnStart()
    {
        Debug.Log("Start Slash Action");
        // clickPos = player.transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public override IEnumerator Apply()
    {
        // Debug.Log("Doing Slash Action");
        player.rb.velocity = direction.normalized * _force;
        yield return new WaitForSeconds(_duration);
    }

    public override void OnFinish()
    {
        // Debug.Log("Finish Slash Action");
        player.rb.velocity = Vector2.zero;
        player.changeState(PlayerController.PlayerState.MoveState);
    }
}
