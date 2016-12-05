using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerAttack : MonoBehaviour {
    GameObject[] target;
    public float attackRange;

	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Space) || CrossPlatformInputManager.GetButton("Attack"))
        {
           CmdAttack();
        }
	}

    private void CmdAttack()
    {
        //attack closest enemy doesnt work on client for some reason
        target = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < target.Length; i++)
        {
            if (Vector3.Distance(transform.position, target[i].transform.position) <= attackRange)
            {
                EnemyHealth hp = target[i].GetComponent<EnemyHealth>();
                hp.TakeDamage(10);
            }
        }
    }
}
