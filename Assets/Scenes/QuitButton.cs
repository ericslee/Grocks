﻿using UnityEngine;
using System.Collections;

public class QuitButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnMouseDown() {
		Application.Quit();
		Debug.Log("Quitting");
	}

	// Update is called once per frame
	void Update () {
	
	}
}