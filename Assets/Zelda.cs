using UnityEngine;
using System.Collections;

public class Zelda : MonoBehaviour {
	
	private bool locked = false;
	private bool hasSword = true;
	public bool bombing = false;
	public bool shooting = false;
	private int swinging;
	private int colliding = 0;
	private direction facing;
	public Transform swordup;
	public Transform obstacle;
	public Transform bomb;
	public Vector3 trans;
	public static Zelda Z;
	public float bounce = 0;
	private int pixelsMoved = 0;
	private float pixel = 0.0625f;
	private RaycastHit hit;
	private int layermask = 1 << 8;
	public bool invincible = false;
	float invincibleTimer = 0;
	// Width of a Zelda pixel (not a screen pixel) in meters
	//for hud
	public static Vector3 trajectory;
	public static int health = 6;
	public static int MAX_HEALTH = 6;
	public static int bombs = 0;
	public static int keys = 0;
	public static bool deity = false;
	public static bool map = false;
	public static bool compass = false;
	public static bool obst = false;
	public Material [] skins = new Material [4];

	void snap() {
		transform.position = new Vector3(Mathf.Round(transform.position.x * 2) / 2, 
		                                 Mathf.Round(transform.position.y * 2) / 2, 
		                                 transform.position.z);
	}

	void Start () {
		Z = this;
		facing = direction.north;
	}
	
	void Update() {
		if (colliding == 0) locked = false;

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
				if(keys == 0)
					keys = 1;
				health = MAX_HEALTH;
			}
		}

		if (Input.GetKeyDown (KeyCode.X) || Input.GetKeyDown (KeyCode.Period)) {
			if(hasSword) {
				GameObject[] swords = GameObject.FindGameObjectsWithTag("Sword");
				if(swords.Length > 0) return;
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
		if (Input.GetKeyDown (KeyCode.C)) {
			if(obst){
				snap ();
				if(facing == direction.north || facing == direction.south)
					Instantiate(obstacle, transform.position + trajectory * 16 * pixel, Quaternion.Euler(0, 0, 180));
				else
					Instantiate(obstacle, transform.position + trajectory * -16 * pixel, Quaternion.Euler(0, 0, 180));

				transform.position = new Vector3(Mathf.Round(transform.position.x * 2) / 2, Mathf.Round(transform.position.y * 2) / 2,  transform.position.z);
			}
		}
	}

	void FixedUpdate () {
		if(bounce > 0) {
			if(locked) {
				bounce = 0;
				return;
			}
			else {
				bounce -= 5f*pixel;
				Vector3 orig = transform.position;
				Vector3 dir = -1*(5f*pixel + 0.5f)*trajectory;
				Debug.DrawRay(orig, dir, Color.red, 5f);
				if(!Physics.Raycast(new Ray(transform.position, -1 * trajectory), out hit, 0.5f + 5f*pixel, layermask)) {
					transform.Translate (trajectory * -5f * pixel);
				}
				else {
					transform.Translate (trajectory * -1 * (hit.distance - 1f));
				}
				return;
			}
		}
		if(swinging < Sword.framesToDelay) {
			swinging++;
			return;
		}

		Vector3 oldTrajectory = trajectory;		

		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
			this.gameObject.renderer.material = skins[3];
			if(invincibleTimer <= 0)
				invincible = deity;

			trajectory = Vector3.right;
			facing = direction.west;

			Vector3 origin = transform.position - (pixel - 0.5f)*trajectory - pixel * Vector3.up;
			Vector3 dir = -1 * trajectory;
			Debug.DrawRay(origin, dir, Color.red);
			if(!Physics.Raycast(new Ray(origin, dir), out hit, 1f, layermask)) {
				transform.Translate (trajectory * 1.4f * pixel);
			}
		} else if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) {
			this.gameObject.renderer.material = skins[0];
			if(invincibleTimer <= 0)
				invincible = deity;

			trajectory = Vector3.up;
			facing = direction.north;

			Vector3 origin = transform.position + (pixel - 0.5f)*trajectory;
			Debug.DrawRay(origin, trajectory, Color.red);
			if(!Physics.Raycast(new Ray(origin, trajectory), out hit, 0.5f, layermask)) {
				transform.Translate (trajectory * 1.4f * pixel);
			}
		} else if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
			this.gameObject.renderer.material = skins[1];
			if(invincibleTimer <= 0)
				invincible = deity;

			trajectory = Vector3.left;
			facing = direction.east;

			Vector3 origin = transform.position - (pixel - 0.5f)*trajectory - pixel * Vector3.up;
			Vector3 dir =  -1 * trajectory;
			Debug.DrawRay(origin, dir, Color.red);
			if(!Physics.Raycast(new Ray(origin, dir), out hit, 1f, layermask)) {
				transform.Translate (trajectory * 1.4f * pixel);
			}
		} else if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) {
			this.gameObject.renderer.material = skins[2];
			if(invincibleTimer <= 0)
				invincible = deity;

			trajectory = Vector3.down;
			facing = direction.south;

			Vector3 origin = transform.position + (pixel - 0.5f)*trajectory;
			Debug.DrawRay(origin, trajectory, Color.red);
			if(!Physics.Raycast(new Ray(origin, trajectory), out hit, 1f, layermask)) {
				transform.Translate (trajectory * 1.4f * pixel);
			}
		}

		if(trajectory != oldTrajectory) {
			snap();
		}
	}
	
	// Returns true if overlapping, but not just touching
	bool overlap(Collider other) {
		return (Mathf.Abs (other.gameObject.transform.position.x - transform.position.x) < 1 &&
		        Mathf.Abs (other.gameObject.transform.position.y - transform.position.y) < 1);
	}
	
	void OnTriggerExit(Collider other) {
		if (other.gameObject.layer == 8) {
			colliding--;
			if (colliding == 0) locked = false;
		}
	}
	void OnTriggerStay(Collider other) {

		if(other.gameObject.tag == "Moveable"){
			other.gameObject.tag = "Obstacle";
			if (trajectory == Vector3.up || trajectory == Vector3.down)
				other.transform.Translate(trajectory*-16*pixel);
			else
				other.transform.Translate(trajectory*16*pixel);
		}
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.layer == 8) {
			locked = true;
			colliding++;
		}
		if (other.gameObject.name == "Locked"){
			if(keys > 0){
				if(other.gameObject != null)
					Destroy(other.gameObject);
				if(!deity) keys--;
				colliding--;
				locked = false;
			}
		}
		if(other.gameObject.tag == "Item")
		{
			switch(other.gameObject.name){
			case "Heart":
				if(health+2 <= MAX_HEALTH)
					health += 2;
				else if (health+1 <= MAX_HEALTH)
					health++;
				break;
			case "Key":
			case "Key(Clone)":
				keys++;
				break;
			case "Bombs":
				bombs += 4;
				break;
			case "Map":
				map = true;
				break;
			case "Compass":
				compass = true;
				break;
			case "Obstacles":
				obst = true;
				break;
			case "Triforce":
				compass = false;
				map = false;
				Application.LoadLevel("_Intro");
				break;
			default:
				break;
			}
			Destroy(other.gameObject);
		}

		if ((other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boomerang") && !invincible) {
			bounce = 2f;
			if(other.gameObject.tag == "Boomerang") {
				health -= 1;
			}
			else {
				health -= other.gameObject.GetComponent<Enemy>().damage;
			}
			invincible = true;
			invincibleTimer = 2f;
			if(health < 1){
				Application.LoadLevel("_Intro");
				health = MAX_HEALTH;
			}
		}
	}

	public void MoveLink(direction dir, float dist){
		Vector3 dest = transform.position;
		invincible = true;

		if(dist != 0){
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
				break;
			}
		}
		transform.position = dest;
	}
}
