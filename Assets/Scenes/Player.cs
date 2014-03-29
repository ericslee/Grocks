﻿using UnityEngine;
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
		// add force on button press
		if (Input.GetKeyDown ("w") && !changedGravity) {
			rigidbody.AddForce(0, 100.0f, 0);
			changedGravity = true;
		}
		if (Input.GetKeyDown ("s") && !changedGravity) {
			rigidbody.AddForce(0, -100.0f, 0);
			changedGravity = true;
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

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.tag == "Ceiling") {
			dir = -1;
			changedGravity = false;
			rigidbody.velocity = Vector3.zero;
		}
		else if (collision.gameObject.tag == "Floor") {
			dir = 1;
			changedGravity = false;
			rigidbody.velocity = Vector3.zero;
		}
	}
}
