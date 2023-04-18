using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveBehaviour : EntityBehaviour
{

    [SerializeField]
    private float _speed = 15.0f;

    private PlayerController _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveDir = Input.GetAxis("Horizontal");
        _player.rb.velocity = new Vector2(moveDir * _speed, _player.rb.velocity.y);
    }
}
