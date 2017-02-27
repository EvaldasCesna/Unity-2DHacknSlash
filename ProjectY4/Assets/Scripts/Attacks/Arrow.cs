using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public EnemyHealth hp;
    public float speed;
    public PlayerAttack pa;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (pa.isAttacking == true)
        {
            GameObject objectColided = collision.gameObject;
            hp = collision.GetComponent<EnemyHealth>();
            if (collision.gameObject.tag == "Enemy" && hp.isDamaged == false)
            {
      
                Vector3 direction = collision.transform.position - transform.position;
                direction = direction.normalized;

                hp.TakeDamage(10, direction);

                Destroy(gameObject);

            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
