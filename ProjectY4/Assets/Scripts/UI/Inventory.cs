using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    GameObject invPanel;
    GameObject slotPanel;
    ItemsDatabase items;
    public GameObject invSlot;
    public GameObject invItem;

    int slotAmount;
    public List<Item> inventory = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    private void Start()
    {
        //Grabs the item List
        items = GetComponent<ItemsDatabase>();

        slotAmount = 20;
        invPanel = GameObject.Find("InventoryPanel");
        slotPanel = invPanel.transform.FindChild("SlotPanel").gameObject;
        for(int i = 0; i < slotAmount; i++)
        {
            inventory.Add(new Item());
            slots.Add(Instantiate(invSlot));
            slots[i].GetComponent<InventorySlot>().id = i;
            slots[i].transform.SetParent(slotPanel.transform);
        }

        AddItem(0);

        AddItem(1);
    }


    public void AddItem(int id)
    {
        Item itemToAdd = items.GetItemByID(id);
        //Can be more efficient
        if (itemToAdd.Stackable && IsInInventory(itemToAdd))
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].ID == id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    break;
                }

            }

        }

        else
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].ID == -1)
                {
                    inventory[i] = itemToAdd;
                    GameObject itemObj = Instantiate(invItem);
                    itemObj.GetComponent<ItemData>().item = itemToAdd;
                    itemObj.GetComponent<ItemData>().amount = 1;
                    itemObj.GetComponent<ItemData>().slot = i;
                    itemObj.transform.SetParent(slots[i].transform);
                    itemObj.transform.position = Vector2.zero;
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    itemObj.name = itemToAdd.Title;
                    break;
                }
            }
        }
    }

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

    bool IsInInventory(Item item)
    {
        for(int i = 0; i< inventory.Count; i++)
            if (inventory[i].ID == item.ID)
                return true;
            return false;
    }
}
