using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	public GUISkin customSkin;

	int screenCenterX = Screen.width/2;
	int screenCenterY = Screen.height/2;
	int buttonWidth = 100;
	int buttonHeight = 50;
	int titleWidth = 235;
	int titleHeight = 150;

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
//
//		GUI.Label(new Rect (screenCenterX-titleWidth/2, screenCenterY-titleHeight/2, titleWidth, titleHeight), 
//		          "GROCKS");

		if(GUI.Button(new Rect(screenCenterX-buttonWidth/2, 2*Screen.height/3, buttonWidth, buttonHeight), "Start")) {
			Application.LoadLevel("Arena_Eric");
		}

		if(GUI.Button(new Rect(screenCenterX-buttonWidth/2, 2*Screen.height/3 + 60, buttonWidth, buttonHeight), "Quit")) {
			Application.Quit();
		}
	}
}
