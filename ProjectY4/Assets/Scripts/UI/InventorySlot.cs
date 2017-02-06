using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler {

    public int id;
    public string location;
    private Inventory inventory;
    private Equipment equipment;

     void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        equipment = GameObject.Find("Inventory").GetComponent<Equipment>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
        //Depending on which window it drops on do..
        if (location == "Inventory")
        {
      
            if (inventory.inventory[id].ID == -1)
            {
                //Clears the slot that the item is taken out of
                if(droppedItem.location == "Inventory")
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
            }

            else if (droppedItem.slot != id)
            {
                Debug.Log(this.transform.GetChild(0));
                //Item pointed at below the dropped item
                Transform item = this.transform.GetChild(0);

                //Sets the item below to position where the picked up item is
                item.GetComponent<ItemData>().slot = droppedItem.slot;
                item.transform.SetParent(inventory.slots[droppedItem.slot].transform);
                item.transform.position = inventory.slots[droppedItem.slot].transform.position;

                inventory.inventory[droppedItem.slot] = item.GetComponent<ItemData>().item;
                inventory.inventory[id] = droppedItem.item;
                droppedItem.slot = id;
            }
        }

        else if (location == "Equipment")
        {
      
            if (equipment.equipment[id].ID == -1)
            {
          
                inventory.inventory[droppedItem.slot] = new Item();
     

                /* need to make it so different types are equipabble to different spots
                if (droppedItem.item.Type == "Weapon")
                {

                    equipment.equipment[2] = droppedItem.item;
                    droppedItem.slot = 2;
                    droppedItem.location = "Equipment";
                }
                */

                equipment.equipment[3] = droppedItem.item;
                droppedItem.slot = 3;
                droppedItem.location = "Equipment";


            }

            else if(droppedItem.slot != id)
            {

                Transform item = this.transform.GetChild(0);
             
                    item.GetComponent<ItemData>().slot = droppedItem.slot;
                    item.GetComponent<ItemData>().location = droppedItem.location;
                    item.transform.SetParent(inventory.slots[droppedItem.slot].transform);
                    item.transform.position = inventory.slots[droppedItem.slot].transform.position;
                    inventory.inventory[droppedItem.slot] = droppedItem.item;
                                    //3 for now while no type check function
                    equipment.equipment[3] = item.GetComponent<ItemData>().item;
                    droppedItem.slot = id;
                    droppedItem.location = location;

            }
        }

    }

}
