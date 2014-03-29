using UnityEngine;
using System.Collections;

public class DEATHScript : MonoBehaviour {

	private GameManager manager;

	// Use this for initialization
	void Start () {
		// cache the game manager
		manager = GameObject.FindWithTag ("GameManager").GetComponent<GameManager>();;
		if (!manager) Debug.Log ("WHERE IS THE GAME MANAGER????????");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// For triggering a win
	void OnTriggerEnter(Collider other) {
		// this way will need to be changed if we want to have more than 2 players
		if(other.tag == "Player1") {
			Debug.Log("PLAYER 2 WINS");

			// Report winner and display replay GUI
			manager.setCurrentWinner("Player 2");

			// Destroy player 1

			// Make winner hop?

		}
		else if(other.tag == "Player2") {
			Debug.Log ("PLAYER 1 WINS");

			// Report winner and display replay GUI
			manager.setCurrentWinner("Player 1");
		}
	}
}
