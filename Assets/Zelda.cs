using UnityEngine;
using System.Collections;

public class Zelda : MonoBehaviour {
	
	private Vector3 trajectory;
	private bool locked = false;
	private bool hasSword = true;
	private int swinging;
	private direction facing;
	public int health = 6;
	public Transform sword;
	public Vector3 trans;
	public static Zelda Z;
	
	private float pixel = 0.0625f;
	// Width of a Zelda pixel (not a screen pixel) in meters
	
	// Use this for initialization
	void Start () {
		Z = this;
		facing = direction.north;
	}
	
	void Update() {
		if (Input.GetKeyDown (KeyCode.X) || Input.GetKeyDown (KeyCode.Period)) {
			if(hasSword) {
				Instantiate(sword, transform.position + trajectory, Quaternion.identity);
				swinging = 0;
			}
		}
	}
	// Update is called once per frame
	void FixedUpdate () {
		if(swinging < Sword.framesToDelay) {
			swinging++;
			return;
		}
		if(locked) return;
		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
			trajectory = Vector3.right;
			transform.Translate (trajectory * pixel);
			facing = direction.west;
		} else if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) {
			trajectory = Vector3.up;
			transform.Translate (trajectory * pixel);
			facing = direction.north;
		} else if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
			trajectory = Vector3.left;
			transform.Translate (trajectory * pixel);
			facing = direction.east;
		} else if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) {
			trajectory = Vector3.down;
			transform.Translate (trajectory * pixel);
			facing = direction.south;
		}
	}
	
	
	// Returns true if overlapping, but not just touching
	bool overlap(Collider other) {
		return (Mathf.Abs (other.gameObject.transform.position.x - transform.position.x) < 1 &&
		        Mathf.Abs (other.gameObject.transform.position.y - transform.position.y) < 1);
	}
	
	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Obstacle" || other.gameObject.tag == "Obstacle") {
			print ("Unlocked");
			locked = false;
		}
	}
	void OnTriggerStay(Collider other) {

		if(other.gameObject.tag == "Wall"){
			print ("Wall");
			transform.Translate (trajectory * -1 * pixel);
		}

		else if (other.gameObject.tag == "Obstacle") {
			locked = true;
			if(overlap (other)) transform.Translate (trajectory * -1 * pixel);
			else locked = false;
			
			
			// If colliding with unit cube
			if (other.gameObject.transform.lossyScale == new Vector3 (1f, 1f, 1f)) {
				if (trajectory == Vector3.left) {
					if (other.gameObject.transform.position.y - transform.position.y > 0.5f) {
						transform.position = new Vector3 (transform.position.x, other.gameObject.transform.position.y - 1f, transform.position.z);
					} else if (other.gameObject.transform.position.y - transform.position.y < -0.5f) {
						transform.position = new Vector3 (transform.position.x, other.gameObject.transform.position.y + 1f, transform.position.z);
					} else {
						print ("Skip");
					}
				} else if (trajectory == Vector3.up) {
					if (other.gameObject.transform.position.x - transform.position.x > 0.5f) {
						transform.position = new Vector3 (other.gameObject.transform.position.x - 1f, transform.position.y, transform.position.z);
					} else if (other.gameObject.transform.position.x - transform.position.x < -0.5f) {
						transform.position = new Vector3 (other.gameObject.transform.position.x + 1f, transform.position.y, transform.position.z);
					} else {
						print ("Skip");
					}
				} else if (trajectory == Vector3.right) {
					if (other.gameObject.transform.position.y - transform.position.y > 0.5f) {
						transform.position = new Vector3 (transform.position.x, other.gameObject.transform.position.y - 1f, transform.position.z);
					} else if (other.gameObject.transform.position.y - transform.position.y < -0.5f) {
						transform.position = new Vector3 (transform.position.x, other.gameObject.transform.position.y + 1f, transform.position.z);
					} else {
						print ("Skip");
					}
				} else if (trajectory == Vector3.down) {
					if (other.gameObject.transform.position.x - transform.position.x > 0.5f) {
						transform.position = new Vector3 (other.gameObject.transform.position.x - 1f, transform.position.y, transform.position.z);
					} else if (other.gameObject.transform.position.x - transform.position.x < -0.5f) {
						transform.position = new Vector3 (other.gameObject.transform.position.x + 1f, transform.position.y, transform.position.z);
					} else {
						print ("Skip");
					}
				}
			}
		}
	}
	
	
	
	
	void OnTriggerEnter(Collider other) {

		if (other.gameObject.tag == "Enemy") {
			transform.Translate (trajectory * -2);
			health--;
			if(health < 1)
				Application.LoadLevel("_Dungeon1_Troy");
		}

		locked = true;
		if (other.gameObject.tag == "Obstacle" && overlap(other)) {
			print("Locked");
		}
		else if(other.gameObject.tag == "Wall"){
			transform.Translate (trajectory * -1 * pixel);
			print ("WALL");
			locked = false;
		}
		else  locked = false;
	}

	public void MoveLink(direction dir){

		Vector3 dest = transform.position;
		switch (dir){
		case direction.north:
			dest.y += trans.y;
			break;
		case direction.south:
			dest.y -= trans.y;
			break;
		case direction.east:
			dest.x += trans.x;
			break;
		case direction.west:
			dest.x -= trans.x;
			break;
		case direction.up:
			dest.z += trans.z;
			break;
		case direction.down:
			dest.z += trans.z;
			break;
		default:
			print ("Broke");
			break;
		}
		transform.position = dest;
	}
}
