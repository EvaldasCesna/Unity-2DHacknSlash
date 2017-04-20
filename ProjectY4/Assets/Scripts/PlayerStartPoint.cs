using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerStartPoint : NetworkBehaviour {

    private PlayerStats player;
    private List<GameObject> players;
    private List<Transform> points;
    private Camera cam;

    public string startPoint;

    void Start () {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            if (!isServer)
            {
                return;
            }

            points = new List<Transform>();
            points.AddRange(GetComponentsInChildren<Transform>());

            players = new List<GameObject>();
            players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
            int i = 1;
    
            foreach (GameObject p in players)
            {
                player = p.GetComponentInParent<PlayerStats>();
                if (player.startPoint == startPoint)
                {      
                    player.MoveTo(points[i].transform.position);
                    i++;
                }
 
            }
        }

    }

}
