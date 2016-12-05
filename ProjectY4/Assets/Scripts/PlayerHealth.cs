using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour {
    public const int maxHealth = 100;
    [SyncVar (hook = "OnChangeHealth")] public int currentHealth = maxHealth;
    public RectTransform healthbar;

    public void TakeDamage(int amount)
    {
        if(!isServer)
        {
            return;
        }

        currentHealth -= amount;
        if(currentHealth <=0)
        {
            currentHealth = maxHealth;
            RpcRespawn();
 
        }


    }
    //Sync healthbar to damage by server
    void OnChangeHealth(int health)
    {
        healthbar.sizeDelta = new Vector2(health, healthbar.sizeDelta.y);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if(isLocalPlayer)
        {
            transform.position = Vector3.zero;
        }
    }
}
