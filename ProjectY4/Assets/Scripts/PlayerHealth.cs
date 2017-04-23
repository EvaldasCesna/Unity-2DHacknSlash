using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour
{

    private NetworkStartPosition[] spawnPoints;
    private Stats stats;
    private Button potionButton;
    private Text potionText;

    [SyncVar(hook = "OnMaxHealthChanged")]
    public int maxHealth = 100;
    AudioSource sound;
    [SyncVar(hook = "OnHealthChanged")]
    public int currentHealth;
    public bool died = false;
    public int def;
    public int potions;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
        stats = Stats.pStats;
        currentHealth = maxHealth;
        potionButton = stats.GetComponentInChildren<Button>();
        potionText = potionButton.GetComponentInChildren<Text>();
        potionText.text = potions.ToString();
        potionButton.onClick.AddListener(UsePotion);

    }

    private void Update()
    {
        if (Input.GetButtonDown("Heal"))
        {
            UsePotion();
        }
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
        //Armor calculation and health take away
        sound.Play();
        currentHealth -= amount - (amount * stats.getBonusArmor() / maxHealth);
        if (currentHealth <= 0)
        {
            FindObjectOfType<AudioSource>().Play();
            died = true;
            currentHealth = maxHealth;
            RpcRespawn(died);
            return died;
        }

        return died;
    }

    public void HealPlayer(int amount)
    {
        if (!isLocalPlayer)
        {
            return;
        }
        CmdHeal(amount);
    }

    [Command]
    public void CmdHeal(int amount)
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

    public void UsePotion()
    {
        if (potions != 0 && currentHealth < maxHealth)
        {
            potions--;
            HealPlayer(100);
            potionText.text = potions.ToString();
        }
    }

    public void SetPotions(int amount)
    {
        if (!isLocalPlayer)
        {
            return;
        }
        potions = amount;
        potionText.text = potions.ToString();
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
        if (isLocalPlayer && died)
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
