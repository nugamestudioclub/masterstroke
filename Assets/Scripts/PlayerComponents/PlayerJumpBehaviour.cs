using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpBehaviour : EntityBehaviour
{

    [SerializeField]
    private float _jumpSpeed = 20.0f;

    private PlayerController _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<PlayerController>();
        _player.rb.velocity = new Vector2(_player.rb.velocity.x, _jumpSpeed);
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
