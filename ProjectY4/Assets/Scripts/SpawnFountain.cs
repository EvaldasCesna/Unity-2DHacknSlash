using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFountain : MonoBehaviour {

    //Could have other features too
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().HealPlayer(1);
            collision.gameObject.GetComponent<PlayerHealth>().SetPotions(3);

        }
    }

}
