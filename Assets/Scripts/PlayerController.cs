using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    static float MaxMagnitude(float f1, float f2)
    {
        if (Mathf.Abs(f1) > Mathf.Abs(f2))
        {
            return f1;
        }
        return f2;
    }

    public Rigidbody2D rb { get; private set; }

    private PlayerState _state = PlayerState.MoveState;

    private float distToGround;

    private bool _canSlash;

    public bool testing = true;

    [SerializeField]
    [SerializeReference]
    List<PlayerAction> actionList = new List<PlayerAction>{
        new MoveAction(),
        new JumpAction(),
        new SlashAction()
    };


    void Start()
    {
        distToGround = GetComponent<CapsuleCollider2D>().bounds.extents.y;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        updateState();
    }

    public void changeState(PlayerState s)
    {
        exitState(_state);
        _state = s;
        enterState(_state);
    }
    
    void enterState(PlayerState s)
    {
        switch(s)
        {
            case PlayerState.SlashState:
                DoAction(actionList[(int)PlayerActions.Slash]);
                _canSlash = false;
                break;
        }
    }

    void exitState(PlayerState s)
    {
        switch(s)
        {
            case PlayerState.SlashState:
                break;
        }
    }

    void updateState()
    {
        switch(_state)
        {
            case PlayerState.MoveState:
                // float moveDir = Input.GetAxis("Horizontal");
                // rb.velocity = new Vector2(MaxMagnitude(rb.velocity.x, moveDir * maxSpeed), rb.velocity.y);
                // _rb.velocity = new Vector2(moveDir * _speed, _rb.velocity.y);
                DoAction(actionList[(int)PlayerActions.Move]);
                if (Input.GetButtonDown("Jump") && this.IsGrounded())
                {
                    // _rb.velocity = new Vector2(_rb.velocity.x, _jumpSpeed);
                    DoAction(actionList[(int)PlayerActions.Jump]);
                }
                
                if(IsGrounded())
                {
                    _canSlash = true;
                }
                
                if (Input.GetMouseButtonDown(0) && _canSlash)
                {
                    changeState(PlayerState.SlashState);
                }
                break;
            case PlayerState.SlashState:
                break;
        }
    }


    public bool IsGrounded()
    {
        Vector3 bottomPos = new Vector3(transform.position.x, transform.position.y - distToGround - 0.05f, transform.position.z);
        return Physics2D.Raycast(bottomPos, -Vector2.up, 0.05f);
    }

    public void DoAction(PlayerAction a)
    {
        StartCoroutine(a.DoAction(this));
    }

    public enum PlayerState {
        MoveState, SlashState
    }

    enum PlayerActions
    {
        Move, Jump, Slash
    }
}

