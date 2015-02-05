using UnityEngine;
using System.Collections;

public enum EnemyTypes {Bat, Skelleton, Blob, Dragon, Fireball, Goriya};
public class Enemy : MonoBehaviour {
	public EnemyTypes type;
	public int xcoord, ycoord;
	private Vector3 startPos;
	public Transform fireballUp;
	public Transform fireballCenter;
	public Transform fireballDown;
	public int health = 1;
	public int damage = 1; // Amount of damage to deal
	public Vector3 trajectory = Vector3.zero;
	private int frames = 51; 
	private int dragonFrames = 0;
	private int dragonShots = 0;
	public float pixel = .0625f;
	public direction facing;
	private RaycastHit hit;
	private int layermask = 1 << 8;
	public Material [] skins = new Material [4];
	public bool haskey;
	public GameObject [] items = new GameObject [3];

	// Use this for initialization
	void Start () {
		Vector3 scale = new Vector3();
		scale.z = 1f;
		scale.x = 1f;
		scale.y = 1f;

		if(type == EnemyTypes.Skelleton) {
			frames = 75;
			trajectory = Vector3.left;
			health = 2;
			scale.x = .99f;
			scale.y = .99f;
			this.gameObject.renderer.material = skins[0];
		}
		else if(type == EnemyTypes.Bat){
			scale.y = .5f;
			this.gameObject.renderer.material = skins[1];
		}
		else if (type == EnemyTypes.Dragon) {
			health = 12;
			scale.x = 1.5f;
			scale.y = 2f;
			this.gameObject.renderer.material = skins[2];
			BoxCollider box = this.gameObject.GetComponent<BoxCollider>();
			Vector3 center, size;
			center.x = 0.1667f;
			center.y = -0.31f;
			center.z = 0f;
			size.x = 0.667f;
			size.y = 0.38f;
			size.z = 0f;
			box.center = center;
			box.size = size;
			trajectory = new Vector3(-1, 0, 0);
		}
		else if(type == EnemyTypes.Fireball){
			scale.x = .5f;
			scale.y = .5f;
		}
		else if(type == EnemyTypes.Blob){
			scale.x = .5f;
			scale.y = .5f;
			this.gameObject.renderer.material = skins[3];
		}
		this.transform.localScale = scale;
		startPos = transform.position;
	}

	void Update(){
		if(MoveCamera.xcoord != xcoord || MoveCamera.ycoord != ycoord)
			transform.position = startPos;
	}
	
	void FixedUpdate () {
		if(type == EnemyTypes.Fireball) return;
		frames++;
		if(type == EnemyTypes.Dragon) {
			dragonFrames++;
			transform.Translate(trajectory * 0.5f * pixel);
			if(dragonFrames == 32) {
				dragonFrames = 0;
				dragonShots++;
				if(dragonShots == 6) {
					dragonShots = 0;
					Instantiate(fireballUp, transform.position, Quaternion.identity);
					Instantiate(fireballCenter, transform.position, Quaternion.identity);
					Instantiate(fireballDown, transform.position, Quaternion.identity);
				}
				//70.75	74.75
				if(transform.position.x <= 71) {
					trajectory.x *= -1;
				}
				else if(transform.position.x >= 74.5) {
					trajectory.x *= -1;
				}
				else {
					int coin = Random.Range(0, 2); // 0 or 1
					if(coin == 0){
						trajectory = new Vector3(-1, 0, 0);
					}
					else {
						trajectory = new Vector3(1, 0, 0);
					}
				}
			}
			return;
		}

		if(frames > 20) {
			// Pick a random direction
			if(type == EnemyTypes.Bat) {
				trajectory =  new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
				trajectory.Normalize ();
			}
			else if(type == EnemyTypes.Skelleton) {
				return;
				/*if(trajectory == Vector3.left || trajectory == Vector3.right) {
					trajectory = Vector3.up;
				}
				else if(trajectory == Vector3.up || trajectory == Vector3.down || trajectory == Vector3.forward || trajectory == Vector3.back) {
					trajectory = Vector3.right;
				}
				
				if(Random.Range(-1f, 1f) < 0f) {
					trajectory = -1f * trajectory;
				}*/
			}
			
			frames = 0;
		}
		else {
			if(type == EnemyTypes.Bat) {
				transform.Translate (trajectory*pixel);
				return;
			}
		}

		Vector3 origin = transform.position;
		Vector3 dir = trajectory;
		if(trajectory == Vector3.left || trajectory == Vector3.right) {
			//dir = -1 * trajectory;
		}
		Debug.DrawRay(origin, dir, Color.cyan);
		if(!Physics.Raycast(new Ray(origin, dir), out hit, 0.5f + pixel, layermask)) {
			transform.Translate (trajectory * pixel);
			////print ("Hit nothing");
		}
		else {
			////print ("Hit a thing");
		}



	}

	void OnTriggerStay(Collider other) {

	}

	void OnTriggerEnter(Collider other) {

		/*if(other.gameObject.tag == "Link"){
			Zelda.health -= damage;
			//print (Zelda.health);
			//print ("HIT");
		}*/
		if(other.gameObject.name == "door") {
			if(type == EnemyTypes.Skelleton) return;
			trajectory = -1 * trajectory;
			transform.Translate (pixel * trajectory);
		}
		if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Obstacle") {
			if(type == EnemyTypes.Dragon || type == EnemyTypes.Skelleton) return;
			//if(type == EnemyTypes.Bat) {
				trajectory = -1 * trajectory;
				transform.Translate (trajectory * 3 * pixel);
			//}
		}
		
		if (other.gameObject.tag == "Sword") {
			health -= 1;
		}
		else if (other.gameObject.tag == "Boomerang") {
			health -= 1;
		}
		else if (other.gameObject.tag == "Bomb") {
			health -= 4;
		}
		if(health <= 0) {
			Destroy(gameObject);
			var number = Random.Range(0,12);
			if(number < 2)
				Instantiate(items[number], transform.position, Quaternion.Euler(0, 0, 180));
			if(haskey)
				Instantiate(items[2], transform.position, Quaternion.Euler(0, 0, 180));
			////print ("Enemy destroyed");
		}
	}
}