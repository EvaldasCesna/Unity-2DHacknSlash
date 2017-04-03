using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UISpawner : NetworkBehaviour {
    public GameObject UIprefab;

    private void OnPlayerConnected(NetworkPlayer player)
    {


        

        Cmdspawner();
    }

    public void Start()
    {
      
    }

    //Spawns enemies in locations
    [Command]
    private void Cmdspawner()
    {

            GameObject ui = (GameObject)Instantiate(UIprefab, transform.position, transform.rotation);
            NetworkServer.Spawn(ui);
    }
}
