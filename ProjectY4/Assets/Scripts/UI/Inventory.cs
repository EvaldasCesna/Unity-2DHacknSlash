﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory pInventory;
    GameObject invPanel;
    GameObject slotPanel;
    ItemsDatabase items;
    public GameObject invSlot;
    public GameObject invItem;

    int slotAmount;
    public List<Item> inventory = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    private void Awake()
    {
        if (pInventory == null)
            pInventory = this;
        else if (pInventory != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        //Grabs the item List
        items = GetComponent<ItemsDatabase>();
        slotAmount = 100;
        invPanel = GameObject.Find("InventoryPanel");
        slotPanel = GameObject.FindGameObjectWithTag("SlotPanel").gameObject; //The Prefabs are assigned

        for (int i = 0; i < slotAmount; i++)
        {
            inventory.Add(new Item());
            slots.Add(Instantiate(invSlot));
            slots[i].GetComponent<InventorySlot>().id = i;
            slots[i].GetComponent<InventorySlot>().location = "Inventory";
            slots[i].transform.SetParent(slotPanel.transform);
            slots[i].GetComponent<RectTransform>().localScale = Vector3.one;
        }
        invPanel.SetActive(false);
    }


    public bool AddItem(int id)
    {
        Item itemToAdd = items.GetItemByID(id);
        if (itemToAdd.Stackable && IsInInventory(itemToAdd))
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].ID == id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();

                    return true;
                }
            }
        }
        else
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].ID == -1) // IF Empty
                {
                    //Having this as a separate function would be more efficient
                    inventory[i] = itemToAdd;  // Adds the item to inventory
                    GameObject itemObj = Instantiate(invItem); //This shows the item is added visually
                    itemObj.GetComponent<ItemData>().item = itemToAdd;
                    itemObj.GetComponent<ItemData>().amount = 1;
                    itemObj.GetComponent<ItemData>().slot = i;
                    itemObj.GetComponent<ItemData>().location = "Inventory";
                    itemObj.transform.SetParent(slots[i].transform);
                    itemObj.transform.position = slots[i].transform.position;
                    itemObj.transform.localScale = Vector3.one;
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    itemObj.name = itemToAdd.Title;
                    return true;
                }
            }
        }
        return false;
    }
    //Remove might not be working correctly
    public void RemoveItem(int id)
    {
        Item itemToRemove = items.GetItemByID(id);
        if (itemToRemove.Stackable && IsInInventory(itemToRemove))
        {
            for (int j = 0; j < inventory.Count; j++)
            {
                if (inventory[j].ID == id)
                {
                    ItemData data = slots[j].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount--;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    if (data.amount == 0)
                    {
                        Destroy(slots[j].transform.GetChild(0).gameObject);
                        inventory[j] = new Item();
                        break;
                    }
                    if (data.amount == 1)
                    {
                        slots[j].transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "";
                        break;
                    }
                    break;
                }
            }
        }
        else for (int i = 0; i < inventory.Count; i++)
                if (inventory[i].ID != -1 && inventory[i].ID == id)
                {
                    Destroy(slots[i].transform.GetChild(0).gameObject);
                    inventory[i] = new Item();
                    break;
                }
    }

    public void PopulateInv(int id, int amount, int slot)
    {
        Item itemToAdd = items.GetItemByID(id);

        // Adds to inventory at certain location/amount of
        inventory[slot] = itemToAdd;
        GameObject itemObj = Instantiate(invItem);
        itemObj.GetComponent<ItemData>().item = itemToAdd;
        itemObj.GetComponent<ItemData>().amount = amount;
        itemObj.GetComponent<ItemData>().slot = slot;
        itemObj.GetComponent<ItemData>().location = "Inventory";
        itemObj.transform.SetParent(slots[slot].transform);
        itemObj.transform.position = slots[slot].transform.position;
        itemObj.transform.localScale = Vector3.one;
        itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
        itemObj.name = itemToAdd.Title;
    }


    bool IsInInventory(Item item)
    {
        for (int i = 0; i < inventory.Count; i++)
            if (inventory[i].ID == item.ID)
                return true;
        return false;
    }

    public List<ItemData> getAllitems()
    {
        List<ItemData> temp = new List<ItemData>();
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].ID != -1)
            {   //Gets all item info in the inventory
                temp.Add(slots[i].transform.GetChild(0).GetComponent<ItemData>());
            }
        }
        return temp;
    }
}
