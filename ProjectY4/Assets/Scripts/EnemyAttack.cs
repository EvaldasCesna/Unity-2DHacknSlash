using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            GetComponentInParent<EnemyScript>().enemyAttack(collision.gameObject);
    }

}
