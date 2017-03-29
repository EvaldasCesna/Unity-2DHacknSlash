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
    Stats stats;
    public int meleeDamage;
    public int rangedDamage;
    public int magicDamage;
    [SyncVar]
    public float attackTime;
    [SyncVar]
    private float attackTimeCounter;
    [SyncVar]
    public bool isAttacking = false;

    public float speed;
    public GameObject arrowPrefab;
    public GameObject fireAoe;
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

        stats = GameObject.FindGameObjectWithTag("UIGUI").GetComponent<Stats>();
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
        if (CrossPlatformInputManager.GetButton("Melee"))
        {
            CmdMelee();
        }
        if (CrossPlatformInputManager.GetButton("Ranged"))
        {
            CmdRanged();
        }
        if (CrossPlatformInputManager.GetButton("Magic"))
        {
            CmdMagic();
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

        if (inSword != -1)
        {
            meleeDamage = 10;
            sword.GetComponent<SpriteRenderer>().sprite = item.GetItemByID(inSword).Sprite;
            meleeDamage = item.GetItemByID(inSword).Strength + meleeDamage + stats.currentLevel;
        }
        if (inBow != -1)
        {
            rangedDamage = 10;
            bow.GetComponent<SpriteRenderer>().sprite = item.GetItemByID(inBow).Sprite;
            rangedDamage = item.GetItemByID(inBow).Strength + rangedDamage + stats.currentLevel;
        }
        if (inStaff != -1)
        {
            magicDamage = 5;
            staff.GetComponent<SpriteRenderer>().sprite = item.GetItemByID(inStaff).Sprite;
            magicDamage = item.GetItemByID(inBow).Strength + magicDamage + stats.currentLevel;
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
            //NetworkServer.Spawn(arrowPrefab);
            attackTimeCounter = attackTime;
            //  bow.Shoot();
        }
 
    }

    [Command]
    private void CmdMagic()
    {
        RpcMagic();
        if (isAttacking == false)
        {
            isAttacking = true;

            Instantiate(fireAoe, transform.position, transform.rotation);
            attackTimeCounter = attackTime;
            anim.SetBool("isAttacking", true);
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
                    hp.TakeDamage(magicDamage);
                }
            }

        }
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
