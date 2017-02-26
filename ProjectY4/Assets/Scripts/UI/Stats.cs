using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Stats : NetworkBehaviour
{
    private Equipment equipment; 
    private GameObject stats;
    private string data;
    public int currentLevel;
    public int currentXp;
    public int[] levels;

    Item melee;

    Item ranged;

    Item magic;
    // Use this for initialization
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Stats");
        equipment = GameObject.Find("Inventory").GetComponent<Equipment>();

    }

    private void Update()
    {

        //if (!Network.isServer)
        //    CmdUpdateStats();
        //else
        //    RpcUpdateStats();
        //Later only update if something changes but works for now
          UpdateStats();
        //   levelUp();
    }

    //[Command]
    //public void CmdUpdateStats()
    //{
    //    RpcUpdateStats();
    //}

  //  [ClientRpc]
    public void UpdateStats()
    {
        
        int def = 0, str = 0, vit = 0;

        for (int i = 0; i < equipment.equipment.Count; i++)
        {
            def += equipment.equipment[i].Defence;
            str += equipment.equipment[i].Strength;
            vit += equipment.equipment[i].Vitality;
        }

        data = "  Strenght: " + str + "\n\n  Defence: " + def + "\n\n  Vitality: " + vit;
        stats.GetComponent<Text>().text = data;

    }

    public void levelUp()
    {
        if(currentXp >= levels[currentLevel])
        {
            currentLevel++;
        }
    }

    public void addXp(int xp)
    {
        currentXp += xp;
    }

    public Item getMelee()
    {
        melee = equipment.equipment[3];
        return melee;
    }

    public Item getMagic()
    {
        magic = equipment.equipment[1];
        return magic;
    }

    public Item getRanged()
    {
        ranged = equipment.equipment[5];
        return ranged;
    }
    //pa.setSprites(equipment[1].Sprite, equipment[3].Sprite, equipment[5].Sprite);

}
