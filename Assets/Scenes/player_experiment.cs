using UnityEngine;
using System.Collections;

public class player_experiment : MonoBehaviour {
	private int dir; // -1 is facing upside down (on the ceiling), 1 is facing rightside up (on the floor)
	public bool inAir;
	private Vector3 leftSpeed = new Vector3(0, 0, -30.0f);
	private Vector3 rightSpeed = new Vector3(0, 0, 30.0f);
	public float bounceTimer;

	private KeyCode keyUp;
	private KeyCode keyDown;
	private KeyCode keyRight;
	private KeyCode keyLeft;

	// Use this for initialization
	void Start () {
		dir = -1;
		inAir = false;
		bounceTimer = 0;

		// Assign player keys
		if(tag == "Player1") {
			keyUp = KeyCode.W;
			keyDown = KeyCode.S;
			keyRight = KeyCode.D;
			keyLeft = KeyCode.A;
		}
		else if(tag == "Player2") {
			keyUp = KeyCode.UpArrow;
			keyDown = KeyCode.DownArrow;
			keyRight = KeyCode.RightArrow;
			keyLeft = KeyCode.LeftArrow;
		}
	}
	
	// Update is called once per frame
	void Update () {

		bounceTimer--;
		if (bounceTimer < 0)
			bounceTimer = 0.0f;

		if (!inAir) {
			rigidbody.velocity = Vector3.zero;
		}
		if (inAir) rigidbody.AddForce(0, dir * 300.0f, 0);

		// move left and right
		if (Input.GetKey (keyLeft) && inAir && bounceTimer < 1) {
			rigidbody.MovePosition(rigidbody.position + leftSpeed * Time.deltaTime);
			
			// remove horizontal velocity if player moves in air after collision
			rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
		}

		if (Input.GetKey (keyRight) && inAir && bounceTimer < 1) {
			rigidbody.MovePosition(rigidbody.position + rightSpeed * Time.deltaTime);
			
			// remove horizontal velocity if player moves in air after collision
			rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
		}

		if (Input.GetKeyDown (keyUp) && !inAir && dir < 0) {
			//rigidbody.AddForce(0, 500.0f, 0);
			dir = 1;
			inAir = true;
		}
		if (Input.GetKeyDown (keyDown) && !inAir && dir > 0) {
			//rigidbody.AddForce(0, -500.0f, 0);
			dir = -1;
			inAir = true;
		}
	}

	void OnCollisionEnter (Collision hit) {

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
	}

	void OnTriggerEnter (Collider other) {
		// bounce away
		if(other.tag == "LeftBouncer") {
			rigidbody.AddForce(0, 0.0f, -2000.0f);
			bounceTimer = 2000.0f;
		}
		else if (other.tag == "RightBouncer") {
			rigidbody.AddForce(0, 0.0f, 2000.0f);
			bounceTimer = 2000.0f;
		}
	}

	void OnCollisionExit (Collision collision) {
		inAir = true;
	}
}
