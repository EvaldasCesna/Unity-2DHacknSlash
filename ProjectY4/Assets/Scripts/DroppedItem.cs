using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    public int Id;
    SpriteRenderer sprite;
    ItemsDatabase items;
    // Use this for initialization
    void Start()
    {
        items = Inventory.pInventory.GetComponent<ItemsDatabase>();
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = items.GetItemByID(Id).Sprite;
    }

}
