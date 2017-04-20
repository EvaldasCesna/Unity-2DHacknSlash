using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerAttack : NetworkBehaviour
{
    private Animator anim;
    private Stats stats;
    private Sword sword;
    private Bow bow;
    private Staff staff;
    private bool meleeEquipped;
    private bool magicEquipped;
    private bool rangedEquipped;
    //Use for if you need to disable the ability to attack
    private bool canAttack;
    private float attackTimeCounter;
    private PlayerMovement playerPos;
    private ItemsDatabase item;
    //Default Damages
    public int meleeDamage;
    public int rangedDamage;
    public int magicDamage;
    //Time between attacks
    public float attackTime;
    public bool isAttacking;
    //Different Weapons attached to the player
    public GameObject Melee;
    public GameObject Ranged;
    public GameObject Magic;


    private void Awake()
    {
        playerPos = GetComponentInParent<PlayerMovement>();
        staff = Magic.GetComponentInChildren<Staff>();
        bow = Ranged.GetComponentInChildren<Bow>();
        sword = Melee.GetComponentInChildren<Sword>();

        sword.GetComponent<Collider2D>().enabled = isAttacking;
        anim = GetComponent<Animator>();
        item = Inventory.pInventory.GetComponentInChildren<ItemsDatabase>();
        stats = Stats.pStats;

        bow.GetComponent<SpriteRenderer>().enabled = false;
        staff.GetComponent<SpriteRenderer>().enabled = false;
        sword.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Start()
    {
        if (isLocalPlayer)
            canAttack = true;
    }


    void FixedUpdate()
    {
        attackCounter();

        if (!canAttack)
            return;

        userInput();
    }

    public Vector2 lastMovement()
    {
        return playerPos.lastMovement;
    }

    private void userInput()
    {
#if UNITY_STANDALONE || UNITY_WEBPLAYER
        if (Input.GetKeyDown(KeyCode.Space) && isAttacking == false && meleeEquipped)
        {
            isAttacking = true;
            attackTimeCounter = attackTime;
            CmdMelee(isAttacking);
        }
        if (Input.GetKeyDown(KeyCode.F) && isAttacking == false && rangedEquipped)
        {
            isAttacking = true;
            float angle = (Mathf.Atan2(lastMovement().y, lastMovement().x) * Mathf.Rad2Deg) - 90;
            attackTimeCounter = attackTime;
            CmdRanged(transform.position, new Vector3(lastMovement().y, lastMovement().x, angle), lastMovement().normalized);
        }
        if (Input.GetKeyDown(KeyCode.G) && isAttacking == false && magicEquipped)
        {
            isAttacking = true;
            attackTimeCounter = attackTime;
            CmdMagic();
        }

#else
        if (CrossPlatformInputManager.GetButton("Melee") && isAttacking == false && meleeEquipped)
        {
            isAttacking = true;
            attackTimeCounter = attackTime;
            CmdMelee(isAttacking);
        }
        if (CrossPlatformInputManager.GetButton("Ranged") && isAttacking == false && rangedEquipped)
        {
            isAttacking = true;
            float angle = (Mathf.Atan2(lastMovement().y, lastMovement().x) * Mathf.Rad2Deg) - 90;
            attackTimeCounter = attackTime;
            CmdRanged(transform.position, new Vector3(lastMovement().y, lastMovement().x, angle), lastMovement().normalized);
        }
        if (CrossPlatformInputManager.GetButton("Magic") && isAttacking == false && magicEquipped)
        {
            isAttacking = true;
            attackTimeCounter = attackTime;
            CmdMagic();
        }
#endif

    }
    [Command]
    public void CmdSetEquipment(int inSword, int inBow, int inStaff)
    {
        RpcSetEquipment(inSword, inBow, inStaff);
    }


    [ClientRpc]
    public void RpcSetEquipment(int inSword, int inBow, int inStaff)
    {
        if (inSword != -1)
        {
            meleeEquipped = true;
            meleeDamage = 10;
            sword.GetComponent<SpriteRenderer>().sprite = item.GetItemByID(inSword).Sprite;
            meleeDamage = item.GetItemByID(inSword).Strength + meleeDamage + stats.currentLevel;
        }
        else
        {
            meleeEquipped = false;
            sword.GetComponent<SpriteRenderer>().sprite = null;
        }
        if (inBow != -1)
        {
            rangedEquipped = true;
            rangedDamage = 10;
            bow.GetComponent<SpriteRenderer>().sprite = item.GetItemByID(inBow).Sprite;
            rangedDamage = item.GetItemByID(inBow).Strength + rangedDamage + stats.currentLevel;
        }
        else
        {
            rangedEquipped = false;
            bow.GetComponent<SpriteRenderer>().sprite = null;
        }
        if (inStaff != -1)
        {
            magicEquipped = true;
            magicDamage = 5;
            staff.GetComponent<SpriteRenderer>().sprite = item.GetItemByID(inStaff).Sprite;
            magicDamage = item.GetItemByID(inStaff).Strength + magicDamage + stats.currentLevel;
        }
        else
        {
            magicEquipped = false;
            staff.GetComponent<SpriteRenderer>().sprite = null;
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
            sword.GetComponent<Collider2D>().enabled = isAttacking;
            anim.SetBool("isAttacking", isAttacking);
        }
    }

    [Command]
    private void CmdRanged(Vector3 position, Vector3 rotation, Vector2 direction)
    {
        RpcRanged();

        NetworkServer.Spawn(bow.shootArrow(rangedDamage, rotation, direction));
    }

    [Command]
    private void CmdMagic()
    {
        RpcMagic();

        NetworkServer.Spawn(staff.FireAoe(magicDamage));
    }

    [Command]
    private void CmdMelee(bool attacking)
    {
        sword.GetComponent<Collider2D>().enabled = true;
        RpcMelee(attacking);
    }

    [Command]
    public void CmdMeleeDmg(GameObject enemy, Vector3 dir)
    {
        EnemyHealth hp = enemy.GetComponent<EnemyHealth>();
        hp.TakeDamage(meleeDamage, dir);
    }

    [ClientRpc]
    private void RpcRanged()
    {
        bow.GetComponent<SpriteRenderer>().enabled = true;
        staff.GetComponent<SpriteRenderer>().enabled = false;
        sword.GetComponent<SpriteRenderer>().enabled = false;
    }

    [ClientRpc]
    private void RpcMagic()
    {
        bow.GetComponent<SpriteRenderer>().enabled = false;
        staff.GetComponent<SpriteRenderer>().enabled = true;
        sword.GetComponent<SpriteRenderer>().enabled = false;
    }

    [ClientRpc]
    private void RpcMelee(bool attacking)
    {
        bow.GetComponent<SpriteRenderer>().enabled = false;
        staff.GetComponent<SpriteRenderer>().enabled = false;
        sword.GetComponent<SpriteRenderer>().enabled = true;
        sword.GetComponent<Collider2D>().enabled = true;
        anim.SetBool("isAttacking", attacking);
    }
}
