using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerName : MonoBehaviour {

    public Text name;

	// Use this for initialization
	void Start () {
        name.text = PlayerPrefs.GetString("Player Name");
        Debug.Log(PlayerPrefs.GetString("Player Name"));
	}
	
}
