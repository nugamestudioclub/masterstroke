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


    [Header("Movement Settings")]
    [SerializeField]
    private float _speed = 15.0f;
    [SerializeField]
    private float _jumpSpeed = 20.0f;
    [SerializeField]
    private float _slashForce = 25.0f;
    [SerializeField]
    private float _slashDuration = .10f;

    [Header(" ")]

    private Rigidbody2D _rb;

    private PlayerState _state = PlayerState.MoveState; //is there a reason why this is public?

    Vector2 clickPos;

    public Timer timerPrefab; //is there a reason why this is public?
    private Timer localTimer;

    private float distToGround;

    private bool _canSlash;

    void Start()
    {
        distToGround = GetComponent<CapsuleCollider2D>().bounds.extents.y;
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        updateState();
    }

    void changeState(PlayerState s)
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
                localTimer = Instantiate(timerPrefab);
                localTimer.transform.parent = transform;
                localTimer.SetDuration(_slashDuration);
                localTimer.TimerStart();
                _canSlash = false;
                break;
        }
    }

    void exitState(PlayerState s)
    {
        switch(s)
        {
            case PlayerState.SlashState:
                _rb.velocity = Vector2.zero;
                Destroy(localTimer.gameObject);
                break;
        }
    }

    void updateState()
    {
        switch(_state)
        {
            case PlayerState.MoveState:
                float moveDir = Input.GetAxis("Horizontal");
                // rb.velocity = new Vector2(MaxMagnitude(rb.velocity.x, moveDir * maxSpeed), rb.velocity.y);
                _rb.velocity = new Vector2(moveDir * _speed, _rb.velocity.y);
                if (Input.GetButtonDown("Jump") && this.IsGrounded())
                {
                    _rb.velocity = new Vector2(_rb.velocity.x, _jumpSpeed);
                }
                
                if(IsGrounded())
                {
                    _canSlash = true;
                }
                
                if (Input.GetMouseButtonDown(0) && _canSlash)
                {
                    Vector3 mousePos = Input.mousePosition;
                    clickPos = transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(mousePos));
                    changeState(PlayerState.SlashState);
                }
                break;
            case PlayerState.SlashState:
                _rb.velocity = clickPos.normalized * _slashForce;
                if (localTimer.TimerFinished()) 
                {
                    changeState(PlayerState.MoveState);
                }
                break;
        }
    }


    private bool IsGrounded()
    {
        Vector3 bottomPos = new Vector3(transform.position.x, transform.position.y - distToGround - 0.05f, transform.position.z);
        return Physics2D.Raycast(bottomPos, -Vector2.up, 0.05f);
    }


    public enum PlayerState {
        MoveState, SlashState
    }
}
