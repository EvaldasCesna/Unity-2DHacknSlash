using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LevelManager : NetworkBehaviour {
    public string spawn;
    GameObject user;
    private static bool managerExists;
    Scene scene;
    // Use this for initialization
    void Start () {
       // spawn = GameObject.Find("StartPoint").GetComponent<PlayerStartPoint>().startPoint;


        if (!managerExists)
        {
            managerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        
	}
	
	// Update is called once per frame
	void Update () {
         scene = SceneManager.GetActiveScene();

        if (GameObject.FindGameObjectWithTag("Player"))
        {
      //      user = GameObject.FindGameObjectWithTag("Player");

        //    user.GetComponent<PlayerStats>().startPoint = spawn;
        }
    }

    private void OnPlayerConnected(NetworkPlayer player)
    {
        //   user = GameObject.FindGameObjectWithTag("Player").GetComponent<GameObject>();

        // user.GetComponentInParent<PlayerStats>().startPoint = spawn;

        //   Debug.Log(player);

        RpcloadLevel(scene.name);

    }

    [ClientRpc]
    void RpcloadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

     

}
