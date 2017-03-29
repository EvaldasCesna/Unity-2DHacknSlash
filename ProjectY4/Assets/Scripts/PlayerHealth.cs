using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour {
    public const int maxHealth = 100;
    public int maxHp = maxHealth;
    // [SyncVar (hook = "OnChangeHealth")] public int currentHealth = maxHealth;
    public int currentHealth = maxHealth;
    public RectTransform healthbar;
    private Stats stats;
    private int lvl;
    private void Start()
    {
        stats = GameObject.FindGameObjectWithTag("UIGUI").GetComponent<Stats>();
        if (isLocalPlayer)
        {
            GameObject.Find("Healthbar Canvas").SetActive(false);
        }
        lvl = stats.currentLevel;
        currentHealth = currentHealth + stats.getBonusHealth() + stats.currentLevel;
        updateHealthbar();
    }
    private void Update()
    {
        updateHealthbar(); 
    }
    public void TakeDamage(int amount)
    {
        if (!isServer)
        {
            return;
        }
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
            RpcRespawn();
        }
    }

    void updateHealthbar()
    {
        stats.UpdateHealthbar(maxHealth + stats.getBonusHealth() + stats.currentLevel, currentHealth);
        if(lvl != stats.currentLevel)
        {
            lvl = stats.currentLevel;
            currentHealth = maxHealth + stats.getBonusHealth() + stats.currentLevel;
        }
    }



    //Sync healthbar to damage by server
    //void OnChangeHealth(int health)
    //{
    //    stats.UpdateHealthbar(maxHealth + stats.getBonusHealth(), currentHealth);
    //    healthbar.sizeDelta = new Vector2(health, healthbar.sizeDelta.y);
    //}

    [ClientRpc]
    void RpcRespawn()
    {
        if(isLocalPlayer)
        {
            stats.UpdateHealthbar(maxHealth, currentHealth);
            transform.position = Vector3.zero;
        }
    }
}
