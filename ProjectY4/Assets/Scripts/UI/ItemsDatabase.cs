using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class ItemsDatabase : MonoBehaviour
{
    private List<Item> items = new List<Item>();
    private JsonData itemData;

    void Start()
    {
        //Android uses Different streamingAssets Location for the .json list of items
#if UNITY_STANDALONE || UNITY_WEBPLAYER
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        ConstructItems();
#else
        string jsonPath =  Application.streamingAssetsPath + "/Items.json";
        StartCoroutine(GetJsonData(jsonPath));
#endif

    }

    //Could use binary search if gets too big
    public Item GetItemByID(int id)
    {
        for (int i = 0; i < items.Count; i++)
            if (items[i].ID == id)
                return items[i];
        return null;
    }

    void ConstructItems()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            items.Add(new Item((int)itemData[i]["id"], itemData[i]["title"].ToString(), itemData[i]["type"].ToString(), (int)itemData[i]["value"],
                (int)itemData[i]["stats"]["attack"], (int)itemData[i]["stats"]["defence"], (int)itemData[i]["stats"]["vitality"],
                itemData[i]["description"].ToString(), (bool)itemData[i]["stackable"], (int)itemData[i]["rarity"], itemData[i]["slug"].ToString()));
        }
    }


    IEnumerator GetJsonData(string jsonUrl)
    {
        WWW www = new WWW(jsonUrl);

        yield return www;
        Debug.Log(www.text);
        itemData = JsonMapper.ToObject(www.text);
        ConstructItems();
    }

}

public class Item
{
    public int ID { get; private set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public int Value { get; set; }
    public int Strength { get; set; }
    public int Defence { get; set; }
    public int Vitality { get; set; }
    public string Description { get; set; }
    public bool Stackable { get; set; }
    public int Rarity { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set; }

    public Item(int id, string title, string type, int value, int strength, int defence, int vitality, string description, bool stackable, int rarity, string slug)
    {
        this.ID = id;
        this.Title = title;
        this.Type = type;
        this.Value = value;
        this.Strength = strength;
        this.Defence = defence;
        this.Vitality = vitality;
        this.Description = description;
        this.Stackable = stackable;
        this.Rarity = rarity;
        this.Slug = slug;
        this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + slug);
    }

    public Item()
    {
        this.ID = -1;
    }
}