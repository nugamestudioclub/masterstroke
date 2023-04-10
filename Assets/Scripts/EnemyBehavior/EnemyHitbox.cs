using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // On collision
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.name);
        if (other.name == "SlashHitbox")
        {
            this.Hit();
        }
    }

    public void Hit()
    {
        GetComponentInParent<EnemyBehavior>().GetHit();
    }
}
