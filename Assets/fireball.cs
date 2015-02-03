using UnityEngine;
using System.Collections;

public enum FireDirection {up, center, down};

public class fireball : MonoBehaviour {
	public FireDirection dir;
	private Vector3 trajectory;

	// Use this for initialization
	void Start () {
		GameObject Link = GameObject.Find("Link");
		Vector3 accurateTrajectory = Link.gameObject.transform.position - transform.position;
		float accurateAngle = Mathf.Atan2 (accurateTrajectory.y, accurateTrajectory.x) * Mathf.Rad2Deg;
		float angle = Mathf.Round (accurateAngle / 15f) * 15f; // Round to fifteen degress
		if(dir == FireDirection.up) {
			angle += 15f;
		}
		else if(dir == FireDirection.down) {
			angle -= 15f;
		}
		trajectory = new Vector3(Mathf.Cos (angle * Mathf.Deg2Rad), Mathf.Sin (angle * Mathf.Deg2Rad), 0); // Unit vector in direction of 
		//print (dir + ", " + angle + ", " + trajectory);
	}
	
	void FixedUpdate () {
		transform.Translate (trajectory / 8f); // Advance in direction of trajectory by one pixel
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Wall" || other.gameObject.name == "Link") {
			Destroy(gameObject);
		}
	}
}
