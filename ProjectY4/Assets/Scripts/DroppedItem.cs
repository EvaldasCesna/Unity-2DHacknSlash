using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour {
    ItemsDatabase items;
    public int Id;
    SpriteRenderer sprite;
    // Use this for initialization
    void Start () {
        sprite = GetComponent<SpriteRenderer>();
        items = GetComponentInChildren<ItemsDatabase>();
        Id = Random.Range(0, 1);
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
