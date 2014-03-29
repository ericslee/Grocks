using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject restartMenuPrefab;
	private GameObject restartMenu;

	// Use this for initialization
	void Start () {

	}

	// TODO: here is where you would reset the game state after someone replays or on init
	public void Reset() {
		Debug.Log ("Game reset");
		DestroyRestartMenu();
	}
	
	void CreateRestartMenu() {
		restartMenu = (GameObject)(GameObject.Instantiate(restartMenuPrefab));
	}

	void DestroyRestartMenu() {
		Destroy(restartMenu);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
