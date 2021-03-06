﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Arrow : NetworkBehaviour
{
    public EnemyHealth hp;
    public float speed;
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        GameObject objectColided = collision.gameObject;
        hp = collision.GetComponent<EnemyHealth>();
        if ((collision.gameObject.tag == "Enemy" || collision.tag == "Boss") && hp.isDamaged == false)
        {
            Vector3 direction = collision.transform.position - transform.position;
            direction = direction.normalized;

            hp.TakeDamage(damage, direction);

            Destroy(gameObject);

        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
