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

	// Struggle meshes
	public Mesh defaultCube;
	public Mesh StruggleFloorLeft;
	public Mesh StruggleFloorRight;
	public Mesh StruggleCeilingLeft;
	public Mesh StruggleCeilingRight;

	private bool isStrugglin;
	private float strugglinTimer;
	public GameObject IMSWEATIN;
	public GameObject theSweat;

	GameManager gameManager;
	
	// DEATH CRIES
	public AudioClip deathHi;
	public AudioClip deathLow;
	public AudioClip struggleHi;
	public AudioClip struggleLow;

	public bool won;
	
	// Use this for initialization
	void Start () {
		if (tag == "Player1") dir = -1;
		if (tag == "Player2") dir = 1;
		inAir = false;
		bounceTimer = 0;
		won = false;

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
		if (won) {
			hop();
			return;
		}

		isStrugglin = false;

		strugglinTimer--;
		if(strugglinTimer < 0)
			strugglinTimer = 0.0f;

		bounceTimer--;
		if (bounceTimer < 0)
			bounceTimer = 0.0f;

		if (!inAir) {
			rigidbody.velocity = Vector3.zero;
		}
		if (inAir) rigidbody.AddForce(0, dir * 300.0f, 0);

		// if not struggling, use the default cube mesh
		if(!isStrugglin) {
			GetComponent<MeshFilter>().mesh = defaultCube;
		}

		if (!gameManager.controlsFrozen) {
			
			// move left and right
			if (Input.GetKey (keyLeft)) {
				if(inAir && bounceTimer < 1) {
					rigidbody.MovePosition(rigidbody.position + leftSpeed * Time.deltaTime);
					
					// remove horizontal velocity if player moves in air after collision
					rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
				}
				
				// struggle otherwise
				if(!inAir && strugglinTimer < 1.0f) {
					if(dir == -1) {
						GetComponent<MeshFilter>().mesh = StruggleFloorLeft;
					}
					else {
						GetComponent<MeshFilter>().mesh = StruggleCeilingLeft;
					}
					isStrugglin = true;
				}
			}
			
			if (Input.GetKey (keyRight)) {
				if(inAir && bounceTimer < 1) {
					rigidbody.MovePosition(rigidbody.position + rightSpeed * Time.deltaTime);
					
					// remove horizontal velocity if player moves in air after collision
					rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
				}
				
				// struggle otherwise
				if(!inAir && strugglinTimer < 1.0f) {
					if(dir == -1) {
						GetComponent<MeshFilter>().mesh = StruggleFloorRight;
					}
					else {
						GetComponent<MeshFilter>().mesh = StruggleCeilingRight;
					}
					isStrugglin = true;
				}
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
		if(isStrugglin) {
			if(tag == "Player1") {
				renderer.material = EricStruggle;
				if (!GameObject.Find("One shot audio")){ 
					AudioSource.PlayClipAtPoint (struggleLow, transform.position);
				}
			}
			if(tag == "Player2") {
				renderer.material = GaryStruggle;
				if (!GameObject.Find("One shot audio")){ 
					AudioSource.PlayClipAtPoint(struggleHi,transform.position);
				}
			}

			// TIME TO SWEAT
			if(IMSWEATIN && !theSweat) {
				theSweat = (GameObject)Instantiate(IMSWEATIN, transform.position,
				                                  Quaternion.identity);
				if(theSweat) {
					Destroy(theSweat, 0.25f);
				}
			}
		}
		else {
			if(inAir) {
				if(tag == "Player1") renderer.material = EricAngry;
				if(tag == "Player2") renderer.material = GaryAngry;
			}
			else {
				if(tag == "Player1") renderer.material = EricHappy;
				if(tag == "Player2") renderer.material = GaryHappy;
			}
		}
	}

	bool isOpponent(string tag) {
		for (int i = 0; i < opponents.Length; i++) {
			if (tag == opponents[i]) return true;
		}
		return false;
	}

	void OnCollisionEnter (Collision hit) {
		
		if (hit.collider.tag == "Floor" || hit.collider.tag == "Ceiling") {

			float otherY = hit.transform.position.y;
			float thisY = this.gameObject.transform.position.y;
			// get heights of objects
			float otherH = hit.transform.localScale.y / 2;
			float thisH = this.transform.localScale.y / 2;
			
			bool collidingOnSide = false;
			
			if (Mathf.Abs(thisY - otherY) + 0.1 < thisH + otherH) {
				collidingOnSide = true;
				inAir = false;
				hit.gameObject.GetComponent<box_collision_experiment>().sittingOnMe = gameObject;
			}
		}

		// handle player/player collisions
		if (isOpponent(hit.collider.tag) && inAir) {
			inAir = false;

			float otherY = hit.transform.position.y;
			float thisY = this.gameObject.transform.position.y;
			// get heights of objects
			float otherH = hit.transform.localScale.y / 2;
			float thisH = this.transform.localScale.y / 2;
			
			bool collidingOnSide = false;

			if (Mathf.Abs(thisY - otherY) + 0.1 < thisH + otherH) {
				collidingOnSide = true;
				inAir = true;
				return;
			}
			
			if (hit.gameObject.GetComponent<player_experiment>().inAir == false){
				Debug.Log("breaking on " + hit.collider.tag);
				gameManager.damagePropagate();
				//hit.gameObject.GetComponent<player_experiment>().inAir = true;
			}
		}

		strugglinTimer = 25.0f;
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

	public void playDeathSound() {
		if (tag == "Player1")
			AudioSource.PlayClipAtPoint (deathLow, transform.position);
		else if (tag == "Player2")
			AudioSource.PlayClipAtPoint(deathHi,transform.position);
	}

	void hop() {
		if (!won) return;
		if (bounceTimer < 0) bounceTimer = 20;
		if(tag == "Player1") renderer.material = EricHappy;
		if(tag == "Player2") renderer.material = GaryHappy;
		inAir = false;
		Vector3 shift = new Vector3(0.0f, 5.0f, 0.0f);
		if (bounceTimer < 10) rigidbody.MovePosition(rigidbody.position + shift);
		else rigidbody.MovePosition(rigidbody.position - shift);
		bounceTimer--;
	}
}
