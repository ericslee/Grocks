using UnityEngine;
using System.Collections;

public class box_collision_experiment : MonoBehaviour {

	public int HP;
	private float HPmax;
	private GameObject sitter;
	
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
	}
}
