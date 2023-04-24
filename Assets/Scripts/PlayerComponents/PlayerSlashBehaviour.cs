using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlashBehaviour : EntityBehaviour
{
    private Rigidbody2D _rb;
    private PlayerController _player;

    private Vector2 direction;
    
    private Hitbox _hitboxPrefab;
    private Hitbox _localHitbox;

    [SerializeField]
    private float _force = 25.0f;
    [SerializeField]
    private float _duration = .10f;

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<PlayerController>();
        _player.ChangeState(PlayerController.PlayerState.SlashState);
        _hitboxPrefab = _player.slashHitbox;
        _rb = _player.rb;

        direction = transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _localHitbox = Instantiate(_hitboxPrefab, transform, false);
        _localHitbox.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        _localHitbox.owner = _player;

        StartTimer(_duration);
        // Debug.Log(direction.normalized);
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = direction.normalized * _force;
        if (TimerFinished())
        {
            Destroy(this);
        }
    }

    protected override void DoOnDestroy()
    {
        base.DoOnDestroy();
        _rb.velocity = Vector2.zero;
        _player.ChangeState(PlayerController.PlayerState.MoveState);
        Destroy(_localHitbox.gameObject);
    }

}
