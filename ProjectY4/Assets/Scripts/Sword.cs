using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public EnemyHealth hp;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hp = null;
        GameObject objectColided = collision.gameObject;



        if (objectColided.CompareTag("Enemy"))
        {
             hp = collision.GetComponent<EnemyHealth>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        hp = null;
        GameObject objectColided = collision.gameObject;



        if (objectColided.CompareTag("Enemy"))
        {
            hp = collision.GetComponent<EnemyHealth>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        hp = null;
    }

    public EnemyHealth getHit()
    {

        return hp;
    }

}
