using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject replayMenuPrefab;
	private GameObject replayMenu;

	// Use this for initialization
	void Start () {
		replayMenu = (GameObject)(GameObject.Instantiate(replayMenuPrefab));
		HideReplayMenu();
	}

	// TODO: here is where you would reset the game state after someone replays or on init
	public void Reset() {
		Debug.Log ("Game reset");
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
}
