using UnityEngine;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {
    public Item item;
    public int amount;
    public int slot;
    public string location;


    private Equipment equipment;
    private Inventory inventory;
    private InventoryTooltip tooltip;
    private Vector2 offset;

    private void Start()
    {

        equipment = GameObject.Find("Inventory").GetComponent<Equipment>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        tooltip = inventory.GetComponent<InventoryTooltip>();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
            this.transform.SetParent(this.transform.parent.parent.parent.parent); //Puts it on top when dragging
            this.transform.position = eventData.position;
            GetComponent<CanvasGroup>().blocksRaycasts = false;

        }
    }

    public void OnDrag(PointerEventData eventData)
    {

      
        if (item != null)
        {
            this.transform.position = eventData.position - offset;
        }


    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Depending on where you are hovering do action
        if (location == "Inventory")
        {
            this.transform.SetParent(inventory.slots[slot].transform);
            this.transform.position = inventory.slots[slot].transform.position;
        }
        else if (location == "Equipment")
        {
            this.transform.SetParent(equipment.slots[slot].transform);
            this.transform.position = equipment.slots[slot].transform.position;
        }

        GetComponent<CanvasGroup>().blocksRaycasts = true;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.Activate(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
    }
}

