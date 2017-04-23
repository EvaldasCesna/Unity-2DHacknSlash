using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class EnemyScript : NetworkBehaviour
{
    private Rigidbody2D rig;
    private bool moving;
    private float timeBetweenMovesCount;
    private float timeToMoveCount;
    private Vector3 moveDirection;
    private float attackTimeCounter;
    private bool isAttacking;

    public bool canShoot;
    public bool canMelee;
    public float speed;
    public float projectileSpeed;
    public int damage;
    public float timeBetweenMoves;
    public float timeToMove;
    public GameObject attackPrefab;
    public GameObject projectilePrefab;
    public float attackTime;


    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        timeBetweenMovesCount = Random.Range(timeBetweenMoves * 0.75f, timeBetweenMoves * 1.25f);
        timeToMoveCount = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
    }

    void FixedUpdate()
    {
        if (moving)
        {
            timeToMoveCount -= Time.fixedDeltaTime;
            rig.velocity = moveDirection;
            if (timeToMoveCount < 0f)
            {
                moving = false;
                timeBetweenMovesCount = Random.Range(timeBetweenMoves * 0.75f, timeBetweenMoves * 1.25f);
            }
        }
        else
        {
            timeBetweenMovesCount -= Time.fixedDeltaTime;
            rig.velocity = Vector2.zero;

            if (timeBetweenMovesCount < 0f)
            {
                moving = true;
                timeToMoveCount = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
                moveDirection = new Vector3(Random.Range(-1f, 1f) * speed, Random.Range(-1f, 1f) * speed, 0);
            }
        }
    }

    public void enemyAttack(GameObject player)
    {
        if (!isServer)
        {
            return;
        }
        CmdenemyAttack(player);
    }

    public void enemyProjectile(GameObject player)
    {
        if (!isServer)
        {
            return;
        }
        CmdenemyProjectile(player);
    }

    [Command]
    public void CmdenemyAttack(GameObject player)
    {
        attackCounter();
        PlayerHealth hp = player.GetComponent<PlayerHealth>();

        if (isAttacking == false)
        {
            if (canMelee)
            {
                Instantiate(attackPrefab, new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z - 1f), transform.rotation);
                isAttacking = true;
                attackTimeCounter = attackTime;
                hp.TakeDamage(damage);
            }
        }
    }


    [Command]
    public void CmdenemyProjectile(GameObject player)
    {
        attackCounter();

        if (isAttacking == false)
        {
            if (canShoot)
            {
                GameObject projectile = (GameObject)Instantiate(projectilePrefab, transform.position, transform.rotation);
                isAttacking = true;
                attackTimeCounter = attackTime;
                projectile.GetComponent<Rigidbody2D>().velocity = (player.transform.position - transform.position).normalized * projectileSpeed;
                projectile.GetComponent<EnemyProjectile>().damage = damage;
                Destroy(projectile.gameObject, 5);
                NetworkServer.Spawn(projectile);
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
