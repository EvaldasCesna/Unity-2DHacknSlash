using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerStartPoint : NetworkBehaviour {

    public string startPoint;
    private PlayerStats player;
    private Camera cam;
	// Use this for initialization
	void Start () {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerStats>();
            player.spawn = gameObject;


            if (player.startPoint == startPoint)
            {
                cam.transform.position = transform.position;
                player.transform.position = transform.position;
            }
        }

    }

    private void OnConnectedToServer()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerStats>();
            player.spawn = gameObject;


            if (player.startPoint == startPoint)
            {
                cam.transform.position = transform.position;
                player.transform.position = transform.position;
            }
        }
    }

    // Update is called once per frame
    void Update () {

    
    }
}
