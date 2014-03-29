using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Vector3 leftSpeed = new Vector3(0, 0, -15.0f);
	private Vector3 rightSpeed = new Vector3(0, 0, 15.0f);

	private bool inAir;
	private int dir; // -1 is facing upside down (on the ceiling), 1 is facing rightside up (on the floor)
	private bool changedGravity;

	// Use this for initialization
	void Start () {
		inAir = false;
		dir = 1;
		changedGravity = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// prevent any movement except for changing gravity when not in the air
		if (!inAir) {
			rigidbody.velocity = Vector3.zero;
		}

		// add force on button press
		if (Input.GetKeyDown ("w") && !changedGravity) {
			rigidbody.AddForce(0, 500.0f, 0);
			changedGravity = true;
			inAir = true;
		}
		if (Input.GetKeyDown ("s") && !changedGravity) {
			rigidbody.AddForce(0, -500.0f, 0);
			changedGravity = true;
			inAir = true;
		}

		// move left and right
		if (Input.GetKey ("a") && inAir) {
			rigidbody.MovePosition(rigidbody.position + leftSpeed * Time.deltaTime);
		}
		// move left and right
		if (Input.GetKey ("d") && inAir) {
			rigidbody.MovePosition(rigidbody.position + rightSpeed * Time.deltaTime);
		}
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.tag == "Ceiling") {
			dir = -1;
			changedGravity = false;
			inAir = false;
			//rigidbody.velocity = Vector3.zero;
		}
		else if (collision.gameObject.tag == "Floor") {
			dir = 1;
			changedGravity = false;
			inAir = false;
			//rigidbody.velocity = Vector3.zero;
		}
	}
}
