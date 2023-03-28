using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Tags
{
    public const string Player = "Player";
    public const string Enemy = "Enemy";
}

public class Hitbox : MonoBehaviour
{
    [SerializeField]
    Entity owner;
    [SerializeField]
    public Vector2 direction;

    private new BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        this.collider = this.GetComponent<BoxCollider2D>();
        if (collider == null)
        {
            collider = this.GetComponentInChildren<BoxCollider2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger Entered");
        Entity otherEntity = other.GetComponentInParent<Entity>();

        switch (owner.GetEntityType())
        {
            case EntityType.Player:
                switch (otherEntity.GetEntityType())
                {
                    case EntityType.Enemy:
                        otherEntity.OnHit(this);
                        break;
                }
                break;
            case EntityType.Enemy:
                switch (otherEntity.GetEntityType())
                {
                    case EntityType.Player:
                        otherEntity.OnHit(this);
                        break;
                }
                break;
        }
                
    }

    public EntityType GetOwnerType()
    {
        return owner.GetEntityType();
    }
}

