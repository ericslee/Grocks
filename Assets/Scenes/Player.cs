using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Vector3 leftSpeed = new Vector3(0, 0, -15.0f);
	private Vector3 rightSpeed = new Vector3(0, 0, 15.0f);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// add force on button press
		if (Input.GetKeyDown ("w")) {
			rigidbody.AddForce(0, 100.0f, 0);
		}
		if (Input.GetKeyDown ("s")) {
			rigidbody.AddForce(0, -100.0f, 0);
		}

		// move left and right
		if (Input.GetKey ("a")) {
			rigidbody.MovePosition(rigidbody.position + leftSpeed * Time.deltaTime);
		}
		// move left and right
		if (Input.GetKey ("d")) {
			rigidbody.MovePosition(rigidbody.position + rightSpeed * Time.deltaTime);
		}
	}
}
