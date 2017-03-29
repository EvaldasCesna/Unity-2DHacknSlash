using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldScript : MonoBehaviour {

    public int value;
    public Sprite[] gold;
	// Use this for initialization
	void Start () {
        value = Random.Range(1, 20);
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
	
	// Update is called once per frame
	void Update () {

    }
}
