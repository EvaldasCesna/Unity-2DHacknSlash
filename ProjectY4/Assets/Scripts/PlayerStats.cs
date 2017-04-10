using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class PlayerStats : NetworkBehaviour {
    private Stats stats;
    private PlayerAttack pa;
    private PlayerHealth pHp;
    public int Gold;
    // Use this for initialization
    void Start () {
        //if (!isLocalPlayer)
        //{
        //    return;
        //}
        stats = Stats.pStats;
        stats.Show();
        pa = GetComponent<PlayerAttack>();
        pHp = GetComponent<PlayerHealth>();
        DontDestroyOnLoad(transform.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        levelUpdate();

        updateSprites();
    }

    void updateGold()
    {
        stats.UpdateGold(Gold);
    }

    void levelUpdate()
    {
        if (isLocalPlayer)
        {

            if (stats.leveledUp)
            {
                pHp.ChangeMaxHealth(stats.getBonusHealth() + stats.currentLevel);
                pHp.updateMaxHealth();
            }
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

}
