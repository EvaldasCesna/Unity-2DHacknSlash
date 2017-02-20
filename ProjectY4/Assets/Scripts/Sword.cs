using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    public EnemyHealth hp;
    public PlayerAttack pa;
    // Use this for initialization

    void Start () {
        pa = GetComponentInParent<PlayerAttack>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     
        GameObject objectColided = collision.gameObject;
       
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log(collision.ToString());
            hp = collision.GetComponent<EnemyHealth>();
            if(pa.isAttacking == true)
            pa.CmdDoDamage();

        }
    }
    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject objectColided = collision.gameObject;
        
        if (objectColided.CompareTag("Enemy"))
        {
            hp.Add(collision.GetComponent<EnemyHealth>());
        }
    }*/
    private void OnTriggerExit2D(Collider2D collision)
    {

    }

    public EnemyHealth getHit()
    {

        return hp;
    }

}
