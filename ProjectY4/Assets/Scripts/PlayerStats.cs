using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class PlayerStats : NetworkBehaviour {
    private Stats stats;
    private PlayerAttack pa;
    private PlayerHealth pHp;

    public string startPoint;
    public int Gold;

    void Start () {

        stats = Stats.pStats;
        stats.Show();
        pa = GetComponent<PlayerAttack>();
        pHp = GetComponent<PlayerHealth>();
        DontDestroyOnLoad(transform.gameObject);


    }
	
	void Update () {

        updateSprites();
        updateHealth();
    }

    void updateGold()
    {
        stats.UpdateGold(Gold);
    }

    void updateHealth()
    {

        if (!isLocalPlayer)
        {
            return;

        }
            pHp.CmdChangeMaxHealth(stats.getBonusHealth() + stats.currentLevel);

            if (stats.leveledUp)
            {
                pHp.HealPlayer(pHp.maxHealth);
            }
      
    }

    public void updateSprites()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (!Network.isServer)
        {
            pa.CmdSetEquipment(stats.getMelee().ID, stats.getRanged().ID, stats.getMagic().ID);
        }
        else
        {
            pa.RpcSetEquipment(stats.getMelee().ID, stats.getRanged().ID, stats.getMagic().ID);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gold")
        {
            Gold += collision.GetComponent<GoldScript>().value;
            updateGold();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "DroppedItem")
        {
            Inventory.pInventory.AddItem(collision.GetComponent<DroppedItem>().Id);
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }

    [Server]
    public void MoveTo(Vector3 position)
    {
        
        transform.position = position;
        RpcMoveTo(position);

    }

    [ClientRpc]
    public void RpcMoveTo(Vector3 position)
    {
        transform.position = position;
    }

    [Server]
    public void setLocation(string name)
    {
        startPoint = name;
        RpcsetLocation(name);
    }

    [ClientRpc]
    public void RpcsetLocation(string name)
    {
        startPoint = name;
    }



}
