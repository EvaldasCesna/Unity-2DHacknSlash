using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyScript : NetworkBehaviour {
    public float speed;
   //public Transform player;
    GameObject[] player;
    private Rigidbody2D rig;
    public float aggroDistance;
    public float attackDistance;
    private bool moving;
    public float timeBetweenMoves;
    private float timeBetweenMovesCount;
    public float timeToMove;
    private float timeToMoveCount;
    public GameObject attackParticle;

    public float attackTime;
    private Vector3 moveDirection;
    private float attackTimeCounter;
    private bool isAttacking;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();

        timeBetweenMovesCount = Random.Range(timeBetweenMoves * 0.75f, timeBetweenMoves * 1.25f);
        timeToMoveCount = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
    } 

    // Update is called once per frame

    void FixedUpdate () {

        if (moving)
        {
            timeToMoveCount -= Time.fixedDeltaTime;
            rig.velocity = moveDirection;
            if(timeToMoveCount < 0f)
            {
                moving = false;
                timeBetweenMovesCount = Random.Range(timeBetweenMoves * 0.75f, timeBetweenMoves * 1.25f);
            }
        }else
        {
            timeBetweenMovesCount -= Time.fixedDeltaTime;
            rig.velocity = Vector2.zero;

            if (timeBetweenMovesCount < 0f)
            {
                moving = true;
                timeToMoveCount = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
                moveDirection = new Vector3(Random.Range(-1f,1f) * speed, Random.Range(-1f, 1f) * speed, 0);
            }
        }

        if (!isServer)
        {
            return;
        }

        Cmdattack();


    }
    [Command]
    void Cmdattack()
    {
        attackCounter();

        player = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < player.Length; i++)
        {
            //If player comes near they will go after
            if (Vector3.Distance(transform.position, player[i].transform.position) <= aggroDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player[i].transform.position, speed * Time.deltaTime);
            }
            // Attack
            if (Vector3.Distance(transform.position, player[i].transform.position) <= attackDistance)
            {

                PlayerHealth hp = player[i].GetComponent<PlayerHealth>();
                if (isAttacking == false)
                {
                   Instantiate(attackParticle, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f), transform.rotation);
                   //NetworkServer.Spawn(clone);
                    isAttacking = true;
                    attackTimeCounter = attackTime;
                    hp.TakeDamage(1);
                }
            }

        }
    }


    private void attackCounter()
    {
        if (attackTimeCounter > 0)
        {
            attackTimeCounter -= Time.fixedDeltaTime;
        }
        if (attackTimeCounter <= 0)
        {
            isAttacking = false;
        }

    }


}
