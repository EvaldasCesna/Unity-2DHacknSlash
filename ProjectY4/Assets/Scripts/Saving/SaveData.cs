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
    public GameObject invItem;
    string filename;
    static readonly string SAVE = "Inventory.json";
    private string SaveInventoryUrl = "http://188.141.5.218/Save.php";
 
    // Use this for initialization
    void Start()
    {
      //  slotPanel = GameObject.Find("SlotPanel").gameObject;
        slotPanel = GameObject.FindGameObjectWithTag("SlotPanel");
       // SaveItems();
        InvokeRepeating("SaveItems", 10.0f, 10.0f);
  
    }

    void SaveItems()
    {
        save.Clear();

        StartCoroutine("SaveItemsTodb");

        for (int i = 0; i < slotPanel.GetComponentsInChildren<ItemData>().Length; i++)
        {
          //  Debug.Log("ID: " + slotPanel.GetComponentsInChildren<ItemData>()[i].item.ID + " Amount: " + slotPanel.GetComponentsInChildren<ItemData>()[i].amount + " Slot: " + slotPanel.GetComponentsInChildren<ItemData>()[i].slot);
            //Gathers info from inventory and saves it to json
            save.Add(new SaveItems(slotPanel.GetComponentsInChildren<ItemData>()[i].item.ID, slotPanel.GetComponentsInChildren<ItemData>()[i].amount, slotPanel.GetComponentsInChildren<ItemData>()[i].slot));

        }

        saveData = JsonMapper.ToJson(save);
        filename = Path.Combine(Application.persistentDataPath, SAVE);

        File.WriteAllText(filename, saveData.ToString());


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
