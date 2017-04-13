using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public string spawn;
    GameObject user;

	// Use this for initialization
	void Start () {
        spawn = GameObject.Find("StartPoint").GetComponent<PlayerStartPoint>().startPoint;



        DontDestroyOnLoad(gameObject);
        
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            user = GameObject.FindGameObjectWithTag("Player").GetComponent<GameObject>();

            user.GetComponent<PlayerStats>().startPoint = spawn;
        }
    }

    private void OnPlayerConnected(NetworkPlayer player)
    {
        user = GameObject.FindGameObjectWithTag("Player").GetComponent<GameObject>();

        user.GetComponentInParent<PlayerStats>().startPoint = spawn;

     //   Debug.Log(player);
    }

     

}
