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

	private string[] opponents;

	// Face materials
	public Material EricHappy;
	public Material EricAngry;
	public Material EricStruggle;
	public Material EricStunned;

	public Material GaryHappy;
	public Material GaryAngry;
	public Material GaryStruggle;
	public Material GaryStunned;

	GameManager gameManager;
	public GameObject sittingOn;

	// Use this for initialization
	void Start () {
		if (tag == "Player1") dir = -1;
		if (tag == "Player2") dir = 1;
		inAir = false;
		bounceTimer = 0;

		// Assign player keys, set opponents
		if(tag == "Player1") {
			keyUp = KeyCode.W;
			keyDown = KeyCode.S;
			keyRight = KeyCode.D;
			keyLeft = KeyCode.A;
			opponents = new string[1];
			opponents[0] = "Player2";
		}
		else if(tag == "Player2") {
			keyUp = KeyCode.UpArrow;
			keyDown = KeyCode.DownArrow;
			keyRight = KeyCode.RightArrow;
			keyLeft = KeyCode.LeftArrow;
			opponents = new string[1];
			opponents[0] = "Player1";
		}

		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
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

		if (!gameManager.controlsFrozen) {
			if (sittingOn == null) {
				Debug.Log("calling from " + tag);
				inAir = true;
			}
			
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

		// change face material based on situation
		if(inAir) {
			if(tag == "Player1") renderer.material = EricAngry;
			if(tag == "Player2") renderer.material = GaryAngry;
		}
		else {
			if(tag == "Player1") renderer.material = EricHappy;
			if(tag == "Player2") renderer.material = GaryHappy;
		}
	}

	bool isOpponent(string tag) {
		for (int i = 0; i < opponents.Length; i++) {
			if (tag == opponents[i]) return true;
		}
		return false;
	}

	void OnCollisionEnter (Collision hit) {

		if (sittingOn == null) {
			Debug.Log("calling from " + tag);
			inAir = true;
		}
		
		
		if (hit.collider.tag == "Floor" || hit.collider.tag == "Ceiling") {
			Debug.Log("setting sittingOn!");
			sittingOn = hit.gameObject;
		}

		float otherY = hit.transform.position.y;
		float thisY = this.gameObject.transform.position.y;
		// get heights of objects
		float otherH = hit.transform.localScale.y / 2;
		float thisH = this.transform.localScale.y / 2;
		
		bool collidingOnSide = false;
		
		if (Mathf.Abs(thisY - otherY) + 0.1 < thisH + otherH) {
			//Debug.Log(thisH + otherH + " height difference"); // TODO
			//Debug.Log(Mathf.Abs(thisY - otherY) + 0.1 + " centerpoint difference"); // TODO
			collidingOnSide = true;
		}

		// handle player/player collisions
		if (isOpponent(hit.collider.tag) && inAir) {
			if (hit.collider.GetComponent<player_experiment>().inAir == false){
				inAir = false;
				Debug.Log("passing in " + hit.gameObject.tag);
				playersStacked(hit.gameObject);
				if (hit.collider.GetComponent<player_experiment>().sittingOn == null) {
					hit.collider.GetComponent<player_experiment>().inAir = true;
				}
				//inAir = false;

			}
		}
	}

	void playersStacked(GameObject opponent) {
		if (opponent.GetComponent<player_experiment>().sittingOn == null) {
			Debug.Log ("enemy isn't sitting on anything anymore...");
			opponent.GetComponent<player_experiment>().inAir = true;
			Debug.Log ("enemy should now be falling...");
			return;
		}
		opponent.GetComponent<player_experiment>().sittingOn.GetComponent<box_collision_experiment>().manualDamage();
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
		if (!isOpponent(collision.collider.tag))inAir = true;
	}

	public void Reset() {
		if (tag == "Player1") dir = -1;
		if (tag == "Player2") dir = 1;
		inAir = false;
		bounceTimer = 0;
		rigidbody.velocity = Vector3.zero;
	}
}
