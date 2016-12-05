using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    public float speed;
   //public Transform player;
    GameObject[] player;
    private Rigidbody2D rig;
    public float aggroDistance;
    public float attackDistance;
    // Update is called once per frame

    void FixedUpdate () {


        player = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i< player.Length; i++)
        {
            //If player comes near they will go after
            if (Vector3.Distance(transform.position, player[i].transform.position) <= aggroDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player[i].transform.position, speed * Time.deltaTime);
            }
            //Temp Attack
            if (Vector3.Distance(transform.position, player[i].transform.position) <= attackDistance)
            {

                PlayerHealth hp = player[i].GetComponent<PlayerHealth>();
                hp.TakeDamage(1);
            }

        }
    }


}
