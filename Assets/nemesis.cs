using UnityEngine;
using System.Collections;

public class nemesis : MonoBehaviour {

	public Vector3 trajectory;
	public bool haskey;
	private float frames = 0;
	private float pixelsPerFrame = 1f;
	private float squaresPerDirection = 4f;
	private float pixelsPerSquare = 16f;
	private float framesPerDirection = 0f;
	private int movesBetweenPauses = 0; // Zero for no pauses
	public int health = 2;
	private bool pause;
	private int layermask = 1 << 8;
	private RaycastHit hit;
	public GameObject [] items = new GameObject [3];

	void snap() {
		transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y),  transform.position.z);
	}

	void setTrajectory() {
		int die = Random.Range (0, 4);
		if(die == 0) trajectory = Vector3.up;
		else if(die == 1) trajectory = Vector3.right;
		else if(die == 2) trajectory = Vector3.down;
		else trajectory = Vector3.left;
	}

	void Start () {
		framesPerDirection = pixelsPerSquare * squaresPerDirection / pixelsPerFrame;
		snap();
		setTrajectory();
	}

	void FixedUpdate() {
		if(frames == framesPerDirection) {
			frames = 0;
			if(pause) {
				pause = false;
			}
			int pauseDie = Random.Range (0, movesBetweenPauses);
			if(pauseDie == 1) {
				pause = true;
			}
			else {
				setTrajectory ();
			}
		}
		if(!pause) {
			Vector3 origin = transform.position;
			Vector3 dir = trajectory;
			float magnitude = 0.5f + pixelsPerFrame / pixelsPerSquare;
			// Check each spot for obstacles before moving there.
			Debug.DrawRay(origin, magnitude * dir, Color.red);
			bool proceed = true;
			if(Physics.Raycast(new Ray(origin, dir), out hit, magnitude, layermask)) {
				proceed = false;
			}
			if(Physics.Raycast(new Ray(origin, dir), out hit, magnitude)) {
				if(hit.collider.gameObject.name == "door") {
					proceed = false;
				}
			}
			if(proceed) {
				transform.Translate(trajectory * pixelsPerFrame / pixelsPerSquare);
			}
			else {
				frames = framesPerDirection - 1;
				setTrajectory();
			}
		}
		frames++;
	}

	void OnTriggerStay(Collider other) {
	}
	
	void OnTriggerEnter(Collider other) {

		if (other.gameObject.tag == "Sword") {
			trajectory = other.GetComponent<Sword>().trajectory;
			frames = 0;
			health -= 1;
		}
		else if (other.gameObject.tag == "Boomerang") {
			//health -= 1;
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