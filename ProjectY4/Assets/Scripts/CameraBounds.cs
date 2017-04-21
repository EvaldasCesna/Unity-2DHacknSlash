using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour {

    private BoxCollider2D camBounds;
    private CameraFollow mycam;
	// Use this for initialization
	void Start () {
        camBounds = GetComponent<BoxCollider2D>();
        mycam = FindObjectOfType<CameraFollow>();
        mycam.SetBounds(camBounds);
	}

}
