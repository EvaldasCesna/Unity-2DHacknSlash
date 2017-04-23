using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LoadNewScene : NetworkBehaviour
{
    private List<GameObject> players;
    private PlayerStats player;
    private List<Transform> points;

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
            CmdmovePlayers();
            LoadLevel(level);
        }
    }

    [Command]
    void CmdmovePlayers()
    {
        points = new List<Transform>();
        points.AddRange(GetComponentsInChildren<Transform>());

        players = new List<GameObject>();
        players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        int i = 2;
        foreach (GameObject p in players)
        {
            player = p.GetComponentInParent<PlayerStats>();
            player.setLocation(exitPoint);
            //Move Players to the next areas starting Position
            if (p != null)
                RpcmovePlayers(p, points[i].transform.position);
            i++;
        }
    }

    [ClientRpc]
    void RpcmovePlayers(GameObject player, Vector3 position)
    {
        player.transform.position = position;
    }

    [ServerCallback]
    public void LoadLevel(string name)
    {
        NetworkManager.singleton.ServerChangeScene("Loading Screen");
        NetworkManager.singleton.ServerChangeScene(name);
    }

}
