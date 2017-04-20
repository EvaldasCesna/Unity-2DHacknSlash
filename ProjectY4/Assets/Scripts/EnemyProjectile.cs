using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {
    public int damage;
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = ((collision.transform.position - transform.position) * 5f);
            Destroy(gameObject);
        }
    }

}