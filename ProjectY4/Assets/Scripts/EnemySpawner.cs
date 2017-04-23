using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour
{

    bool isEmpty;

    public GameObject enemyPrefab;
    public bool infiniteSpawn;
    public int numberOfEnemies;
    public bool CustomSpawn;
    public int level;
    public int maxHealth;
    public int damage;
    public float attackTime;
    public float projectileSpeed;
    public int firstId;
    public int lastId;

    private void Start()
    {
        if(!infiniteSpawn)
        {
            CmdSpawn();
        }
    }

    void FixedUpdate()
    {
        if (!isServer)
        {
            return;
        }


        if (isEmpty && infiniteSpawn)
        {
            CmdSpawn();
        }

        isEmpty = true;
    }

    //Spawns enemies in locations
    [Command]
    private void CmdSpawn()
    {

        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(transform.position.x - 10.0f, transform.position.x + 10.0f), Random.Range(transform.position.y - 10.0f, transform.position.y + 10.0f), -1f);
            Quaternion spawnRotation = Quaternion.Euler(0.0f, 0.0f, 0);
            GameObject enemy = (GameObject)Instantiate(enemyPrefab, spawnPosition, spawnRotation);

            //Overrides any default enemy info
            if (CustomSpawn)
            {
                enemy.GetComponent<EnemyHealth>().level = level;
                enemy.GetComponent<EnemyHealth>().maxHealth = maxHealth;
                enemy.GetComponent<EnemyHealth>().firstId = firstId;
                enemy.GetComponent<EnemyHealth>().lastId = lastId;
                enemy.GetComponent<EnemyScript>().damage = damage;
                enemy.GetComponent<EnemyScript>().attackTime = attackTime;
                enemy.GetComponent<EnemyScript>().projectileSpeed = projectileSpeed;
            }
            NetworkServer.Spawn(enemy);

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
                isEmpty = false;
        }
    }
}
