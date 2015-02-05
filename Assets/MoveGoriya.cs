using UnityEngine;
using System.Collections;

public class MoveGoriya : MonoBehaviour {
	public GameObject boomerang;
	public GameObject newBoomerang;


	private int frames = 0;
	private int squaresToMove = 2;
	private Vector3 trajectory = Vector3.left;
	private float bounce = 0;
	private RaycastHit hit;
	private bool waiting = false;
	void snap() {
		transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y),  transform.position.z);
	}	
	
	// Use this for initialization
	void Start () {
		snap();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(MoveCamera.xcoord != GetComponent<Enemy>().xcoord || MoveCamera.ycoord != GetComponent<Enemy>().ycoord) {
			return;
		}
		if(bounce > 0) {
			bounce -= 5f/16f;
			Vector3 orig = transform.position;
			Vector3 dir = -1*(5f/16f + 0.5f)*trajectory;
			Debug.DrawRay(orig, dir, Color.red, 5f);
			if(!Physics.Raycast(new Ray(transform.position, -1 * trajectory), out hit, 0.5f + 5f/16f, 1 << 8)) {
				transform.Translate (trajectory * -5f/16f);
				//print ("Bounced");
			}
			else {
				bounce = 0;
				//print ("Bounce failed");
			}
			return;
		}
		if(frames == 15) {
			//print ("Fifteen");
			int die = Random.Range(0, 7);
			if(die == 0) {
				//print ("die");
				waiting = true;
				newBoomerang = Instantiate(boomerang, transform.position, Quaternion.identity) as GameObject;
				newBoomerang.GetComponent<moveBoomerang>().trajectory = trajectory;
				trajectory = Vector3.zero;
			}
		}
		if(frames >= 32 * squaresToMove) {
			frames = 0;
			snap ();
			squaresToMove = Random.Range (1, 7);
			if(waiting) trajectory = Vector3.zero;
			else {
				int die = Random.Range(0, 7);
				if(die < 2) {
					trajectory = Vector3.up;
				}
				else if(die < 4) {
					trajectory = Vector3.down;
				}
				else if(die < 5) {
					trajectory = Vector3.left;
				}
				else {
					trajectory = Vector3.right;
				}
			}
		}

		RaycastHit hitt;
		if(frames % 32 == 0){
			Vector3 origin = transform.position;
			Vector3 dirr = trajectory;
			Debug.DrawRay(origin, dirr, Color.red, 5f);
			if(Physics.Raycast(new Ray(origin, dirr), out hitt, 1f, 1 << 8)) {
				if(hitt.collider.gameObject.tag == "Obstacle") {
					////print ("Raycast hit obstacle");
					frames = 32 * squaresToMove;
				}
				else {
					////print ("No raycast hit");
					transform.Translate(trajectory / 32f);
				}
			} else {
				////print ("No raycast hit");
				transform.Translate(trajectory / 32f);
			}
		}
		else {
			transform.Translate(trajectory / 32f);
		}
		frames++;
	}
	
	void OnTriggerStay(Collider other) {
		if(other.gameObject.tag == "Sword") {
			bounce = 2f;
		}
		if(other.gameObject.tag == "Wall" || other.gameObject.name == "door") {
			snap ();
			frames = 32 * squaresToMove;
		}
		if(other.gameObject.tag == "Obstacle") {

		}
		if(other.gameObject == newBoomerang) {
			if(other.gameObject.GetComponent<moveBoomerang>().outgoing == false) {
				waiting = false;
				//print ("Destroying boomerang");
				Destroy(other.gameObject);
			}
			//else //print ("Keeping boomerang");
		}
	}
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Sword") {
			bounce = 4f;
			GameObject Link = GameObject.Find("Link");
			trajectory = Link.transform.position - transform.position;
			trajectory.z = 0;
			if(Mathf.Abs(trajectory.x) > Mathf.Abs(trajectory.y)) {
				trajectory.y = 0;
			}
			else {
				trajectory.x = 0;
			}
			trajectory.Normalize();
		}
		if(other.gameObject.tag == "Wall" || other.gameObject.name == "door") {
			snap ();
			bounce = 0;
			transform.Translate (trajectory / 32f);
			//frames = 32 * squaresToMove;
		}
		if(other.gameObject == newBoomerang) {

			if(other.gameObject.GetComponent<moveBoomerang>().outgoing == false) {
				waiting = false;
				//print ("Destroying boomerang");
				Destroy(other.gameObject);
			}
			else //print ("Keeping boomerang");
			snap ();
			bounce = 0;
			transform.Translate (trajectory / 32f);
			//frames = 32 * squaresToMove;
		}
	}
}