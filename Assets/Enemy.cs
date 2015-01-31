using UnityEngine;
using System.Collections;

public enum EnemyTypes {Bat, Skelleton, Blob, Dragon};
public class Enemy : MonoBehaviour {
	public EnemyTypes type;
	public int health = 1;
	public int damage = 1; // Amount of damage to deal
	public Vector3 trajectory = Vector3.zero;
	private int frames = 51; 
	public float pixel = .0625f;
	// Use this for initialization
	void Start () {
		if(type == EnemyTypes.Skelleton) {
			frames = 75;
			trajectory = Vector3.left;
			health = 2;
		}
		else if (type == EnemyTypes.Dragon) {
			health = 2;
		}
	}
	
	void FixedUpdate () {
		frames++;
		if(frames < 20) {
			transform.Translate (trajectory*pixel);
		}
		else {
			// Pick a random direction
			if(type == EnemyTypes.Bat) {
				trajectory =  new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
				trajectory.Normalize ();
			}
			else if(type == EnemyTypes.Skelleton) {
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

			frames = 0;
		}
	}

	void OnTriggerStay(Collider other) {
	}

	void OnTriggerEnter(Collider other) {

		if(other.gameObject.tag == "Link"){
			Zelda.health -= damage;
			print (Zelda.health);
			print ("HIT");
		}
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