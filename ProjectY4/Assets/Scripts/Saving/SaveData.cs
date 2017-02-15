using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    // public Character player;
    GameObject slotPanel;

    private List<SaveItems> save = new List<SaveItems>();
    private JsonData saveData;
    private Inventory inventory;
    private Equipment equipment;
    string InvLoc;
    string EquipLoc;
    static readonly string SAVEIN = "Inventory.json";
    static readonly string SAVEEQ = "Equipment.json";
    private string SaveInventoryUrl = "http://188.141.5.218/Save.php";

    // Use this for initialization
    void Start()
    {
        save.Clear();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        equipment = GameObject.Find("Inventory").GetComponent<Equipment>();
        InvokeRepeating("SaveItems", 10.0f, 10.0f);

    }

    void SaveItems()
    {
        //Get items from inventory/equipment
        List<ItemData> tempInv = inventory.getAllitems();
        List<ItemData> tempEquip = equipment.getAllitems();
        //Clears data so it doesnt duplicate items
        save.Clear();
        for (int i = 0; i < tempInv.Count; i++)
        {
            //Gathers info from inventory and saves it to json
            save.Add(new SaveItems(tempInv[i].item.ID, tempInv[i].amount, tempInv[i].slot));
        }

        saveData = JsonMapper.ToJson(save);
        InvLoc = Path.Combine(Application.persistentDataPath, SAVEIN);
        //Write into json save
        File.WriteAllText(InvLoc, saveData.ToString());

        save.Clear();
        for (int i = 0; i < tempEquip.Count; i++)
        {
            //Gathers info from inventory and saves it to json
            save.Add(new SaveItems(tempEquip[i].item.ID, tempEquip[i].amount, tempEquip[i].slot));
        }

        saveData = JsonMapper.ToJson(save);
        EquipLoc = Path.Combine(Application.persistentDataPath, SAVEEQ);
        //Write into json save
        File.WriteAllText(EquipLoc, saveData.ToString());

        //Start to write into database
        StartCoroutine("SaveItemsTodb");
    }

    IEnumerator SaveItemsTodb()
    {

        while (true)
        {
            //  Debug.Log("Success1");
            WWWForm Form = new WWWForm();
            InvLoc = Path.Combine(Application.persistentDataPath, SAVEIN);
            EquipLoc = Path.Combine(Application.persistentDataPath, SAVEEQ);

            Form.AddField("Username", PlayerPrefs.GetString("Player Name"));
            Form.AddField("Inventory", File.ReadAllText(InvLoc));
            Form.AddField("Equipment", File.ReadAllText(EquipLoc));

            WWW SaveWWW = new WWW(SaveInventoryUrl, Form);

            yield return SaveWWW;

            if (SaveWWW.error != null)
            {
                Debug.LogError("Cannot Connect to db");
            }
            else
            {
                //   Debug.Log(SaveWWW.text);
                string SaveReturn = SaveWWW.text;

            }
            yield return new WaitForSeconds(10f);
        }


    }


}

public class SaveItems
{
    public int id;
    public int amount;
    public int slot;

    public SaveItems(int id, int amount, int slot)
    {
        this.id = id;
        this.amount = amount;
        this.slot = slot;
    }
}
