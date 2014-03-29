using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject restartMenuPrefab;
	private GameObject restartMenu;

	// Use this for initialization
	void Start () {

	}

	// TODO: when brought into main arena, attach prefab to public var in inspector
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
