using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		// Make a background box
		GUI.Box(new Rect(10,10,100,90), "START MENU");

		if(GUI.Button(new Rect(20,40,80,20), "Start")) {
			Application.LoadLevel("Arena_1");
		}

		if(GUI.Button(new Rect(20,70,80,20), "Quit")) {
			Application.Quit();
		}
	}
}
