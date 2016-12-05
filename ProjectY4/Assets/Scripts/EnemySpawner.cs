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
            spawner();
        }
    }

    public override void OnStartServer()
    {
        spawner();
    }

    //Spawns enemies in locations
    private void spawner()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0.0f);
            Quaternion spawnRotation = Quaternion.Euler(0.0f, 0.0f, 0);


            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, spawnRotation) as GameObject;
            NetworkServer.Spawn(enemy);
        }
    }
}
