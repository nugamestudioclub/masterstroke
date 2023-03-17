using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAction : PlayerAction
{
    [SerializeField]
    private float _jumpSpeed = 20.0f;

    public override IEnumerator Apply()
    {
        float moveDir = Input.GetAxis("Horizontal");
        player.rb.velocity = new Vector2(player.rb.velocity.x, _jumpSpeed);
        yield break;
    }
}
