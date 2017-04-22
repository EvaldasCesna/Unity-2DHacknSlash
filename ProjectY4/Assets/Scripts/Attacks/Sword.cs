using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    public EnemyHealth hp;
    public PlayerAttack pa;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        pa = GetComponentInParent<PlayerAttack>();
 
            GameObject objectColided = collision.gameObject;
            hp = collision.GetComponent<EnemyHealth>();
            if ((collision.gameObject.tag == "Enemy" || collision.tag == "Boss") && hp.isDamaged == false)
            {
            Vector3 direction = collision.transform.position - transform.position;
                direction = direction.normalized;
            pa.CmdMeleeDmg(collision.gameObject, direction);

            hp = null;

        }

        if (collision.gameObject.tag == "EnemyProjectile")
        {
            Destroy(collision.gameObject);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
       
    }

    public EnemyHealth getHit()
    {

        return hp;
    }

}
