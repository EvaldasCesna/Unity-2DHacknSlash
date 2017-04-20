using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFountain : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
   
            collision.gameObject.GetComponent<PlayerHealth>().HealPlayer(1);

        }
    }

}
