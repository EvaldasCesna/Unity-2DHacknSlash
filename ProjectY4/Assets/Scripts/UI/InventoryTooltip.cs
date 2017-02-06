using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTooltip : MonoBehaviour {
    private Item item;
    private string data;
    private GameObject tooltip;

    private void Start()
    {
        tooltip = GameObject.Find("Tooltip");
        tooltip.SetActive(false);
      
    }

    private void Update()
    {
        if (tooltip.activeSelf)
        {
            tooltip.transform.position = Input.mousePosition;
        }
    }

    public void Activate(Item item)
    {
        this.item = item;
        ConstructDataString();
        tooltip.SetActive(true);
    }

    public void Deactivate()
    {
        tooltip.SetActive(false);
    }

    public void ConstructDataString()
    {
        //More to add here for different item types
        data = "<color=blue><b>" + item.Title + "</b></color>\n\n" + item.Description + "\n Strength:" + item.Strength +"\n Defence:" + item.Defence + "\n Vitality:" + item.Vitality + "\n Cost:" + item.Value;
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
    }


}
