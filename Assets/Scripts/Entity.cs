using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public abstract void OnHit(Hitbox hitbox);
    public abstract EntityType GetEntityType();
}

public enum EntityType
{
    Player, Enemy
}