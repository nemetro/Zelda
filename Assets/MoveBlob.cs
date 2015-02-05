using UnityEngine;
using System.Collections;

public class MoveBlob : MonoBehaviour {


	private int frames = 0;
	private int squaresToMove = 1;
	private Vector3 trajectory = Vector3.left;
	private float bounce = 0;
	private RaycastHit hit;
	private bool waiting = false;
	private int pauseFrames = 0;

	void snap() {
		transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y),  transform.position.z);
	}	
	
	// Use this for initialization
	void Start () {
		snap();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(frames >= 32) {
			snap ();
			if(frames < 40) {
				// pause for a few frames after each square of movement
				frames++;
				return;
			}
			else {
				frames = 0;
				int die = Random.Range(0, 10);
				if(die < 4) {
					/* Trajectory stays the same */
				}
				else if(die < 6) {
					pauseFrames = 32;
				}
				else if(die < 7) {
					trajectory = new Vector3(trajectory.y, trajectory.x, trajectory.z);
				}
				else if(die < 8) {
					trajectory = new Vector3(-1 * trajectory.y, trajectory.x, trajectory.z);
				}
				else {
					trajectory = new Vector3(trajectory.y, -1 * trajectory.x, trajectory.z);
				}
			}
		}

		if(pauseFrames >= 0) {
			frames++;
			pauseFrames--;
			return;
		}

		RaycastHit hitt;
		if(frames % 32 == 0){
			Vector3 origin = transform.position;
			Vector3 dirr = trajectory;
			Debug.DrawRay(origin, dirr, Color.red, 5f);
			if(Physics.Raycast(new Ray(origin, dirr), out hitt, 1f, 1 << 8)) {
				if(hitt.collider.gameObject.tag == "Obstacle") {
					//print ("Raycast hit obstacle");
					frames = 32 * squaresToMove;
				}
				else {
					//print ("No raycast hit");
					transform.Translate(trajectory / 32f);
				}
			} else {
				//print ("No raycast hit");
				transform.Translate(trajectory / 32f);
			}
		}
		else {
			transform.Translate(trajectory / 32f);
		}
		frames++;
	}
	
	void OnTriggerStay(Collider other) {

		if(other.gameObject.tag == "Wall" || other.gameObject.name == "door") {
			snap ();
			frames = 32 * squaresToMove;
		}
		if(other.gameObject.tag == "Obstacle") {

		}
	}
	void OnTriggerEnter(Collider other) {

		if(other.gameObject.tag == "Wall" || other.gameObject.name == "door") {
			snap ();
			transform.Translate (trajectory / 32f);
			//frames = 32 * squaresToMove;
		}
	}
}