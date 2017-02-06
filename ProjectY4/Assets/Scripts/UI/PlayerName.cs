using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerName : MonoBehaviour {
  //  GameObject allUI;
    public new Text name;

    // Use this for initialization
    void Start () {
     //   allUI = GameObject.Find("UI");
        name.text = PlayerPrefs.GetString("Player Name");

       // allUI.SetActive(false);
	}
	
}
