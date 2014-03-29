using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Vector3 leftSpeed = new Vector3(0, 0, -30.0f);
	private Vector3 rightSpeed = new Vector3(0, 0, 30.0f);

	public bool inAir;
	public int dir; // -1 is facing upside down (on the ceiling), 1 is facing rightside up (on the floor)
	public bool changedGravity;
	private float bounceTimer;

	// Use this for initialization
	void Start () {
		inAir = false;
		dir = 1;
		changedGravity = false;
		bounceTimer = 0;
	}

	// Update is called once per frame
	void FixedUpdate () {
		bounceTimer--;
		if (bounceTimer < 0)
						bounceTimer = 0.0f;

		// prevent any movement except for changing gravity when not in the air
		if (!inAir) {
			rigidbody.velocity = Vector3.zero;
		}

		// constantly apply to vertical force when in the air
		if (inAir) {
			rigidbody.AddForce(0, dir * 20.0f, 0);
		}

		// add force on button press
		if (Input.GetKeyDown ("w") && !changedGravity && dir > 0) {
			rigidbody.AddForce(0, 500.0f, 0);
			changedGravity = true;
			inAir = true;
		}
		if (Input.GetKeyDown ("s") && !changedGravity && dir < 0) {
			rigidbody.AddForce(0, -500.0f, 0);
			changedGravity = true;
			inAir = true;
		}

		// move left and right
		if (Input.GetKey ("a") && inAir && bounceTimer < 1) {
			rigidbody.MovePosition(rigidbody.position + leftSpeed * Time.deltaTime);

			// remove horizontal velocity if player moves in air after collision
			rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
		}
		// move left and right
		if (Input.GetKey ("d") && inAir && bounceTimer < 1) {
			rigidbody.MovePosition(rigidbody.position + rightSpeed * Time.deltaTime);

			// remove horizontal velocity if player moves in air after collision
			rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
		}
	}

	void OnCollisionEnter (Collision collision) {
		/* calculate difference between center of this and center of collision object */
		// get center point Ys
		float otherY = collision.transform.position.y;
		float thisY = this.gameObject.transform.position.y;
		// get heights of objects
		float otherH = collision.transform.localScale.y / 2;
		float thisH = this.transform.localScale.y / 2;

		bool collidingOnSide = false;

		if (Mathf.Abs(thisY - otherY) + 0.1 < thisH + otherH) {
			Debug.Log(thisH + otherH + " height difference"); // TODO
			Debug.Log(Mathf.Abs(thisY - otherY) + 0.1 + " centerpoint difference"); // TODO
			collidingOnSide = true;

		}

		if (collision.gameObject.tag == "Ceiling" && !collidingOnSide) {
			dir = -1;
			changedGravity = false;
			inAir = false;
			//rigidbody.velocity = Vector3.zero;
		}
		else if (collision.gameObject.tag == "Floor" && !collidingOnSide) {
			dir = 1;
			changedGravity = false;
			inAir = false;
			//rigidbody.velocity = Vector3.zero;
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

	public void setInAir(bool isItInAir) {
		inAir = isItInAir;
	}

	public bool isInAir() {
		return inAir;
	}

	public void setDir(int newdir) {
		dir = newdir;
	}
}
