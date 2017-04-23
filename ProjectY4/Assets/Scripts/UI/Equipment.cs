using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Equipment : MonoBehaviour
{
    private GameObject charPanel;
    private GameObject slotPanel;
    private ItemsDatabase items;
    private int slotAmount;

    public static Equipment pEquipment;
    public GameObject equipSlot;
    public GameObject equipItem;
    public List<Item> equipment = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    private void Awake()
    {
        //Used for the singleton
        if (pEquipment == null)
            pEquipment = this;
        else if (pEquipment != this)
            Destroy(gameObject);
    }

    void Start()
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

        charPanel.SetActive(false);
    }

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
        itemObj.transform.localScale = Vector3.one;
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
