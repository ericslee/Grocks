using UnityEngine;
using System.Collections;

public class AboutMenu : MonoBehaviour {

	public GUISkin customSkin;

	int screenCenterX = Screen.width/2;
	int screenCenterY = Screen.height/2;
	int buttonWidth = 100;
	int buttonHeight = 50;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {

		GUI.skin = customSkin;

		// HORIZONTAL BUTTON LAYOUT
		if(GUI.Button(new Rect(10, 10, buttonWidth, buttonHeight), "Back")) {
			Application.LoadLevel("StartMenu_0");
		}
		
		if(GUI.Button(new Rect(Screen.width - buttonWidth - 10, 10, buttonWidth, buttonHeight), "Start")) {
			Application.LoadLevel("Arena_Eric");
		}
	}
}
