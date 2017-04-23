using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NpcScript : NetworkBehaviour
{
    Inventory inv;
    Stats stats;
    GameObject player;

    public GameObject ui;
    public Button item1;
    public Button item2;
    public Button item3;

    // Use this for initialization
    void Start()
    {
        stats = Stats.pStats;
        inv = Inventory.pInventory;
    }

    public void buyItem(int id)
    {
        if (!player.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            return;
        }
        CmdbuyItem(id);
    }

    [Command]
    public void CmdbuyItem(int id)
    {
        int gold = int.Parse(stats.gold.text);
        if ((gold - 40) >= 0)
        {
            inv.AddItem(id);
            gold -= 40;
            stats.gold.text = gold.ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Network Identity from collided player
        if (collision.tag == "Player" && collision.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            player = collision.gameObject;
            ui.SetActive(true);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            ui.SetActive(false);
        }
    }
}
