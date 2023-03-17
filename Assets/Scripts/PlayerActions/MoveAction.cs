using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : PlayerAction
{
    [SerializeField]
    private float _speed = 15.0f;

    public override IEnumerator Apply()
    {
        float moveDir = Input.GetAxis("Horizontal");
        player._rb.velocity = new Vector2(moveDir * _speed, player._rb.velocity.y);
        yield break;
    }
}
