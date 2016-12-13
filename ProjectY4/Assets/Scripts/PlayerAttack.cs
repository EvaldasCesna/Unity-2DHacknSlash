using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerAttack : NetworkBehaviour
{
    GameObject[] target;
    public float attackRange;

	
	// Update is called once per frame
	void FixedUpdate() {
        if (!isLocalPlayer)
        {
            return;
        }

#if UNITY_STANDALONE || UNITY_WEBPLAYER
        if (Input.GetKeyUp(KeyCode.Space))
        {
           CmdAttack();
        }
#else
        if (CrossPlatformInputManager.GetButton("Attack")) {
            CmdAttack();
        }
#endif

	}
    [Command]
    private void CmdAttack()
    {

        target = GameObject.FindGameObjectsWithTag("Enemy");
       Debug.Log(GameObject.FindGameObjectsWithTag("Enemy"));
        for (int i = 0; i < target.Length; i++)
        {
            if (Vector3.Distance(transform.position, target[i].transform.position) <= attackRange)
            {
               Debug.Log(target[i].ToString() );
                EnemyHealth hp = target[i].GetComponent<EnemyHealth>();
               Debug.Log(hp);

                if (!Network.isServer)
                    hp.CmdTakeDamage(10);
                else
                    hp.RpcTakeDamage(10);
            }
        }
    }
}
