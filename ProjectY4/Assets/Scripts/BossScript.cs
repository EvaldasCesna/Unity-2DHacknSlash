using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BossScript : NetworkBehaviour
{
    private Rigidbody2D rig;
    private bool moving;
    private float timeBetweenMovesCount;
    private float timeToMoveCount;
    private Vector3 moveDirection;
    private float attackTimeCounter;
    private float shootTimeCounter;
    private bool isAttacking;
    private bool isShooting;

    public bool canShoot;
    public bool doubleShots;
    public int numberOfShots;
    public float shootTime;
    public bool trackMelee;
    public bool doubleMelee;
    public bool canMelee;
    public float attackTime;
    public float speed;
    public float projectileSpeed;
    public int damage;
    public float timeBetweenMoves;
    public float timeToMove;
    public GameObject attackPrefab;
    public GameObject projectilePrefab;


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
     //   PlayerHealth hp = player.GetComponent<PlayerHealth>();

        if (isAttacking == false)
        {
            if (canMelee)
            {
                if (trackMelee)   {
                    if (!doubleMelee)
                    {
                        GameObject attack = (GameObject)Instantiate(attackPrefab, new Vector3(player.transform.position.x + Random.Range(-1, 1), player.transform.position.y + Random.Range(-1, 1), transform.position.z - 1f), transform.rotation);
                        attack.GetComponent<BossAttack>().damage = damage / 2;
                        NetworkServer.Spawn(attack);
                    }

                    if(doubleMelee)
                    {
                        GameObject attack = (GameObject)Instantiate(attackPrefab, new Vector3(player.transform.position.x + Random.Range(-1, 1), player.transform.position.y + Random.Range(-1, 1), transform.position.z - 1f), transform.rotation);
                        attack.GetComponent<BossAttack>().damage = damage / 2;
                        NetworkServer.Spawn(attack);

                        GameObject attack2 = (GameObject)Instantiate(attackPrefab, new Vector3((transform.position.x -player.transform.position.x) + Random.Range(-1, 1),(transform.position.y-player.transform.position.y)+ Random.Range(-1, 1), transform.position.z - 1f), transform.rotation);
                        attack2.GetComponent<BossAttack>().damage = damage / 2;
                        NetworkServer.Spawn(attack2);
                    }
                }

                if(!trackMelee)
                {
                        GameObject attack = (GameObject)Instantiate(attackPrefab, transform.position, transform.rotation);
                        attack.GetComponent<BossAttack>().damage = damage / 2;
                        NetworkServer.Spawn(attack);
                }



                isAttacking = true;
                attackTimeCounter = attackTime;
               // hp.TakeDamage(damage);
            }
        }

    }


    [Command]
    public void CmdenemyProjectile(GameObject player)
    {
        shootingCounter();

        if (isShooting == false)
        {
            if (canShoot)
            {
                if (!doubleShots)
                {
                    for (int i = 0; i < numberOfShots; i++)
                    {
                        GameObject projectile = (GameObject)Instantiate(projectilePrefab, transform.position, transform.rotation);
                        isShooting = true;
                        shootTimeCounter = shootTime;
                        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10)) * projectileSpeed;
                        projectile.GetComponent<EnemyProjectile>().damage = damage;
                        Destroy(projectile.gameObject, 5);
                        NetworkServer.Spawn(projectile);
                    }
                }
                if (doubleShots)
                {
                    for (int i = 0; i < numberOfShots; i++)
                    {
                        GameObject projectile = (GameObject)Instantiate(projectilePrefab, new Vector3(transform.position.x -1,transform.position.y, transform.position.z), transform.rotation);
                        isShooting = true;
                        shootTimeCounter = shootTime;
                        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10)) * projectileSpeed;
                        projectile.GetComponent<EnemyProjectile>().damage = damage;
                        Destroy(projectile.gameObject, 5);
                        NetworkServer.Spawn(projectile);
                    }
                    for (int i = 0; i < numberOfShots; i++)
                    {
                        GameObject projectile = (GameObject)Instantiate(projectilePrefab, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), transform.rotation);
                        isShooting = true;
                        shootTimeCounter = shootTime;
                        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10)) * projectileSpeed;
                        projectile.GetComponent<EnemyProjectile>().damage = damage;
                        Destroy(projectile.gameObject, 5);
                        NetworkServer.Spawn(projectile);
                    }
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


    private void shootingCounter()
    {
        if (shootTimeCounter > 0)
        {
            shootTimeCounter -= Time.fixedDeltaTime;
        }
        if (shootTimeCounter <= 0)
        {
            isShooting = false;
        }

    }

}
