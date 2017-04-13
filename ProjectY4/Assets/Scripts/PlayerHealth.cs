using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour {
     int maxHealth = 100;
    // [SyncVar (hook = "OnChangeHealth")] public int currentHealth = maxHealth;
    [SyncVar(hook = "OnHealthChanged")] int currentHealth;
    public RectTransform healthbar;
    public bool died = false;
    //  private Stats stats;
    //   private int lvl;
    private void Start()
    {

    //    stats = GameObject.FindGameObjectWithTag("UIGUI").GetComponent<Stats>();

    //     lvl = stats.currentLevel;
    currentHealth = currentHealth + maxHealth;
      //  OnHealthChange();
    }
    private void Update()
    {
        //   OnHealthChange(); 
    }

    public void ChangeMaxHealth(int amount)
    {
        maxHealth = 100;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        maxHealth += amount;
    }
    //Only the server should be responsivle for the player health
    [Server]
    public bool TakeDamage(int amount)
    {
  
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            died = true;
            currentHealth = maxHealth;
            RpcRespawn(died);
            return died;
        }

        return died;
    }

    [ClientRpc]
    void RpcTakeDamage(int amount, bool died)
    {

        if (died)
            RpcRespawn(died);
    }

    void OnHealthChanged(int amount)
    {
        currentHealth = amount;
        if (isLocalPlayer)
        {
            updateMaxHealth();
            Stats.pStats.UpdateHealthbar(maxHealth, currentHealth);
       //     if (lvl != stats.currentLevel)
       //     {
      //          lvl = stats.currentLevel;
              
       //         currentHealth = maxHealth ;
          //  }
        }
    }

    public void updateMaxHealth()
    {
        if (isLocalPlayer)
        {
            Stats.pStats.UpdateHealthbar(maxHealth, currentHealth);
        }
    }




    //Sync healthbar to damage by server
    //void OnChangeHealth(int health)
    //{
    //    stats.UpdateHealthbar(maxHealth + stats.getBonusHealth(), currentHealth);
    //    healthbar.sizeDelta = new Vector2(health, healthbar.sizeDelta.y);
    //}

    [ClientRpc]
    void RpcRespawn(bool died)
    {
        if(isLocalPlayer && died)
        {
            Stats.pStats.UpdateHealthbar(maxHealth, currentHealth);
            transform.position = Vector3.zero;
        }
    }
}
