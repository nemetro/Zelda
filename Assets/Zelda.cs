using UnityEngine;
using System.Collections;

public class Zelda : MonoBehaviour {
	
	private Vector3 trajectory;
	private bool locked = false;
	private bool hasSword = true;
	private int swinging;
	private int colliding = 0;
	private direction facing;
	public int health = 6;
	public Transform swordup;
	public Transform swordright;
	public Vector3 trans;
	public static Zelda Z;
	private float bounce = 0;
	private int pixelsMoved = 0;
	private float pixel = 0.0625f;
	// Width of a Zelda pixel (not a screen pixel) in meters
	

	void snap() {
		transform.position = new Vector3(Mathf.Round(transform.position.x * 2) / 2, Mathf.Round(transform.position.y * 2) / 2,  transform.position.z);
		/*if(trajectory == Vector3.right || trajectory == Vector3.left) {
			float pixel = 16f * transform.position.y - 24f; // Pixels from topmost pixel of topmost floor tile
			while(pixel >= 175f) pixel -= 175f;
			float offset = pixel / 16f; // meters from topmost pixel of topmost floor tile of room
			float move = Mathf.Round(offset * 2f) / 2f - offset; // number of meters to move to snap vertically
			print (pixel + ", " + offset + ", " + move);
			transform.position = new Vector3(transform.position.x, transform.position.y + move, transform.position.z);
		}
		else {
			float pixel = 16f * transform.position.x + 430f; // Pixels from leftmost pixel of leftmost floor tile
			while(pixel < 248f) pixel += 248f;
			float offset = pixel / 16f; // meters from leftmost pixel of leftmost floor tile of room
			float move = Mathf.Round(offset * 2f) / 2f - offset; // number of meters to move to snap horizontally
			print (pixel + ", " + offset + ", " + move);
			transform.position = new Vector3(transform.position.x + move, transform.position.y, transform.position.z);
		}*/
	}

	// Use this for initialization
	void Start () {
		Z = this;
		facing = direction.north;
	}
	
	void Update() {

		if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W)){
			facing = direction.north;
		}else if(Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D)){
			facing = direction.east;
		}else if(Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.S)){
			facing = direction.south;
		}else if(Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A)){
			facing = direction.west;
		}

		if (Input.GetKeyDown (KeyCode.X) || Input.GetKeyDown (KeyCode.Period)) {
			if(hasSword) {
				if(facing == direction.north || facing == direction.south)
					Instantiate(swordup, transform.position + trajectory * 12 * pixel, Quaternion.identity);
				else
					Instantiate(swordright, transform.position + trajectory * 12 * pixel, Quaternion.identity);
				swinging = 0;
			}
		}
	}
	// Update is called once per frame
	void FixedUpdate () {
		if(bounce > 0) {
			if(locked) {
				bounce = 0;
				return;
			}
			else {
				bounce -= 5*pixel;
				transform.Translate (trajectory * -5f * pixel);
				return;
			}
		}
		if(swinging < Sword.framesToDelay) {
			swinging++;
			return;
		}
		if(locked) return;
		Vector3 oldTrajectory = trajectory;
		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
			trajectory = Vector3.right;
			facing = direction.west;
		} else if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) {
			trajectory = Vector3.up;
			facing = direction.north;
		} else if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
			trajectory = Vector3.left;
			facing = direction.east;
		} else if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) {
			trajectory = Vector3.down;
			facing = direction.south;
		}
		else return;
		if(trajectory != oldTrajectory) {
			while(pixelsMoved != 0) {
				pixelsMoved = (pixelsMoved + 1) % 8;
				transform.Translate (oldTrajectory * pixel);
			}
		}
		transform.Translate (trajectory * pixel);
		pixelsMoved = (pixelsMoved + 1) % 8;
	}
	
	
	// Returns true if overlapping, but not just touching
	bool overlap(Collider other) {
		return (Mathf.Abs (other.gameObject.transform.position.x - transform.position.x) < 1 &&
		        Mathf.Abs (other.gameObject.transform.position.y - transform.position.y) < 1);
	}
	
	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Obstacle" || other.gameObject.tag == "Wall") {
			colliding--;
			//print ("Unlocked");
			locked = false;
		}
	}
	void OnTriggerStay(Collider other) {
		if(other.gameObject.tag == "Wall" || other.gameObject.tag == "Obstacle"){
			//transform.Translate (trajectory * -1 * pixel);
		}
	}

	void OnTriggerEnter(Collider other) {
		//print(colliding);
		if (other.gameObject.tag == "Enemy") {
			//transform.Translate (trajectory * -2);
			bounce = 2f;
			health--;
			if(health < 1)
				Application.LoadLevel("_Dungeon1_Clara2");
		}

		locked = true;
		if(other.gameObject.tag == "Wall" || other.gameObject.tag == "Obstacle"){
			if(bounce > 0) {
				bounce = 0;
				transform.Translate (trajectory * 3 * pixel);
			}
			snap ();
			pixelsMoved = 0;
		}
		else  locked = false;
	}

	public void MoveLink(direction dir){
		print (dir);
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
