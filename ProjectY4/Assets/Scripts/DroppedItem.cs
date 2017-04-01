using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour {

    public int Id;
    SpriteRenderer sprite;
    ItemsDatabase items;
    // Use this for initialization
    void Start () {
        items = GameObject.Find("Inventory").GetComponent<ItemsDatabase>();
        sprite = GetComponent<SpriteRenderer>();

        Id = Random.Range(0, 4);
        //  items.GetItemByID(Id);
        //  Debug.Log(items.GetItemByID(Id).ID);
        sprite.sprite = items.GetItemByID(Id).Sprite;

    }
	
	// Update is called once per frame
	void Update () {
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameObject.Find("Inventory").GetComponent<Inventory>().AddItem(Id);
            Destroy(gameObject);
        }
    }
}
