using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    private string LoadUrl = "http://188.141.5.218/Load.php";

    private Inventory inventory;
    private JsonData itemData;

    static readonly string SAVE = "Inventory.json";
    void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        StartCoroutine("LoadItemsFromdb");
    }

    void ConstructItems()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            //  Debug.Log(i + " id " + (int)itemData[i]["id"]);
            inventory.PopulateInv((int)itemData[i]["id"], (int)itemData[i]["amount"], (int)itemData[i]["slot"]);
          //  inventory.AddItem((int)itemData[i]["id"]);
        }
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
            string filename = Path.Combine(Application.persistentDataPath, SAVE);
            itemData = JsonMapper.ToObject(File.ReadAllText(filename));
            ConstructItems();
        }
        else
        {
   
             Debug.Log(LoadWWW.text);
            itemData = JsonMapper.ToObject(LoadWWW.text);
            ConstructItems();
        }
    }

}