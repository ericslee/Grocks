using UnityEngine;
using System.Collections;

public class ReplayMenu : MonoBehaviour {
	
	public GUISkin customSkin;
	private GameManager gameManager;
	private string currentWinner; 

	int screenCenterX = Screen.width/2;
	int screenCenterY = Screen.height/2;
	int buttonWidth = 100;
	int buttonHeight = 50;
	int titleWidth = 335;
	int titleHeight = 150;
	
	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

		currentWinner = "shlane";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Show() {
		enabled = true;
	}

	public void Hide() {
		enabled = false;
	}
	
	void OnGUI() {
		
		GUI.skin = customSkin;
		
		GUI.Label(new Rect (screenCenterX-titleWidth/2, screenCenterY-titleHeight/2, titleWidth, titleHeight), 
		          currentWinner + " wins!");
		
		if(GUI.Button(new Rect(screenCenterX-buttonWidth/2, 265, buttonWidth, buttonHeight), "Replay")) {
			gameManager.Reset();
		}
		
		if(GUI.Button(new Rect(screenCenterX-buttonWidth/2, 325, buttonWidth, buttonHeight), "Quit")) {
			Application.Quit();
		}
	}

	public void SetCurrentWinner(string thisGuyWins) {
		currentWinner = thisGuyWins;
		Debug.Log (currentWinner);
		Show ();
	}
}
