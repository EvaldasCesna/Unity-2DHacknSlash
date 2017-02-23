using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerAttack : NetworkBehaviour
{
   // GameObject[] target;
    //public float attackRange;
    Animator anim;

    public int damage = 10;

    public float attackTime;
    private float attackTimeCounter;
    public bool isAttacking = false;

    public GameObject Melee;
    public GameObject Ranged;
    public GameObject Magic;
    Sword sword;
    Bow bow;
    Staff staff;

    private PlayerMovement playerPos;

    void Start()
    {
        playerPos = GetComponentInParent<PlayerMovement>();
        staff = Magic.GetComponentInChildren<Staff>();
        bow = Ranged.GetComponentInChildren<Bow>();
        sword = Melee.GetComponentInChildren<Sword>();
        anim = GetComponent<Animator>();

        Ranged.SetActive(false);
        Magic.SetActive(false);
        Melee.SetActive(false);
    }


	// Update is called once per frame
	void FixedUpdate() {
        if (!isLocalPlayer)
        {
            return;
        }

        userInput();

        attackCounter();

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
    public Vector2 lastMovement()
    {
        return playerPos.lastMovement;
    }

    private void userInput()
    {
#if UNITY_STANDALONE || UNITY_WEBPLAYER
        if (Input.GetKeyDown(KeyCode.Space) && isAttacking == false)
        {
            CmdMelee();
        }
        if (Input.GetKeyDown(KeyCode.F) && isAttacking == false)
        {
            CmdRanged();
        }
        if (Input.GetKeyDown(KeyCode.G) && isAttacking == false)
        {
            CmdMagic();
        }

#else
        if (CrossPlatformInputManager.GetButton("Attack")) {
            CmdAttack();
        }
#endif

    }

    private void attackCounter()
    {
        if (attackTimeCounter > 0)
        {
            attackTimeCounter -= Time.fixedDeltaTime;
        }
        if (attackTimeCounter <= 0)
        {
            isAttacking = false;
            anim.SetBool("isAttacking", isAttacking);
        }

    }

    [Command]
    private void CmdRanged()
    {
        Ranged.SetActive(true);
        Magic.SetActive(false);
        Melee.SetActive(false);
        if (isAttacking == false)
        {
            attackTimeCounter = attackTime;
            bow.Shoot();
        }
    }
    [Command]
    private void CmdMagic()
    {
        Ranged.SetActive(false);
        Magic.SetActive(true);
        Melee.SetActive(false);
    }

    [Command]
    private void CmdMelee()
    {
        Melee.SetActive(true);
        Ranged.SetActive(false);
        Magic.SetActive(false);

        if (isAttacking == false)
        {
            isAttacking = true;

            anim.SetBool("isAttacking", isAttacking);
            attackTimeCounter = attackTime;

        }
        //        Debug.Log(sword.getHit().currentHealth);
        //        EnemyHealth hp = sword.getHit();
        //        if (sword.getHit() != null)
        //        {
        //            if (!Network.isServer)
        //                hp.CmdTakeDamage(10);
        //            else
        //                hp.RpcTakeDamage(10);
        //        }
        //sword.hp = null;



    }
    [Command]
    public void CmdDoDamage()
    {
        EnemyHealth hp = sword.getHit();
        if (sword.getHit() != null)
        {
            if (!Network.isServer)
                hp.CmdTakeDamage(damage);
            else
                hp.RpcTakeDamage(damage);
        }
        hp = null;

    }
}
