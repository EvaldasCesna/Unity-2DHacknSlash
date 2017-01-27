using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler {

    public int id;
    private Inventory items;

     void Start()
    {
        items = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
        if (items.inventory[id].ID == -1)
        {
            //Clears the slot that the item is taken out of
            items.inventory[droppedItem.slot] = new Item();
            items.inventory[id] = droppedItem.item;
            droppedItem.slot = id;
        }
        else if (droppedItem.slot != id)
        {
            Transform item = this.transform.GetChild(0);
            item.GetComponent<ItemData>().slot = droppedItem.slot;
            item.transform.SetParent(items.slots[droppedItem.slot].transform);
            item.transform.position = items.slots[droppedItem.slot].transform.position;

            items.inventory[droppedItem.slot] = item.GetComponent<ItemData>().item;
            items.inventory[id] = droppedItem.item;
            droppedItem.slot = id;
        }
    }

}
