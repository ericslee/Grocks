using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Prefabs
	public GameObject replayMenuPrefab;
	public GameObject ceilingSetPrefab;
	public GameObject floorSetPrefab;
	public GameObject player1Prefab;
	public GameObject player2Prefab;

	private GameObject replayMenu;
	private GameObject ceiling;
	private GameObject floor;

	private GameObject player1;
	private GameObject player2;

	private bool roundHasWinner;
	public bool controlsFrozen;
	public int totalRounds = 3;
	public int currentRound;

	// Use this for initialization
	void Start () {
		replayMenu = (GameObject)(GameObject.Instantiate(replayMenuPrefab));
		roundHasWinner = false;
		controlsFrozen = true;
		currentRound = 0;
		HideReplayMenu();

		Reset();
	}

	public void Reset() {
		currentRound++;
		Debug.Log ("Game reset");
		roundHasWinner = false;
		controlsFrozen = true;

		if (ceiling) Destroy(ceiling);
		if (floor) Destroy(floor);

		ceiling = (GameObject)(GameObject.Instantiate(ceilingSetPrefab));
		floor = (GameObject)(GameObject.Instantiate(floorSetPrefab));

		if (player1) Destroy(player1);
		if (player2) Destroy(player2);

		player1 = (GameObject)(GameObject.Instantiate(player1Prefab));
		player2 = (GameObject)(GameObject.Instantiate(player2Prefab));

		player1.transform.position = new Vector3(-41.0f, 4.0f, -16.0f);
		player2.transform.position = new Vector3(-41.0f, 4.0f, 16.0f);

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
		if (!roundHasWinner) {
			roundHasWinner = true;
			if (currentRound == totalRounds) {
				replayMenu.GetComponent<ReplayMenu> ().SetCurrentWinner (thisGuyWins);
			} else {
				Reset();
			}
		}
	}
}
