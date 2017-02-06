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
    private string inventoryFromdb;
    static readonly string SAVE = "Inventory.json";
    void Start()
    {
        StartCoroutine("LoadItemsFromdb");
        string filename = Path.Combine(Application.persistentDataPath, SAVE);

        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        //itemData = JsonMapper.ToObject(File.ReadAllText(filename));
     //   itemData = JsonMapper.ToObject(inventoryFromdb);
      //  ConstructItems();
       
    }




    void ConstructItems()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
          //  Debug.Log(i + " id " + (int)itemData[i]["id"]);
            inventory.AddItem((int)itemData[i]["id"]);

          //  (int)itemData[i]["id"], (int)itemData[i]["amount"], (int)itemData[i]["slot"]));
        }
    }


    IEnumerator LoadItemsFromdb()
    {
        //   Debug.Log("Attempting Log in");
        WWWForm Form = new WWWForm();
        Form.AddField("Username", PlayerPrefs.GetString("Player Name"));

        WWW LoadWWW = new WWW(LoadUrl, Form);
        yield return LoadWWW;
        if (LoadWWW.error != null)
        {
            Debug.LogError("Cannot Connect to Login");
            string filename = Path.Combine(Application.persistentDataPath, SAVE);
            itemData = JsonMapper.ToObject(File.ReadAllText(filename));
            ConstructItems();
        }
        else
        {
            inventoryFromdb = LoadWWW.text;
             //Debug.Log(inventoryFromdb);
            itemData = JsonMapper.ToObject(inventoryFromdb);
            ConstructItems();
        }
    }

}