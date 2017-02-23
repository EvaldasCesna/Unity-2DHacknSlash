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
    private JsonData invData;
    private JsonData equipData;

    static readonly string SAVEIN = "Inventory.json";
    static readonly string SAVEEQ = "Equipment.json";
    void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        equipment = GameObject.Find("Inventory").GetComponent<Equipment>();
        StartCoroutine("LoadItemsFromdb");
    }

    void ConstructItems()
    {
        for (int i = 0; i < invData.Count; i++)
        {
            //  Debug.Log(i + " id " + (int)itemData[i]["id"]);
            inventory.PopulateInv((int)invData[i]["id"], (int)invData[i]["amount"], (int)invData[i]["slot"]);
          //  inventory.AddItem((int)itemData[i]["id"]);
        }
        for (int i = 0; i < equipData.Count; i++)
        {
            //  Debug.Log(i + " id " + (int)itemData[i]["id"]);
            equipment.PopulateEquip((int)equipData[i]["id"], (int)equipData[i]["amount"], (int)equipData[i]["slot"]);
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
            string invLoc = Path.Combine(Application.persistentDataPath, SAVEIN);
            invData = JsonMapper.ToObject(File.ReadAllText(invLoc));
            string equipLoc = Path.Combine(Application.persistentDataPath, SAVEEQ);
            equipData = JsonMapper.ToObject(File.ReadAllText(equipLoc));

            //ConstructItems();
        }
        else
        {
            string LogText = LoadWWW.text;
            string[] LogTextSplit = LogText.Split('*');
          //  Debug.Log(LogTextSplit[0]  +" 0  1 " +  LogTextSplit[1]);
            invData = JsonMapper.ToObject(LogTextSplit[0]);
            equipData = JsonMapper.ToObject(LogTextSplit[1]);
            ConstructItems();
        }
    }

}