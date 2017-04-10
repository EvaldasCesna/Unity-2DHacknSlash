using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour {

    public GameObject enemyPrefab;
    public int numberOfEnemies;

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < 2)
        {
            CmdSpawn();
        }
    }

    public void Start()
    {
        CmdSpawn();
    }

    //Spawns enemies in locations
    [Command]
    private void CmdSpawn()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0.0f);
            Quaternion spawnRotation = Quaternion.Euler(0.0f, 0.0f, 0);


            GameObject enemy = (GameObject)Instantiate(enemyPrefab, spawnPosition, spawnRotation);
            NetworkServer.Spawn(enemy);


        }
    }
}
