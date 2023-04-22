using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform gunTransform;

    [SerializeField] private Rigidbody2D gunRB;

    [SerializeField] private Rigidbody2D enemyRB;

    [SerializeField] private Rigidbody2D playerRB;

    [SerializeField] private GameObject shootingPoint;

    [SerializeField] private SpriteRenderer gunSpriteRenderer;


    [Header("Gun Stuff")]

    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletUpOffset;
    [SerializeField] private float bulletDownOffset;


    [SerializeField] private float shootingStartDelay;

    [SerializeField] private float delayBetweenShots;



    private Vector2 lookDir;
    private float angle;


    void Start()
    {
        //InvokeRepeating("Shoot", shootingStartDelay, delayBetweenShots);
    }


    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.transform.position, shootingPoint.transform.rotation);

        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

        bulletRB.velocity = shootingPoint.transform.right * bulletSpeed + shootingPoint.transform.up * Random.Range(bulletDownOffset, bulletUpOffset);

    }

    public void ShootRepeat()
    {
        InvokeRepeating("Shoot", shootingStartDelay, delayBetweenShots);
    }

    public void ShootStop()
    {
        CancelInvoke();
    }




    private void flipYFalse()
    {
        gunSpriteRenderer.flipY = false;
    }

    private void flipYTrue()
    {
        gunSpriteRenderer.flipY = true;
    }



    private void Update()
    {
        if (playerRB.position.x > enemyRB.position.x)
        {
            flipYFalse();
            this.gameObject.transform.parent.GetComponentInParent<EnemyBehavior>().TurnLeft(false);
        }
        else
        {
            flipYTrue();
            this.gameObject.transform.parent.GetComponentInParent<EnemyBehavior>().TurnLeft(true);
        }

    }

    private void FixedUpdate()
    {
        gunRB.position = enemyRB.position;

        lookDir = playerRB.position - gunRB.position;

        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        gunRB.rotation = angle;

    }

}
