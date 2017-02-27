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

    [SyncVar]
    public float attackTime;
    [SyncVar]
    private float attackTimeCounter;
    [SyncVar]
    public bool isAttacking = false;

    public float speed;
    public GameObject arrowPrefab;
    public GameObject Melee;
    public GameObject Ranged;
    public GameObject Magic;
    Sword sword;
    Bow bow;
    Staff staff;
  //  public EnemyHealth hp;
    private PlayerMovement playerPos;
    private ItemsDatabase item;

    void Start()
    {
       // item = GameObject.FindGameObjectWithTag("Inventory").GetComponentInChildren<ItemsDatabase>();
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
    [Command]
    public void CmdSetSprites(int inSword, int inBow, int inStaff)
    {
     
        RpcSetSprites(inSword, inBow, inStaff);
    }


    [ClientRpc]
    public void RpcSetSprites(int inSword, int inBow, int inStaff)
    {
        item = GameObject.FindGameObjectWithTag("Inventory").GetComponentInChildren<ItemsDatabase>();
        damage = 10;

        if (inSword != -1)
        { 
            sword.GetComponent<SpriteRenderer>().sprite = item.GetItemByID(inSword).Sprite;
            damage = item.GetItemByID(inSword).Strength + damage;
        }
        if (inBow != -1)
        {
            bow.GetComponent<SpriteRenderer>().sprite = item.GetItemByID(inBow).Sprite;
            damage = item.GetItemByID(inBow).Strength + damage;
        }
        if (inStaff != -1)
        {
            staff.GetComponent<SpriteRenderer>().sprite = item.GetItemByID(inStaff).Sprite;
        }
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
        RpcRanged();
        if (isAttacking == false)
        {
            isAttacking = true;

            float angle = (Mathf.Atan2(lastMovement().y, lastMovement().x) * Mathf.Rad2Deg) - 90;
            GameObject arrow = (GameObject)Instantiate(arrowPrefab, transform.position, Quaternion.Euler(new Vector3(lastMovement().y, lastMovement().x, angle)));
            arrow.GetComponent<Arrow>().pa = this;
            arrow.GetComponent<Rigidbody2D>().velocity = lastMovement().normalized * speed;
            NetworkServer.Spawn(arrowPrefab);
            attackTimeCounter = attackTime;
            //  bow.Shoot();
        }
 
    }

    [Command]
    private void CmdMagic()
    {
        RpcMagic();
    }

    [Command]
    private void CmdMelee()
    {
        RpcMelee();
    }

    [ClientRpc]
    private void RpcRanged()
    {
        Ranged.SetActive(true);
        Magic.SetActive(false);
        Melee.SetActive(false);
     
    }
    [ClientRpc]
    private void RpcMagic()
    {
        Ranged.SetActive(false);
        Magic.SetActive(true);
        Melee.SetActive(false);
    }


    [ClientRpc]
    private void RpcMelee()
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
        //        Debug.Log(sword.getHit().currentHealth); Old
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
    //[Command]   //Old
    //public void CmdDoDamage()
    //{
    //    if (hp != null)
    //    {
    //        if (!Network.isServer)
    //            hp.CmdTakeDamage(damage);
    //        else
    //            hp.RpcTakeDamage(damage);
    //    }
    //    hp = null;

    //}




}
