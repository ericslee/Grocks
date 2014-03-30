using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Prefabs
	public GameObject replayMenuPrefab;
	public GameObject ceilingSetPrefab;
	public GameObject floorSetPrefab;
	public GameObject player1Prefab;
	public GameObject player2Prefab;
	public GameObject readyPrefab;
	public GameObject goPrefab;

	private GameObject replayMenu;
	private GameObject ceiling;
	private GameObject floor;

	private GameObject player1;
	private GameObject player2;

	private GameObject readyPlane;
	private GameObject goPlane;

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

		Invoke("ResetAnimation", 3);

		Invoke("ShowReady", 0.1f);
		Invoke("DestroyReady", 2.6f);
		Invoke("ShowGo", 2.7f);
		Invoke("DestroyGo", 3.1f);

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

	void ShowReady() {
		readyPlane = (GameObject)(GameObject.Instantiate(readyPrefab));
	}

	void DestroyReady() {
		Destroy(readyPlane);
	}

	void ShowGo() {
		goPlane = (GameObject)(GameObject.Instantiate(goPrefab));
	}

	void DestroyGo() {
		Destroy(goPlane);
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

	public void damagePropagate() {
		if (!player1.GetComponent<player_experiment>().inAir &&
		    !player2.GetComponent<player_experiment>().inAir) {
			Transform[] sittings;
			sittings = floor.GetComponentsInChildren<Transform>();
			for (int i = 0; i < sittings.Length; i++) {
				if (sittings[i].tag != "Floor") continue;
				if (sittings[i].GetComponent<box_collision_experiment>().sittingOnMe == null) continue;
				string tag = sittings[i].GetComponent<box_collision_experiment>().sittingOnMe.tag;
				if (tag == "Player1" || tag == "Player2") {
					sittings[i].GetComponent<box_collision_experiment>().manualDamage();
					if (sittings[i].GetComponent<box_collision_experiment>().HP == 0) {
						sittings[i].GetComponent<box_collision_experiment>().sittingOnMe.GetComponent<player_experiment>().inAir = true;
					}
				}
			}
			sittings = ceiling.GetComponentsInChildren<Transform>();
			for (int i = 0; i < sittings.Length; i++) {
				if (sittings[i].tag != "Ceiling") continue;
				if (sittings[i].GetComponent<box_collision_experiment>().sittingOnMe == null) continue;
				string tag = sittings[i].GetComponent<box_collision_experiment>().sittingOnMe.tag;
				if (tag == "Player1" || tag == "Player2") {
					sittings[i].GetComponent<box_collision_experiment>().manualDamage();
					if (sittings[i].GetComponent<box_collision_experiment>().HP == 0) {
						sittings[i].GetComponent<box_collision_experiment>().sittingOnMe.GetComponent<player_experiment>().inAir = true;
					}
				}
			}
		}
	}

	public void setCurrentWinner(string thisGuyWins) {
		if (!roundHasWinner) {
			roundHasWinner = true;
			if (currentRound == totalRounds) {
				replayMenu.GetComponent<ReplayMenu> ().SetCurrentWinner (thisGuyWins);
			} else {
				Invoke("Reset", 3);
			}
		}
	}
}
