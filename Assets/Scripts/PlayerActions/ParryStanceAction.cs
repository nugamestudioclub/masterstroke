using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryStanceAction : PlayerAction
{

    private Vector3 _clickPos;

    [SerializeField]
    private float _duration = .8f;

    public override void OnStart()
    {
        Debug.Log("Start Parry Stance");
        player.ChangeState(PlayerController.PlayerState.ParryState);
        _clickPos = player.transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        player.parryAngle = _clickPos;
        player.rb.velocity = Vector2.zero;
    }

    public override IEnumerator Apply()
    {
        yield return player.StartCoroutine(ParryStance());   
    }

    public override void OnFinish()
    {
        Debug.Log("End Parry Stance");
        player.ChangeState(PlayerController.PlayerState.MoveState);
        
    }

    // For use for manual cancelling
    private IEnumerator ParryStance()
    {
        yield return new WaitForSeconds(_duration);
    }

}
