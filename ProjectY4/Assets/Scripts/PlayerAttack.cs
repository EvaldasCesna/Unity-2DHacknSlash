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
    Animator anim;
    Sword sword;

    public float attackTime;
    private float attackTimeCounter;
    private bool isAttacking;

    void Start()
    {
        sword = GetComponentInChildren<Sword>();
        anim = GetComponent<Animator>();
    }
	// Update is called once per frame
	void FixedUpdate() {
        if (!isLocalPlayer)
        {
            return;
        }
        
#if UNITY_STANDALONE || UNITY_WEBPLAYER
        if (Input.GetKeyDown(KeyCode.Space))
        {
           CmdAttack();
        }
#else
        if (CrossPlatformInputManager.GetButton("Attack")) {
            CmdAttack();
        }
#endif
        if(attackTimeCounter > 0)
        {
            attackTimeCounter -= Time.deltaTime;
        }
        if (attackTimeCounter <= 0)
        {
            isAttacking = false;
            anim.SetBool("isAttacking", isAttacking);
        }

    }
    //Old attack
    //[Command]
    //private void CmdAttack()
    //{

    //    anim.SetBool("isAttacking", true);
    //    target = GameObject.FindGameObjectsWithTag("Enemy");
    // //  Debug.Log(GameObject.FindGameObjectsWithTag("Enemy"));
    //    for (int i = 0; i < target.Length; i++)
    //    {
    //        if (Vector3.Distance(transform.position, target[i].transform.position) <= attackRange)
    //        {
    //            //  Debug.Log(target[i].ToString() );
    //            // EnemyHealth hp = target[i].GetComponent<EnemyHealth>();

    //            EnemyHealth hp = sword.getHit();
    //            //  anim.SetBool("isAttacking", false);
    //            anim.SetBool("isAttacking", false);
    //            if (!Network.isServer)
    //                hp.CmdTakeDamage(10);

    //            else
    //                hp.RpcTakeDamage(10);
    //        }
    //    }

    //}

    [Command]
    private void CmdAttack()
    {
    
  
        anim.SetBool("isAttacking", isAttacking);

        if (sword.getHit() != null)
        {
            EnemyHealth hp = sword.getHit();

            if (!Network.isServer)
                hp.CmdTakeDamage(10);
            else
                hp.RpcTakeDamage(10);
        }
        attackTimeCounter = attackTime;
        isAttacking = true;

    }
}
