using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UISpawner : NetworkBehaviour {
    public GameObject UIprefab;


    public override void OnStartServer()
    {
        spawner();
    }

    //Spawns enemies in locations
    private void spawner()
    {

            GameObject ui = Instantiate(UIprefab, transform.position, transform.rotation) as GameObject;
            NetworkServer.Spawn(ui);
    }
}
