using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    public EnemyHealth hp;
    public PlayerAttack pa;
    public GameObject damageParticle;
    public GameObject damageNumber;
    private PlayerMovement player;
    // Use this for initialization

    void Start () {
        player = GetComponentInParent<PlayerMovement>();
        pa = GetComponentInParent<PlayerAttack>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (pa.isAttacking == true)
        {
            GameObject objectColided = collision.gameObject;
            hp = collision.GetComponent<EnemyHealth>();
            if (collision.gameObject.tag == "Enemy" && hp.isDamaged == false)
            {
                objectColided.GetComponent<Rigidbody2D>().AddForce(player.lastMovement * 10000f);
                pa.CmdDoDamage();

                Instantiate(damageParticle, objectColided.transform.position, objectColided.transform.rotation);
                //Creates a clone and sets the damage numbers to what the damage is
                var clone = (GameObject)Instantiate(damageNumber, objectColided.transform.position, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingNumbers>().damageNum = pa.damage;

             
                
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
       
    }

    public EnemyHealth getHit()
    {

        return hp;
    }

}
