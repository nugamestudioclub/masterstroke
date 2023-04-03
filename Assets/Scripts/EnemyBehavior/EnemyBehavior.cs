using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    bool blind = false;

    [SerializeField]
    int sightRadius = 10;

    [SerializeField]
    bool facingLeft = true;

    private static int numSightLines = 50;

    private float distToSide;


    // Start is called before the first frame update
    void Start()
    {
        distToSide = GetComponent<CapsuleCollider2D>().bounds.extents.x;
    }

    // Update is called once per frame
    void Update()
    {
        bool see = false;
        if (!blind)
        {
            see = this.SeePlayer();     
        }
        if (see)
        {
            //Debug.Log("Player seen");
        }

    }

    // Checks whether the enemy sees the player
    bool SeePlayer() 
    {
        Vector2 lookOrigin;
        float baseAngle;
        if(facingLeft)
        {
            lookOrigin = new Vector2(transform.position.x - distToSide - 0.05f, transform.position.y);
            baseAngle = 180;
        }
        else
        {
            lookOrigin = new Vector2(transform.position.x + distToSide + 0.05f, transform.position.y);
            baseAngle = 0;
        }

        int raysAboveHorizon = numSightLines / 2;
        int raysBelowHorizon = numSightLines - raysAboveHorizon;
        float angleIncrementUp = 45.0f / raysAboveHorizon;
        float angleIncrementDown = 45.0f / raysBelowHorizon;

        float lookAngle;
        Vector2 lookDirection;

        // First 45 degrees search for player
        for (int i = 1; i <= raysAboveHorizon; i++)
        {
            lookAngle = baseAngle - angleIncrementUp * i;
            lookDirection = MathHelpers.DegreeToVector2(lookAngle);
            RaycastHit2D rayCast = Physics2D.Raycast(lookOrigin, lookDirection, sightRadius);
            //Debug.DrawRay(lookOrigin, lookDirection * sightRadius, Color.red, 10.0f);
            if (rayCast.collider != null && rayCast.collider.name == "Player")
            {
                return true;
            }
        }
        // Second 45 degrees search for player
        for (int i = 0; i < raysBelowHorizon; i++)
        {
            lookAngle = baseAngle + angleIncrementDown * i;
            lookDirection = MathHelpers.DegreeToVector2(lookAngle);
            RaycastHit2D rayCast = Physics2D.Raycast(lookOrigin, lookDirection, sightRadius);
            //Debug.DrawRay(lookOrigin, lookDirection * sightRadius, Color.red, 10.0f);
            if (rayCast.collider != null && rayCast.collider.name == "Player")
            {
                return true;
            }
        }
        return false;
    }
    
    public void GetHit()
    {
        Debug.Log("Ouch");
    }


}


public static class MathHelpers
{
    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }
    public static Vector2 RadianToVector2(float radian, float length)
    {
        return RadianToVector2(radian) * length;
    }
    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
    public static Vector2 DegreeToVector2(float degree, float length)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad) * length;
    }
}
