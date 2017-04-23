using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    private string LoadUrl = "http://188.141.5.218/Load.php";
    private Inventory inventory;
    private Equipment equipment;
    private Stats stats;
    private JsonData invData;
    private JsonData equipData;
    private int gold;
    private int level;
    private int xp;
    private int mobs;
    private int bosses;
    //Json Files
    static readonly string SAVEIN = "Inventory.json";
    static readonly string SAVEEQ = "Equipment.json";

    void Start()
    {
        inventory = Inventory.pInventory;
        equipment = Equipment.pEquipment;
        stats = Stats.pStats;
        StartCoroutine("LoadItemsFromdb");
    }

    void ConstructItems()
    {
        //Populates the Inventory and Equipment from the database
        for (int i = 0; i < invData.Count; i++)
        {
            inventory.PopulateInv((int)invData[i]["id"], (int)invData[i]["amount"], (int)invData[i]["slot"]);
        }
        for (int i = 0; i < equipData.Count; i++)
        {
            equipment.PopulateEquip((int)equipData[i]["id"], (int)equipData[i]["amount"], (int)equipData[i]["slot"]);
        }
        //Sets Other Stats that are stored in the database
        stats.setLevel(level, xp);
        stats.initialiseKills(mobs, bosses);
        stats.UpdateGold(gold);
        stats.UpdateStats();
    }

    IEnumerator LoadItemsFromdb()
    {
        WWWForm Form = new WWWForm();
        Form.AddField("Username", PlayerPrefs.GetString("Player Name"));

        WWW LoadWWW = new WWW(LoadUrl, Form);
        yield return LoadWWW;
        if (LoadWWW.error != null)
        {
            Debug.LogError("Cannot Connect to DB");
            string invLoc = Path.Combine(Application.persistentDataPath, SAVEIN);
            invData = JsonMapper.ToObject(File.ReadAllText(invLoc));
            string equipLoc = Path.Combine(Application.persistentDataPath, SAVEEQ);
            equipData = JsonMapper.ToObject(File.ReadAllText(equipLoc));
        }
        else
        {
            string LogText = LoadWWW.text;
            string[] LogTextSplit = LogText.Split('*');
            invData = JsonMapper.ToObject(LogTextSplit[0]);
            equipData = JsonMapper.ToObject(LogTextSplit[1]);
            gold = int.Parse(LogTextSplit[2]);
            level = int.Parse(LogTextSplit[3]);
            xp = int.Parse(LogTextSplit[4]);
            mobs = int.Parse(LogTextSplit[5]);
            bosses = int.Parse(LogTextSplit[6]);
            ConstructItems();
        }
    }

}