using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class PlayerStats : NetworkBehaviour {
    public Stats stats;
    public PlayerAttack pa;

    public int Gold;
    // Use this for initialization
    void Start () {
        //if (!isLocalPlayer)
        //{
        //    return;
        //}

        pa = GetComponent<PlayerAttack>();
        stats = GameObject.FindGameObjectWithTag("UIGUI").GetComponent<Stats>();
    }
	
	// Update is called once per frame
	void Update () {

        if (!isLocalPlayer)
        {
            return;
        }

        if (!Network.isServer)
            pa.CmdSetSprites(stats.getMelee().ID, stats.getRanged().ID, stats.getMagic().ID);
        else
            pa.RpcSetSprites(stats.getMelee().ID, stats.getRanged().ID, stats.getMagic().ID);
    }

    void updateGold()
    {
        stats.UpdateGold(Gold);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gold")
        {
            Gold += collision.GetComponent<GoldScript>().value;
            updateGold();
            collision.gameObject.SetActive(false);
        }
    }

}
