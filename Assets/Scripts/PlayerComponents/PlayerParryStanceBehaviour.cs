using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryStanceBehaviour : EntityBehaviour
{

    private Vector3 _clickPos;

    [SerializeField]
    private float _duration = .8f;

    private PlayerController _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<PlayerController>();
        _player.ChangeState(PlayerController.PlayerState.ParryState);
        _clickPos = _player.transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        _player.parryAngle = _clickPos;
        _player.rb.velocity = Vector2.zero;

        StartTimer(_duration);
        Debug.Log("Start Parry Stance");
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerFinished())
        {
            Destroy(this);
        }
    }

    protected override void DoOnDestroy()
    {
        base.DoOnDestroy();
        _player.ChangeState(PlayerController.PlayerState.MoveState);
        Debug.Log("End Parry Stance");
    }
}
