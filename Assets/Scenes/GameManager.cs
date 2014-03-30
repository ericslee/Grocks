using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Prefabs
	public GameObject replayMenuPrefab;
	public GameObject ceilingSetPrefab;
	public GameObject floorSetPrefab;

	private GameObject replayMenu;
	private GameObject ceiling;
	private GameObject floor;

	private GameObject player1;
	private GameObject player2;

	public bool controlsFrozen;

	// Use this for initialization
	void Start () {
		replayMenu = (GameObject)(GameObject.Instantiate(replayMenuPrefab));
		player1 = GameObject.FindWithTag("Player1");
		player2 = GameObject.FindWithTag("Player2");

		controlsFrozen = true;
		HideReplayMenu();

		Reset();
	}

	// TODO: here is where you would reset the game state after someone replays or on init
	public void Reset() {
		Debug.Log ("Game reset");
		controlsFrozen = true;

		ceiling = (GameObject)(GameObject.Instantiate(ceilingSetPrefab));
		floor = (GameObject)(GameObject.Instantiate(floorSetPrefab));

		player1.transform.position = new Vector3(-41.0f, 4.0f, -16.0f);
		player2.transform.position = new Vector3(-41.0f, 4.0f, 16.0f);

		player_experiment p1Comp = player1.GetComponent<player_experiment>();
		p1Comp.Reset();
		player_experiment p2Comp = player2.GetComponent<player_experiment>();
		p2Comp.Reset();

		Invoke("ResetAnimation", 2);

		HideReplayMenu();
	}

	void ResetAnimation() {
		Invoke("UnfreezeControls", 1.5f);
		player_experiment p1Comp = player1.GetComponent<player_experiment>();
		player1.rigidbody.AddForce(0, 300.0f, 0);
		p1Comp.inAir = true;
		
		player_experiment p2Comp = player2.GetComponent<player_experiment>();
		player2.rigidbody.AddForce(0, -300.0f, 0);
		p2Comp.inAir = true;
	}

	void UnfreezeControls() {
		controlsFrozen = false;
	}

	void ShowReplayMenu() {
		replayMenu.GetComponent<ReplayMenu>().Show();
	}

	void HideReplayMenu() {
		replayMenu.GetComponent<ReplayMenu>().Hide();
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void setCurrentWinner(string thisGuyWins) {
		replayMenu.GetComponent<ReplayMenu> ().SetCurrentWinner (thisGuyWins);
		//ShowReplayMenu ();
	}
}
