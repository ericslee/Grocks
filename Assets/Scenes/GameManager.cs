using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject replayMenuPrefab;
	private GameObject replayMenu;
	private GameObject player1;
	private GameObject player2;

	// Use this for initialization
	void Start () {
		replayMenu = (GameObject)(GameObject.Instantiate(replayMenuPrefab));
		player1 = GameObject.Find("PLAYER1");
		player2 = GameObject.Find("PLAYER2");
		HideReplayMenu();
		Reset();
	}

	// TODO: here is where you would reset the game state after someone replays or on init
	public void Reset() {
		Debug.Log ("Game reset");
		player1.transform.position = new Vector3(-41.0f, 4.0f, -16.0f);
		player2.transform.position = new Vector3(-41.0f, 4.0f, 16.0f);

		player_experiment p1Comp = player1.GetComponent<player_experiment>();
		player1.rigidbody.AddForce(0, 300.0f, 0);
		p1Comp.inAir = true;

		player_experiment p2Comp = player2.GetComponent<player_experiment>();
		player2.rigidbody.AddForce(0, -300.0f, 0);
		p2Comp.inAir = true;

		HideReplayMenu();
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
