using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LoadNewScene : NetworkBehaviour {

    public string level;
    public string exitPoint;
    private PlayerStats player;
	// Use this for initialization
	void Start () {
 
    }


    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.tag == "Player")
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerStats>();
            SceneManager.LoadSceneAsync("Loading Screen");
            player.startPoint = exitPoint;
            SceneManager.LoadSceneAsync(level);
            LoadLevel(level);

        }

    }

    [ServerCallback]
    public void LoadLevel(string name)
    {
        NetworkManager.singleton.ServerChangeScene(name);
    }

}
