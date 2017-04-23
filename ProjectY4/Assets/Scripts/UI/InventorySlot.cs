using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{           //possibly enum was better
    private string[] EquipType = { "Top", "Magic", "Mid", "Melee", "Bot", "Ranged" };
    public int id;
    public string location;
    private Inventory inventory;
    private Equipment equipment;
    private SaveData save;

    void Start()
    {
        inventory = Inventory.pInventory;
        save = inventory.GetComponent<SaveData>();
        equipment = Equipment.pEquipment;
    }

    public void OnDrop(PointerEventData eventData)
    {

        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
        //Depending on which window it drops on do..
        if (location == "Inventory" && droppedItem != null)
        {

            if (inventory.inventory[id].ID == -1)
            {
                //Clears the slot that the item is taken out of
                if (droppedItem.location == "Inventory")
                {
                    inventory.inventory[droppedItem.slot] = new Item();
                }
                else
                {
                    equipment.equipment[droppedItem.slot] = new Item();
                }
                inventory.inventory[id] = droppedItem.item;
                droppedItem.slot = id;
                droppedItem.location = "Inventory";
                Stats.pStats.UpdateStats();
            }

            else if (droppedItem.slot != id)
            {
                //Item pointed at below the dropped item
                Transform item = this.transform.GetChild(0);
                if (droppedItem.location == "Inventory")
                {
                    //Sets the item below to position where the picked up item is
                    item.GetComponent<ItemData>().slot = droppedItem.slot;
                    item.transform.SetParent(inventory.slots[droppedItem.slot].transform);
                    item.transform.position = inventory.slots[droppedItem.slot].transform.position;
                    inventory.inventory[droppedItem.slot] = item.GetComponent<ItemData>().item;
                    inventory.inventory[id] = droppedItem.item;
                    droppedItem.slot = id;
                }
                else
                {
                    // Some problems could occur here
                    for (int i = 0; i < EquipType.Length; i++)
                    {
                        if (inventory.inventory[id].Type == EquipType[i])
                        {
                            item.GetComponent<ItemData>().slot = droppedItem.slot;
                            item.GetComponent<ItemData>().location = droppedItem.location;
                            item.transform.SetParent(equipment.slots[droppedItem.slot].transform);
                            item.transform.position = equipment.slots[droppedItem.slot].transform.position;
                            equipment.equipment[i] = droppedItem.item;
                            equipment.equipment[i] = item.GetComponent<ItemData>().item;
                            droppedItem.slot = id;
                            droppedItem.location = location;
                        }
                    }
                }
            }
        }

        else if (location == "Equipment")
        {
            // Add code here to not fill the same spot
            if (equipment.equipment[id].ID == -1 && droppedItem != null)
            {
                if (droppedItem.location == "Equipment")
                {
                    equipment.equipment[droppedItem.slot] = new Item();
                }
                else
                {
                    inventory.inventory[droppedItem.slot] = new Item();
                }
                // Make this into check what type of equipment it is 
                for (int i = 0; i < EquipType.Length; i++)
                {
                    if (droppedItem.item.Type == EquipType[i] && equipment.equipment[i].ID == -1)
                    {
                        equipment.equipment[i] = droppedItem.item;
                        droppedItem.slot = i;
                        droppedItem.location = "Equipment";
                        Stats.pStats.UpdateStats();
                    }
                }
            }

            else if (droppedItem.slot != id)
            {
                Transform item = this.transform.GetChild(0);

                item.GetComponent<ItemData>().slot = droppedItem.slot;
                item.GetComponent<ItemData>().location = droppedItem.location;
                item.transform.SetParent(inventory.slots[droppedItem.slot].transform);
                item.transform.position = inventory.slots[droppedItem.slot].transform.position;
                inventory.inventory[droppedItem.slot] = droppedItem.item;

                for (int i = 0; i < EquipType.Length; i++)
                {
                    if (droppedItem.item.Type == EquipType[i])
                    {
                        equipment.equipment[i] = item.GetComponent<ItemData>().item;
                    }
                }
                droppedItem.slot = id;
                droppedItem.location = location;
            }
        }
        Invoke("Isave", 0.1f);
    }

    void Isave()
    {
        save.SaveItems();
    }

}
