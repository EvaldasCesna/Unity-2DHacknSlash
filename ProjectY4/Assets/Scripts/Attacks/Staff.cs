using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour {
    public GameObject fireAoe;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject FireAoe(int damage)
    {
        GameObject clone = Instantiate(fireAoe, transform.position, transform.rotation);
        //NetworkServer.Spawn(clone);

        //anim.SetBool("isAttacking", true);
        GameObject[] target = GameObject.FindGameObjectsWithTag("Enemy");
        //  Debug.Log(GameObject.FindGameObjectsWithTag("Enemy"));
        for (int i = 0; i < target.Length; i++)
        {
            if (Vector3.Distance(transform.position, target[i].transform.position) <= 2.5f)
            {
                //  Debug.Log(target[i].ToString() );
                EnemyHealth hp = target[i].GetComponent<EnemyHealth>();

                // EnemyHealth hp = staff.getHit();
                //  anim.SetBool("isAttacking", false);
                // anim.SetBool("isAttacking", false);
                hp.TakeDamage(damage);
            }
        }

        return clone;
    }

}
