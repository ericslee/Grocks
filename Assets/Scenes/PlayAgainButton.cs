using UnityEngine;
using System.Collections;

public class PlayAgainButton : MonoBehaviour {

	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
	}

	void OnMouseDown() {
		gameManager.Reset();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
