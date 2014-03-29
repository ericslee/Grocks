﻿using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	public GUISkin customSkin;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {

		GUI.skin = customSkin;

		// Make a background box

		//GUI.Box(new Rect(10,10,100,90), "START MENU");

		int screenCenterX = Screen.width/2;
		int screenCenterY = Screen.height/2;
		int buttonWidth = 100;
		int buttonHeight = 50;
		int titleWidth = 200;
		int titleHeight = 150;

		GUI.Label(new Rect (screenCenterX-titleWidth/2, screenCenterY-titleHeight/2, titleWidth, titleHeight), 
		          "GROCKS");

		if(GUI.Button(new Rect(screenCenterX-buttonWidth/2, 265, buttonWidth, buttonHeight), "Start")) {
			Application.LoadLevel("Arena_1");
		}

		if(GUI.Button(new Rect(screenCenterX-buttonWidth/2, 325, buttonWidth, buttonHeight), "Quit")) {
			Application.Quit();
		}
	}
}
