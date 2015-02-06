using UnityEngine;
using System.Collections;

public class MoveSkelleton : MonoBehaviour {
	private int frames = 0;
	private int squaresToMove = 2;
	private Vector3 trajectory = Vector3.left;
	private Vector3 bounceTrajectory;
	private float bounce = 0;
	private RaycastHit hit;
	void snap() {
		transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y),  transform.position.z);
	}	
	
	// Use this for initialization
	void Start () {
		snap();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(bounce > 0) {
			bounce -= 5f/16f;
			Vector3 orig = transform.position;
			Vector3 dir = -1 * bounceTrajectory;

			float magnitude = (0.5f + 5f/16f);
			Debug.DrawRay(orig, dir * magnitude, Color.cyan, 5f);
			if(!Physics.Raycast(new Ray(orig, dir), out hit, magnitude, 1 << 8)) {
				transform.Translate (dir * 5f / 16f);
			}
			else {
				bounce = 0;
			}
			return;
		}
		else if(frames >= 16 * squaresToMove) {
			frames = 0;
			snap ();
			squaresToMove = Random.Range (1, 7);
			if(trajectory == Vector3.left || trajectory == Vector3.right) {
				trajectory = Vector3.up;
			}
			else if(trajectory == Vector3.up || trajectory == Vector3.down || trajectory == Vector3.forward || trajectory == Vector3.back) {
				trajectory = Vector3.right;
			}
			if(Random.Range(-1f, 1f) < 0f) {
				trajectory = -1f * trajectory;
			}
		}

		Vector3 origin = transform.position;
		Vector3 dirr = -1 * (0.5f + 1f/16f) * trajectory;
		Debug.DrawRay(origin, dirr, Color.red, 5f);
		if(!Physics.Raycast(new Ray(origin, dirr), out hit, 0.5f + 1f/16f, 1 << 8)) {
			transform.Translate (trajectory / 22f);
		}

		frames++;
	}
	
	void OnTriggerStay(Collider other) {
		if(other.gameObject.tag == "Sword") {
			bounce = 2f;
		}
		if(other.gameObject.layer == 8 || other.gameObject.name == "door") {
			bounce = 0f;
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Sword") {
			bounceTrajectory = other.GetComponent<Sword>().trajectory;
			bounce = 4f;
		}
		if(other.gameObject.layer == 8 || other.gameObject.name == "door") {
			bounce = 0;
			trajectory = -1f * trajectory;
			//frames = 16 * squaresToMove;
		}
	}
}