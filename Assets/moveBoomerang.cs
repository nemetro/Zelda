using UnityEngine;
using System.Collections;
using System.Timers; 

public class moveBoomerang : MonoBehaviour {
	public static int framesToDelay = 10;
	public Vector3 trajectory;
	private int updates = 0;
	public bool outgoing = true;

	void Start () {
		if(trajectory.y == 0) trajectory *= -1;
	}
	
	void FixedUpdate () {
		transform.position += trajectory / 8f;
		return;
		updates++;
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Wall" || other.gameObject.name == "door" || other.gameObject.tag == "Link") {
			if(outgoing) {
				trajectory = -1 * trajectory;
				outgoing = false;
			}
		}
	}
}
