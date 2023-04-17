using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
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

    public PlayerState state { get; private set; } 

    private float distToGround;

    private bool _canSlash;

    [field: SerializeField]
    public Hitbox slashHitbox { get; private set; }

    [System.NonSerialized]
    public Vector2 parryAngle;

    [SerializeField]
    // The maximum angle distance at which you can parry a hit, in degrees
    private float _parryWidth = 30;
    
    void Start()
    {
        distToGround = GetComponent<CapsuleCollider2D>().bounds.extents.y;
        rb = GetComponent<Rigidbody2D>();
        EnterState(PlayerState.MoveState);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }

    public void ChangeState(PlayerState s)
    {
        ExitState(state);
        state = s;
        EnterState(state);
    }
    
    void EnterState(PlayerState s)
    {
        switch(s)
        {
            case PlayerState.MoveState:
                gameObject.AddComponent<PlayerMoveBehaviour>();
                break;
            case PlayerState.SlashState:
                _canSlash = false;
                break;
        }
    }

    void ExitState(PlayerState s)
    {
        switch(s)
        {
            case PlayerState.MoveState:
                Destroy(gameObject.GetComponent<PlayerMoveBehaviour>());
                break;
            case PlayerState.SlashState:
                break;
        }
    }

    void UpdateState()
    {
        switch(state)
        {
            case PlayerState.MoveState:
                if (Input.GetButtonDown("Jump") && this.IsGrounded())
                {
                    gameObject.AddComponent<PlayerJumpBehaviour>();
                }
                
                if(IsGrounded())
                {
                    _canSlash = true;
                }
                
                if (Input.GetMouseButtonDown(0) && _canSlash)
                {
                    gameObject.AddComponent<PlayerSlashBehaviour>();
                }

                if (Input.GetMouseButtonDown(1))
                {
                    gameObject.AddComponent<PlayerParryStanceBehaviour>();
                }
                break;
            case PlayerState.SlashState:
                break;
            case PlayerState.ParryState:
                break;
        }
    }

    public override void OnHit(Hitbox hitbox)
    {
        // This should be an overrideable function from a superclass
        if (checkParry(hitbox))
        {
            Debug.Log("Parried");
            gameObject.AddComponent<PlayerParryBehaviour>();
        }
        else
        {
            Debug.Log("Player Hit");
        }
    }

    private bool checkParry(Hitbox hitbox)
    {
        if (state == PlayerState.ParryState && hitbox.GetOwnerType() == EntityType.Enemy)
        {
            Vector2 enemyToPlayer = hitbox.position - (Vector2) transform.position;
            float hitboxRot = Mathf.Atan2(enemyToPlayer.y, enemyToPlayer.x) * Mathf.Rad2Deg;
            float playerRot = Mathf.Atan2(parryAngle.y, parryAngle.x) * Mathf.Rad2Deg;
            float diff = Mathf.Abs(hitboxRot - playerRot) % 360;
            // Debug.Log(hitboxRot);
            // Debug.Log(playerRot);
            // Debug.Log(diff);
            return diff < _parryWidth;
        }
        return false;
    }

    public bool IsGrounded()
    {
        Vector3 bottomPos = new Vector3(transform.position.x, transform.position.y - distToGround - 0.05f, transform.position.z);
        foreach (RaycastHit2D raycastHit in Physics2D.RaycastAll(bottomPos, -Vector2.up, 0.05f)) {
            if (!raycastHit.collider.isTrigger)
            {
                return true;
            }
        }
        return false;
    }

    public override EntityType GetEntityType()
    {
        return EntityType.Player;
    }

    public enum PlayerState {
        MoveState, SlashState, ParryState
    }

}

