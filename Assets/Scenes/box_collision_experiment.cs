using UnityEngine;
using System.Collections;

public class box_collision_experiment : MonoBehaviour {

	public int HP;
	private float HPmax;
	private GameObject sitter;

	// Materials for floor
	public Material terrain1;
	public Material terrain2;
	public Material terrain3;
	public Material terrain4;

	// Materials for ceiling
	public Material terrain1_ceiling;
	public Material terrain2_ceiling;
	public Material terrain3_ceiling;
	public Material terrain4_ceiling;

	// Use this for initialization
	void Start () {
		HPmax = HP;
		if (HP == 0) HPmax = 1;
	}
	
	// Update is called once per frame
	void Update () {
		// presumably we'll put differing render code here?
		// right now the block will just get darker the more times it's struck
		//gameObject.renderer.material.color = new Color(HP/HPmax,HP/HPmax,HP/HPmax);

	}
	
	void OnCollisionEnter(Collision hit) {
		/* calculate difference between center of this and center of collision object */
		// get center point Ys
		sitter = hit.gameObject;
		if (HP == 0) {
			Destroy (gameObject);
			return;
		}

		float otherY = hit.transform.position.y;
		float thisY = this.gameObject.transform.position.y;
		// get heights of objects
		float otherH = hit.transform.localScale.y / 2;
		float thisH = this.transform.localScale.y / 2;
		
		bool collidingOnSide = false;
		
		if (Mathf.Abs(thisY - otherY) + 0.1 < thisH + otherH) {
			Debug.Log(thisH + otherH + " height difference"); // TODO
			Debug.Log(Mathf.Abs(thisY - otherY) + 0.1 + " centerpoint difference"); // TODO
			collidingOnSide = true;
		}

		if (!collidingOnSide) {
			HP--;
			sitter.GetComponent<player_experiment>().inAir = false;
		}

		// Change texture
		float percentHealthRemaining = (HP + 1) / (HPmax + 1);
		if(percentHealthRemaining > 0.5f && percentHealthRemaining <= 0.75f) {
			if(gameObject.tag == "Floor") renderer.material = terrain2;
			else renderer.material = terrain2_ceiling;
		}
		else if(percentHealthRemaining > 0.25f && percentHealthRemaining <= 0.5f) {
			if(gameObject.tag == "Floor") renderer.material = terrain3;
			else renderer.material = terrain3_ceiling;
		}
		else if(percentHealthRemaining > 0.0f && percentHealthRemaining <= 0.25f) {
			if(gameObject.tag == "Floor") renderer.material = terrain4;
			else renderer.material = terrain4_ceiling;
		}
	}
}
