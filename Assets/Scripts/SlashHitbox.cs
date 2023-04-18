using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashHitbox : MonoBehaviour
{

    [SerializeField] private GameObject PaintStroke;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    // On collision
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.name);
        // If this slash hit an enemy
        if (other.name == "EnemyHitbox")
        {
            other.gameObject.GetComponent<EnemyHitbox>().Hit();
        }
    }
    */


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Instantiate(PaintStroke, collision.gameObject.transform.position, Quaternion.Euler(transform.localRotation.eulerAngles));
        }
    }
}
