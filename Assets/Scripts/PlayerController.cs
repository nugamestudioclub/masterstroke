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
    float speed = 20.0f;
    [SerializeField]
    float maxSpeed = 10.0f;
    [SerializeField]
    float jumpSpeed = 10.0f;
    [SerializeField]
    float slashForce = 7.0f;
    [SerializeField]
    float slashDuration = .15f;

    [Header(" ")]

    [SerializeField] private Rigidbody2D rb;

    public PlayerState state = PlayerState.MoveState; //is there a reason why this is public?

    Vector2 clickPos;

    public Timer timerPrefab; //is there a reason why this is public?
    private Timer localTimer;


    // Update is called once per frame
    void Update()
    {
        updateState();
    }

    void changeState(PlayerState s)
    {
        exitState(state);
        state = s;
        enterState(state);
    }
    
    void enterState(PlayerState s)
    {
        switch(s)
        {
            case PlayerState.SlashState:
                localTimer = Instantiate(timerPrefab);
                localTimer.transform.parent = transform;
                localTimer.SetDuration(slashDuration);
                localTimer.TimerStart();
                break;
        }
    }

    void exitState(PlayerState s)
    {
        switch(s)
        {
            case PlayerState.SlashState:
                Destroy(localTimer.gameObject);
                break;
        }
    }

    void updateState()
    {
        switch(state)
        {
            case PlayerState.MoveState:
                float moveDir = Input.GetAxis("Horizontal");
                rb.velocity = new Vector2(MaxMagnitude(rb.velocity.x, moveDir * maxSpeed), rb.velocity.y);
                
                if (Input.GetButtonDown("Jump"))
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                }
                
                
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 mousePos = Input.mousePosition;
                    clickPos = transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(mousePos));
                    changeState(PlayerState.SlashState);
                }
                break;
            case PlayerState.SlashState:
                rb.velocity = clickPos.normalized * slashForce;
                if (localTimer.TimerFinished()) 
                {
                    changeState(PlayerState.MoveState);
                }
                break;
        }
    }


    public enum PlayerState {
        MoveState, SlashState
    }
}
