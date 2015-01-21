using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public int health = 1;
	public int damage = 1; // Amount of damage to deal
	public Vector3 trajectory = Vector3.zero;
	private int frames = 51; 
	public float pixel = .0625f;
	// Use this for initialization
	void Start () {
		
	}
	
	void FixedUpdate () {
		frames++;
		if(frames < 20) {
			transform.Translate (trajectory*pixel);
		}
		else {
			// Pick a random direction
			trajectory =  new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
			trajectory.Normalize ();
			frames = 0;
		}
	}

	void OnTriggerStay(Collider other) {
	}

	void OnTriggerEnter(Collider other) {

		if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Obstacle") {
			trajectory = -1 * trajectory;
			transform.Translate (pixel * trajectory);
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
			print ("Enemy destroyed");
		}
	}
}