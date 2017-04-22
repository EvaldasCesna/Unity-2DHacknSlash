using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (GetComponentInParent<EnemyScript>() != null)
                GetComponentInParent<EnemyScript>().enemyAttack(collision.gameObject);
            if (GetComponentInParent<BossScript>() != null)
                GetComponentInParent<BossScript>().enemyAttack(collision.gameObject);

        }
    }

}
