using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LoadNewScene : NetworkBehaviour {
    private List<GameObject> players;
    private PlayerStats player;

    public string level;
    public string exitPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isServer)
        {
            return;
        }

        if (collision.gameObject.tag == "Player")
        {
            players = new List<GameObject>();

            players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
            int i = 1;
            foreach(GameObject p in players)
            {
               player = p.GetComponentInParent<PlayerStats>();
                player.setLocation(exitPoint);
                //Move Players far away first so they dont get damaged in the next scene
                player.MoveTo(new Vector3(-1000 + i, 1000, 0));
                i++;
                

            }

            LoadLevel(level);

        }

    }

    [ServerCallback]
    public void LoadLevel(string name)
    {
        NetworkManager.singleton.ServerChangeScene("Loading Screen");

        NetworkManager.singleton.ServerChangeScene(name);
    }

}
