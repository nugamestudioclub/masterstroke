using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour
{
    //The Player GameObject
    [SerializeField] private GameObject player;

    //The parent object of the attack (the attack prefab)
    [SerializeField] private GameObject attackPrefab;

    //The hitbox child object of the attackPrefab (the one with the collider)
    [SerializeField] private GameObject attackHitbox;


    //How long the attack hitbox lasts for. This can be edited in the inspector
    public float attackDuration = 0.5f;


    //Keeps track of whether or not the player is attacking
    private bool attacking = false;



    void Update()
    {
        //When you left click
        if (Input.GetMouseButtonDown(0))
        {        
            //if you are not attacking
            if (attacking == false)
            {
                //perform an attack
                DoAttack();
            }
        }
    }


    //attack method
    void DoAttack()
    {
        //get the position of the mouse on the screen
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //get the direction that the player is facing relative to the moiuse
        Vector2 lookDir = mousePos - player.transform.position;
        
        //angle math
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        //set the parent object's rotation to the angle that the player is facing the mouse
        attackPrefab.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        //the child object is offset from the parent so that when you rotate the parent, the child rotates around it.
        //Parent position should be equal to player position

        //start a coroutine that performs an attack that lasts x amount of seconds
        StartCoroutine(performAttack(attackDuration));

        //player is attacking
        attacking = true;

        //sets the attack prefab to active, which activates both the parent and the child. The important one is the hitbox with the collider
        attackPrefab.SetActive(true);

    }

    //after waitTime seconds, stop attacking
    private IEnumerator performAttack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        //turns off the attack prefab
        attackPrefab.SetActive(false);
       
        //player is no longer attacking
        attacking = false;
    }




}





