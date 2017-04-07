using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Equipment : MonoBehaviour {
    public static Equipment pEquipment;
    GameObject charPanel;
    GameObject slotPanel;
    ItemsDatabase items;
    public GameObject equipSlot;
    public GameObject equipItem;

    int slotAmount;
    public List<Item> equipment = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    //public class SyncListGameObject : SyncList<GameObject>
    //{
    //    protected override GameObject DeserializeItem(NetworkReader reader)
    //    {
    //        return reader.ReadGameObject();
    //    }

    //    protected override void SerializeItem(NetworkWriter writer, GameObject item)
    //    {
    //        writer.Write(item);
    //    }
    //}
    //public SyncListGameObject syncslots = new SyncListGameObject();

    private void Awake()
    {
        if (pEquipment == null)
            pEquipment = this;
        else if (pEquipment != this)
            Destroy(gameObject);
    }

    void Start ()
    {
        //Grabs the item List
      
        items = GetComponent<ItemsDatabase>();

        slotAmount = 6;
        charPanel = GameObject.Find("CharacterPanel");
        slotPanel = charPanel.transform.FindChild("EquipmentPanel").gameObject;
        for (int i = 0; i < slotAmount; i++)
        {
            equipment.Add(new Item());
            slots.Add(Instantiate(equipSlot));
            slots[i].GetComponent<InventorySlot>().id = i;
            slots[i].GetComponent<InventorySlot>().location = "Equipment";
            slots[i].transform.SetParent(slotPanel.transform);
            slots[i].GetComponent<RectTransform>().localScale = Vector3.one;
        }

        //PopulateEquip(0, 1, 3);
        charPanel.SetActive(false);
    }
    //private void Update()
    //{
    //    syncslots.Equals(slots);
    //}

    public void PopulateEquip(int id, int amount, int slot)
    {
       
        Item itemToAdd = items.GetItemByID(id);


        // Adds to equip at certain location/amount of
        equipment[slot] = itemToAdd;
        GameObject itemObj = Instantiate(equipItem);
        itemObj.GetComponent<ItemData>().item = itemToAdd;
        itemObj.GetComponent<ItemData>().amount = amount;
        itemObj.GetComponent<ItemData>().slot = slot;
        itemObj.GetComponent<ItemData>().location = "Equipment";
        itemObj.transform.SetParent(slots[slot].transform);
        itemObj.transform.position = slots[slot].transform.position;
        itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
        itemObj.name = itemToAdd.Title;

    }

    public List<ItemData> getAllitems()
    {
        List<ItemData> temp = new List<ItemData>();
        for (int i = 0; i < equipment.Count; i++)
        {
            if (equipment[i].ID != -1)
            {   //Gets all item info in the inventory
                temp.Add(slots[i].transform.GetChild(0).GetComponent<ItemData>());
            }
        }

        return temp;
    }    

}
