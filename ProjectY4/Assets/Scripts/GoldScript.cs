using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class GoldScript : NetworkBehaviour
{
    [SyncVar(hook = "syncGold")]
    public int value;
    public Sprite[] gold;
    // Use this for initialization
    void Start()
    {

        if (isServer)
        {
            value = Random.Range(1, 20);
        }

        if (value == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = gold[0];
        }
        if (value == 2)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = gold[1];
        }
        if (value == 4)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = gold[2];
        }
        if (value == 10)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = gold[3];
        }
        if (value == 20)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = gold[4];
        }
    }

    void syncGold(int gold)
    {
        if (isServer) return;
        value = gold;
    }

}
