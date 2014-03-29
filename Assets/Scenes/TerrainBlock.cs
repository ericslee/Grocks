﻿using UnityEngine;
using System.Collections;

public class TerrainBlock : MonoBehaviour {

	public int HP;
	private float HPmax;
	private GameObject sitter;
	public GameObject rock_HitPrefab;

	// Use this for initialization
	void Start () {
		HPmax = HP;
		if (HP == 0) HPmax = 1;
	}
	
	// Update is called once per frame
	void Update () {
		// presumably we'll put differing render code here?
		// right now the block will just get darker the more times it's struck
		gameObject.renderer.material.color = new Color(HP/HPmax,HP/HPmax,HP/HPmax);

		if (HP == 0) {
			Destroy (gameObject);
			sitter.GetComponent<Player>().setInAir(true);
			sitter.GetComponent<Player>().invertDir();
		}
	}

	void OnCollisionEnter(Collision hit) {
		HP--;
		sitter = hit.gameObject;

		if(rock_HitPrefab) {
			foreach (ContactPoint contact in hit.contacts) {
				//Debug.DrawRay(contact.point, contact.normal, Color.white);
				GameObject rocks = (GameObject)Instantiate(rock_HitPrefab, contact.point, Quaternion.identity);
				Destroy(rocks, 1.0f);
			}
		}
	}
}