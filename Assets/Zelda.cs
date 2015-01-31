using UnityEngine;
using System.Collections;

public class Zelda : MonoBehaviour {
	
	private Vector3 trajectory;
	private bool locked = false;
	private bool hasSword = true;
	public bool bombing = false;
	private int swinging;
	private int colliding = 0;
	private direction facing;
	public Transform swordup;
	public Transform swordright;
	public Transform bomb;
	public Vector3 trans;
	public static Zelda Z;
	private float bounce = 0;
	private int pixelsMoved = 0;
	private float pixel = 0.0625f;
	public bool invincible = false;
	float invincibleTimer = 0;
	// Width of a Zelda pixel (not a screen pixel) in meters
	//for hud
	public static int health = 2;
	public static int MAX_HEALTH = 6;
	public static int bombs = 100;
	public static int keys = 10;
	public static bool deity = false;
	

	void snap() {
		Vector3 target = new Vector3(Mathf.Round(transform.position.x * 2) / 2, Mathf.Round(transform.position.y * 2) / 2,  transform.position.z);
		RaycastHit hit;
		if(Physics.Raycast(target - 0.5f*Vector3.up - 0.5f*Vector3.left, Mathf.Sqrt(2)/2f * (Vector3.up + Vector3.left), out hit)) {
			Debug.DrawLine(target - 0.5f*Vector3.up - 0.5f*Vector3.left, hit.point);
			print ("Ray 1 hit");
		}
		/*if(Physics.Raycast(target - 0.5f*Vector3.up - 0.5f*Vector3.right, Vector3.up + Vector3.right, Mathf.Sqrt(2))) {
			print ("Ray 2 hit");
		}*/
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
		if(invincible && invincibleTimer > 0){
			invincibleTimer -= Time.deltaTime;
		}
		else if(invincible && invincibleTimer <= 0){
			print ("DONE");
			invincible = deity;
		}
		if(Input.GetKeyDown(KeyCode.G)){
			deity = !deity;
			invincible = deity;
			if(deity){
				bombs = 1;
				health = MAX_HEALTH;
			}
		}
		if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W)){
			facing = direction.north;
			if(!deity && invincibleTimer <= 0)
				invincible = false;
		}else if(Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D)){
			facing = direction.east;
			if(!deity && invincibleTimer <= 0)
				invincible = false;
		}else if(Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.S)){
			facing = direction.south;
			if(!deity && invincibleTimer <= 0)
				invincible = false;
		}else if(Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A)){
			facing = direction.west;
			if(!deity && invincibleTimer <= 0)
				invincible = false;
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
		if (Input.GetKeyDown (KeyCode.Z) || Input.GetKeyDown (KeyCode.Comma)) {
<<<<<<< HEAD
			if(!bombing) {
				Instantiate(bomb, transform.position + trajectory * 16 * pixel, Quaternion.identity);
=======
			if(!bombing && bombs > 0){
				if(facing == direction.north || facing == direction.south)
					Instantiate(bomb, transform.position + trajectory * 16 * pixel, Quaternion.Euler(0, 0, 180));
				else
					Instantiate(bomb, transform.position + trajectory * -16 * pixel, Quaternion.Euler(0, 0, 180));
				if(!deity)
					bombs--;
>>>>>>> 9550c422ab1cf9ee608d84912a1121d83d43c4ed
				bombing = true;
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
			snap();
			/*while(pixelsMoved != 0) {
				pixelsMoved = (pixelsMoved + 1) % 8;
				transform.Translate (oldTrajectory * pixel);
			}*/
		}
		else {
			transform.Translate (trajectory * pixel);
		}
		//pixelsMoved = (pixelsMoved + 1) % 8;
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
		else if(other.gameObject.tag == "Moveable"){
			print ("MOVEABLE");
			transform.Translate (trajectory *-1* pixel);
			other.gameObject.tag = "Obstacle";
			if (trajectory == Vector3.up || trajectory == Vector3.down)
				other.transform.Translate(trajectory*-16*pixel);
			else
				other.transform.Translate(trajectory*16*pixel);
		}
	}

	void OnTriggerEnter(Collider other) {
		//print(colliding);
		if (other.gameObject.name == "Locked"){
			if(keys > 0)
				keys--;
		}

		if(other.gameObject.tag == "Item")
		{
			print ("ITEM");
			switch(other.gameObject.name){
			case "Heart":
				if(health < MAX_HEALTH)
					health += 2;
				break;
			case "Key":
				keys++;
				break;
			case "Bombs":
				bombs += 4;
				break;
			default:
				print ("Unknown Item");
				break;
			}
			Destroy(other.gameObject);
		}

		if (other.gameObject.tag == "Enemy" && !invincible) {
			//transform.Translate (trajectory * -2);
			bounce = 2f;
<<<<<<< HEAD
			health--;
			if(health < 1)
				Application.LoadLevel("_Dungeon1_Clara2");
=======
			health --;
			invincible = true;
			invincibleTimer = 2f;
			print("SUPER");
			if(health < 1){
				Application.LoadLevel("_Dungeon1_Troy2");
				health = MAX_HEALTH;
			}
>>>>>>> 9550c422ab1cf9ee608d84912a1121d83d43c4ed
		}

		//locked = true;
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
<<<<<<< HEAD
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
=======
		invincible = true;

		if(dist != 0){
			print ("Move by " + dist);
			switch (dir){
			case direction.north:
				dest.y += dist;
				break;
			case direction.south:
				dest.y -= dist;
				break;
			case direction.east:
				dest.x += dist;
				break;
			case direction.west:
				dest.x -= dist;
				break;
			case direction.up:
				dest.z += dist;
				break;
			case direction.down:
				dest.z += dist;
				break;
			default:
				print ("Broke");
				break;
			}
		}
		else{
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
				dest.x += 3;
				dest.y += 7;
				break;
			case direction.down:
				dest.x -= 4;
				dest.y -= 8;
				break;
			default:
				print ("Broke");
				break;
			}
>>>>>>> 9550c422ab1cf9ee608d84912a1121d83d43c4ed
		}
		transform.position = dest;
	}
}
