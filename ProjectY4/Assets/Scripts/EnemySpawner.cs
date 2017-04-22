using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour {

    bool isEmpty;

    public GameObject enemyPrefab;
    public int numberOfEnemies;
  //  public int minEnemies;

    public void Awake()
    {
   //     CmdSpawn();
    }

    public void Start()
    {

    }

    public void Update()
    {


    }

    void FixedUpdate()
    {
        if(!isServer)
        {
            return;
        }


        if (isEmpty)
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
            Vector3 spawnPosition = new Vector3(Random.Range(transform.position.x -10.0f, transform.position.x + 10.0f), Random.Range(transform.position.y - 10.0f, transform.position.y +10.0f), -1f);
            Quaternion spawnRotation = Quaternion.Euler(0.0f, 0.0f, 0);


            GameObject enemy = (GameObject)Instantiate(enemyPrefab, spawnPosition, spawnRotation);

            NetworkServer.Spawn(enemy);

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            isEmpty = false;
        }
        
    }


}
