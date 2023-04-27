using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    private EnemyBehavior _enemyBehavior;

    public override void OnHit(Hitbox hitbox)
    {
        //throw new System.NotImplementedException();
        Debug.Log("Enemy Hit");
        //Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        _enemyBehavior = GetComponent<EnemyBehavior>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override EntityType GetEntityType()
    {
        return EntityType.Enemy;
    }
}

