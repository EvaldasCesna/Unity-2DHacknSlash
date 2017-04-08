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
        //  if (pa.isAttacking == true)
        //    {
 
            GameObject objectColided = collision.gameObject;
            hp = collision.GetComponent<EnemyHealth>();
            if (collision.gameObject.tag == "Enemy" && hp.isDamaged == false) //&& pa.isAttacking == true)
            {
          
            //   Debug.Log(pa.isAttacking);
            //   Debug.Log("its attacking");
            Vector3 direction = collision.transform.position - transform.position;
                direction = direction.normalized;
            pa.CmdMeleeDmg(collision.gameObject, direction);
            //    hp.TakeDamage(pa.meleeDamage, direction);
            hp = null;



     //       }
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
