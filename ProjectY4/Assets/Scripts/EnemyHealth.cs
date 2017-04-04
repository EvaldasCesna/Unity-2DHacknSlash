﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class EnemyHealth : NetworkBehaviour
{
    public const int maxHealth = 100;
    [SyncVar(hook = "OnChangeHealth")] public int currentHealth = maxHealth;
    public RectTransform healthbar;
    public bool destroyOnDeath;
    [SyncVar]
    public bool isDamaged;
    public GameObject dmgPrefab;
    public GameObject dmgNumPrefab;
    public GameObject goldPrefab;
    public GameObject itemPrefab;
    public float invincibility;
    private float invicibilityCounter;

    public int expToGive;
    public int dropRate;

    private void Start()
    {

    }

    public void FixedUpdate()
    {
        invincibilityCounter();
    }

    [Command]
    public void CmdTakeDamage(int amount, Vector3 dir)
    {

        RpcTakeDamage(amount, dir);
  
    }

    [ClientRpc]
    public void RpcTakeDamage(int amount, Vector3 dir)
    {
        if (isDamaged == false)
        {
          //  Debug.Log("its attacked");
            isDamaged = true;
            invicibilityCounter = invincibility;
            GetComponent<Rigidbody2D>().AddForce(dir * 5000);
            currentHealth -= amount;


            dmgNumPrefab.GetComponent<FloatingNumbers>().damageNum = amount;
            if (isServer)
            {     
                GameObject dmgNum = (GameObject)Instantiate(dmgNumPrefab, transform.position, Quaternion.Euler(Vector3.zero));
                NetworkServer.Spawn(dmgNum);
                
                GameObject hurt = (GameObject)Instantiate(dmgPrefab, transform.position, transform.rotation);
                NetworkServer.Spawn(hurt);

                if (currentHealth <= 0)
                {
                    int rate = Random.Range(0, dropRate);
                    if (rate == 0 || rate > dropRate/2)
                    {
                        GameObject clone = Instantiate(goldPrefab, transform.position, transform.rotation);
                        NetworkServer.Spawn(clone);
                    }
                    if (rate == 1)
                    {
                        GameObject clone = Instantiate(itemPrefab, transform.position, transform.rotation);
                        NetworkServer.Spawn(clone);
                    }
                    //Stats.pStats.addXp(expToGive);
                    if (destroyOnDeath)
                    {
                        Destroy(gameObject);
                    }

                }
            }
           
        }
    }


    public void TakeDamage(int amount, Vector3 dir)
    {
   

        if (!isServer)
        {
            return;
        }
        CmdGiveXp(expToGive, amount);
        CmdTakeDamage(amount,dir);
    
    //       RpcTakeDamage(amount,dir);
    }

    public void TakeDamage(int amount)
    {
        TakeDamage(amount, Vector3.zero);

         //   RpcTakeDamage(amount, Vector3.zero);
    }

    [Command]
    public void CmdGiveXp(int xp, int amount)
    {

        if (currentHealth - amount <= 0)
        {
            RpcGiveXp(xp);
        }
    }

    [ClientRpc]
    public void RpcGiveXp(int xp)
    {

            Stats.pStats.addXp(xp);
 
    }
    //Sync healthbar to damage by server
    void OnChangeHealth(int health)
    {
        healthbar.sizeDelta = new Vector2(health, healthbar.sizeDelta.y);
    }

    private void invincibilityCounter()
    {
        if (invicibilityCounter > 0)
        {
            invicibilityCounter -= Time.fixedDeltaTime;
        }
        if (invicibilityCounter <= 0)
        {
            isDamaged = false;
    
        }
    }


}
