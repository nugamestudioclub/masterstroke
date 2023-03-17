using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class SlashAction : PlayerAction
{

    [SerializeReference]
    PlayerAction dashAction = new DashClickAction();
    [SerializeReference]
    PlayerAction hitboxAction = new HitboxClickAction();

    public override IEnumerator Apply()
    {
        player.StartCoroutine(hitboxAction.DoAction(player));
        yield return player.StartCoroutine(dashAction.DoAction(player));
    }

    public override void OnFinish()
    {
        // Debug.Log("Finish Slash Action");
        player._rb.velocity = Vector2.zero;
        player.changeState(PlayerController.PlayerState.MoveState);
    }
}
