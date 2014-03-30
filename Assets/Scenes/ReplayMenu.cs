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
	int titleWidth = 350;
	int titleHeight = 50;
	
	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

		//currentWinner = "shlane";
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

		//Debug.Log ("ON GUI " + currentWinner); 

		GUI.skin = customSkin;
		
		GUI.Label(new Rect (screenCenterX-titleWidth/2, screenCenterY-titleHeight/2, titleWidth, titleHeight), 
		          currentWinner + " wins!");
		
		if(GUI.Button(new Rect(screenCenterX-buttonWidth/2, screenCenterY+titleHeight/2 + buttonHeight/2, buttonWidth, buttonHeight), "Replay")) {
			gameManager.Reset();
		}
		
		if(GUI.Button(new Rect(screenCenterX-buttonWidth/2, screenCenterY+titleHeight/2 + buttonHeight/2 + 55, buttonWidth, buttonHeight), "Quit")) {
			Application.Quit();
		}
	}

	public void SetCurrentWinner(string thisGuyWins) {
		currentWinner = thisGuyWins;
		Debug.Log (currentWinner);
		Show ();
	}
}
