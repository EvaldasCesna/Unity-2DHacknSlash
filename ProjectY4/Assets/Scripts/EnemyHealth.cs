using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class EnemyHealth : NetworkBehaviour
{
    private float invicibilityCounter;

    public Slider hpBar;
    public int maxHealth = 100;
    [SyncVar(hook = "OnChangeHealth")] public int currentHealth;
    public bool destroyOnDeath;
    [SyncVar]
    public bool isDamaged;
    public bool isBoss;
    public GameObject dmgPrefab;
    public GameObject dmgNumPrefab;
    public GameObject goldPrefab;
    public GameObject itemPrefab;
    public float invincibility;
    public int expToGive;
    public int dropRate;
    public int level;
    public Text levelText;

    private void Start()
    {
        currentHealth = maxHealth;
        hpBar.maxValue = maxHealth;
        hpBar.value = currentHealth;
        levelText.text = level.ToString();
    }

    public void FixedUpdate()
    {
        invincibilityCounter();
    }

    [Command]
    public void CmdTakeDamage(int amount, Vector3 dir)
    {
        if (currentHealth - amount <= 0)
        {
            CmdGiveXp(expToGive, amount);
        }

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
                    DropItem();
                    if (isBoss)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            DropItem();
                        }
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

    public void DropItem()
    {


        int rate = Random.Range(0, dropRate);
        if (rate == 0 || rate > dropRate / 2)
        {
            GameObject clone = Instantiate(goldPrefab, new Vector3(transform.position.x + Random.Range(-2.1f, 2.1f),transform.position.y + Random.Range(-2.1f, 2.1f), transform.position.z), transform.rotation);
            NetworkServer.Spawn(clone);
        }
        if (rate == 1)
        {
            GameObject clone = Instantiate(itemPrefab, new Vector3(transform.position.x + Random.Range(-2.1f, 2.1f), transform.position.y + Random.Range(-2.1f, 2.1f), transform.position.z), transform.rotation);
            NetworkServer.Spawn(clone);
        }
    }


    public void TakeDamage(int amount, Vector3 dir)
    {
   
        if (!isServer)
        {
            return;
        }
      
        CmdTakeDamage(amount,dir);
    
    }

    public void TakeDamage(int amount)
    {
        TakeDamage(amount, Vector3.zero);

    }

    [Command]
    public void CmdGiveXp(int xp, int amount)
    {
            RpcGiveXp(xp);
    }

    [ClientRpc]
    public void RpcGiveXp(int xp)
    {

        Stats.pStats.addEnemyKill();
        Stats.pStats.UpdateStats();
        Stats.pStats.addXp(xp);
    }
    //Sync healthbar to damage by server
    void OnChangeHealth(int health)
    {
        hpBar.value = health;
    //    healthbar.sizeDelta = new Vector2(health, healthbar.sizeDelta.y);
      
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
