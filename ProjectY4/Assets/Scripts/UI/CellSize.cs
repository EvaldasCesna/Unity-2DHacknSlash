using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellSize : MonoBehaviour {

    public float width;
    // Use this for initialization
    void Update()
    {
        width = this.gameObject.GetComponent<RectTransform>().rect.width;
      
            Vector2 newSize = new Vector2(width / 4, width / 4);
            this.gameObject.GetComponent<GridLayoutGroup>().cellSize = newSize;
        
    }
}
