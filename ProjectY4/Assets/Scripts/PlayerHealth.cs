using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour {

    [SyncVar(hook = "OnMaxHealthChanged")]
    public int maxHealth = 100;
    private NetworkStartPosition[] spawnPoints;
    private Stats stats;

    [SyncVar(hook = "OnHealthChanged")] public int currentHealth;
    public bool died = false;
    public int def;

    private void Start()
    {
        stats = Stats.pStats;
        currentHealth = maxHealth;
    }

    [Command]
    public void CmdChangeMaxHealth(int amount)
    {
        maxHealth = 100;
  
        maxHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

    }
    //Only the server should be responsivle for the player health
    [Server]
    public bool TakeDamage(int amount)
    {
        //  Debug.Log(amount - (amount * stats.getBonusArmor() / maxHealth));
        //Armor calculation and health take away
        currentHealth -= amount - (amount * stats.getBonusArmor() / maxHealth);
        if (currentHealth <= 0)
        {
            died = true;
            currentHealth = maxHealth;
            RpcRespawn(died);
            return died;
        }

        return died;
    }

    public void HealPlayer(int amount)
    {
        if(!isServer)
        {
            return;
        }
        Heal(amount);
    }

    [Server]
    public void Heal(int amount)
    {
        if (currentHealth != 0 || (currentHealth + amount) <= maxHealth)
        {
            currentHealth += amount;
        }
    }

    void OnHealthChanged(int amount)
    {
        currentHealth = amount;
        if (isLocalPlayer)
        {
            Stats.pStats.UpdateHealthbar(maxHealth, currentHealth);
        }
    }

    void OnMaxHealthChanged(int amount)
    {
        maxHealth = amount;
        if (isLocalPlayer)
        {
            Stats.pStats.UpdateHealthbar(maxHealth, currentHealth);
        }
    }

    [ClientRpc]
    void RpcRespawn(bool died)
    {
        if(isLocalPlayer && died)
        {
            Vector3 spawnPoint = Vector3.zero;

            if (spawnPoints == null)
            {
                spawnPoints = FindObjectsOfType<NetworkStartPosition>();
            }

            if (spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }


            Stats.pStats.UpdateHealthbar(maxHealth, currentHealth);
            transform.position = spawnPoint;
        }
    }
}
