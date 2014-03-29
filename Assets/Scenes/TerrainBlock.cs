using UnityEngine;
using System.Collections;

public class TerrainBlock : MonoBehaviour {

	public int HP;
	private float HPmax;

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
		}
	}

	void OnCollisionEnter(Collision youHitMe) {
		HP--;
	}
}