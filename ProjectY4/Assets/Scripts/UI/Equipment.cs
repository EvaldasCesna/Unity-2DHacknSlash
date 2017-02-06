using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour {
    GameObject charPanel;
    GameObject slotPanel;
  //  ItemsDatabase items;
    public GameObject equipSlot;
    public GameObject equipItem;

    int slotAmount;
    public List<Item> equipment = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();


    void Start ()
    {
        //Grabs the item List
        
      //  items = GetComponent<ItemsDatabase>();

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
        }
        charPanel.SetActive(false);
    }
	
}
