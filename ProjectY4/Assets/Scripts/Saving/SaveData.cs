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
    public Inventory inventory;
    string filename;
    static readonly string SAVE = "Inventory.json";
    private string SaveInventoryUrl = "http://188.141.5.218/Save.php";

    // Use this for initialization
    void Start()
    {
        save.Clear();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        InvokeRepeating("SaveItems", 10.0f, 10.0f);

    }

    void SaveItems()
    {
        //Get items from inventory
        List<ItemData> temp = inventory.getAllitems();
        //Clears data so it doesnt duplicate items
        save.Clear();
        for (int i = 0; i < temp.Count; i++)
        {
            //Gathers info from inventory and saves it to json
            save.Add(new SaveItems(temp[i].item.ID, temp[i].amount, temp[i].slot));
        }

        saveData = JsonMapper.ToJson(save);
        filename = Path.Combine(Application.persistentDataPath, SAVE);
        //Write into json save
        File.WriteAllText(filename, saveData.ToString());
        
        //Start to write into database
        StartCoroutine("SaveItemsTodb");
    }

    IEnumerator SaveItemsTodb()
    {

        while (true)
        {
            //  Debug.Log("Success1");
            WWWForm Form = new WWWForm();
            filename = Path.Combine(Application.persistentDataPath, SAVE);
            Form.AddField("Username", PlayerPrefs.GetString("Player Name"));
            Form.AddField("Inventory", File.ReadAllText(filename));

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
