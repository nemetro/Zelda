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
	private RaycastHit hit;
	private int layermask = 1 << 8;
	public bool invincible = false;
	float invincibleTimer = 0;
	// Width of a Zelda pixel (not a screen pixel) in meters
	//for hud
	public static int health = 6;
	public static int MAX_HEALTH = 8;
	public static int bombs = 2;
	public static int keys = 0;
	public static bool deity = false;
	

	void snap() {
		//Vector3 target = new Vector3(Mathf.Round(transform.position.x * 2) / 2, Mathf.Round(transform.position.y * 2) / 2, 0.5f);
		//RaycastHit hit;
		//int layermask = 1 << 8;
		/*if(Physics.Raycast(target - 0.5f*Vector3.up - 0.5f*Vector3.left, (Vector3.up + Vector3.left), out hit, Mathf.Sqrt (2f), layermask)) {
			Debug.DrawRay(target - 0.5f*Vector3.up - 0.5f*Vector3.left, Mathf.Sqrt(2)/2f * (Vector3.up + Vector3.left), Color.red, 1f);
			print ("Ray hit " + hit.collider.tag + ", " + hit.collider.name + ", " + hit.collider.gameObject.layer);
			//print ("Ray 1 hit");
		}*/
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
			invincible = deity;
		}
		if(Input.GetKeyDown(KeyCode.G)){
			deity = !deity;
			invincible = deity;
			if(deity){
				if(bombs == 0)
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
				switch(facing){
				case direction.north:
					Instantiate(swordup, transform.position + trajectory * 12 * pixel, Quaternion.Euler(0, 0, 180));
					break;
				case direction.south:
					Instantiate(swordup, transform.position + trajectory * 12 * pixel, Quaternion.Euler(0, 0, 0));
					break;
				case direction.east:
					Instantiate(swordup, transform.position + trajectory * -12 * pixel, Quaternion.Euler(0, 0, 90));
					break;
				case direction.west:
					Instantiate(swordup, transform.position + trajectory * -12 * pixel, Quaternion.Euler(0, 0, 270));
					break;
				default:
					break;
				}
				swinging = 0;
			}
		}
		if (Input.GetKeyDown (KeyCode.Z) || Input.GetKeyDown (KeyCode.Comma)) {
			if(!bombing && bombs > 0){
				if(facing == direction.north || facing == direction.south)
					Instantiate(bomb, transform.position + trajectory * 16 * pixel, Quaternion.Euler(0, 0, 180));
				else
					Instantiate(bomb, transform.position + trajectory * -16 * pixel, Quaternion.Euler(0, 0, 180));
				if(!deity)
					bombs--;
				bombing = true;
			}
		}
	}
	// Update is called once per frame
	void FixedUpdate () {
		if(bounce > 0) {
			if(locked) {
				print ("Locked");
				bounce = 0;
				return;
			}
			else {
				bounce -= 5f*pixel;
				Debug.DrawRay(transform.position, -1*(5f*pixel + 0.5f)*trajectory, Color.red, 1f);
				if(!Physics.Raycast(new Ray(transform.position, -1*trajectory), out hit, 0.5f + 5f*pixel, layermask)) {
					transform.Translate (trajectory * -5f * pixel);
					print ("Bounced");
				}
				else {
					print ("Bounce failed");
					transform.Translate (trajectory * -1 * (hit.distance - 1f));
				}
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
			Vector3 origin = transform.position - (pixel - 0.5f)*trajectory - pixel * Vector3.up;
			Vector3 dir = -1 * trajectory;
			Debug.DrawRay(origin, dir, Color.red);
			if(!Physics.Raycast(new Ray(origin, dir), out hit, 1f, layermask)) {
				transform.Translate (trajectory * pixel);
			}
		} else if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) {
			trajectory = Vector3.up;
			facing = direction.north;
			Vector3 origin = transform.position + (pixel - 0.5f)*trajectory;
			Debug.DrawRay(origin, trajectory, Color.red);
			if(!Physics.Raycast(new Ray(origin, trajectory), out hit, 0.5f, layermask)) {
				transform.Translate (trajectory * pixel);
			}
		} else if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
			trajectory = Vector3.left;
			facing = direction.east;
			Vector3 origin = transform.position - (pixel - 0.5f)*trajectory - pixel * Vector3.up;
			Vector3 dir =  -1 * trajectory;
			Debug.DrawRay(origin, dir, Color.red);
			if(!Physics.Raycast(new Ray(origin, dir), out hit, 1f, layermask)) {
				transform.Translate (trajectory * pixel);
			}
		} else if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) {
			trajectory = Vector3.down;
			facing = direction.south;
			Vector3 origin = transform.position + (pixel - 0.5f)*trajectory;
			Debug.DrawRay(origin, trajectory, Color.red);
			if(!Physics.Raycast(new Ray(origin, trajectory), out hit, 1f, layermask)) {
				transform.Translate (trajectory * pixel);
			}
		}

		if(trajectory != oldTrajectory) {
			snap();
			/*while(pixelsMoved != 0) {
				pixelsMoved = (pixelsMoved + 1) % 8;
				transform.Translate (oldTrajectory * pixel);
			}*/
		}
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
		if(other.gameObject.tag == "Moveable"){
			print ("MOVEABLE");
			//transform.Translate (trajectory *-1* pixel);
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
			health --;
			invincible = true;
			invincibleTimer = 2f;
			if(health < 1){
				Application.LoadLevel("_Dungeon1_Clara3");
				health = MAX_HEALTH;
			}
		}
	}

	public void MoveLink(direction dir, float dist){
		print (dir);
		Vector3 dest = transform.position;
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
		}
		transform.position = dest;
	}
}
