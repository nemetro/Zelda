using UnityEngine;
using System.Collections;
using System.Timers; 

public class Sword : MonoBehaviour {

	public static int framesToDelay = 10;
	public Vector3 trajectory;
	private int updates = 0;
	private bool move = false;
	// Use this for initialization
	void Start () {
		trajectory = Zelda.trajectory;
		if(trajectory.y == 0) trajectory *= -1;

		if(Zelda.health >= Zelda.MAX_HEALTH && !Zelda.Z.shooting){ 
			Zelda.Z.shooting = true;
			move = true;
		}
	}

	void Update(){
		if(Zelda.Z.bounce > 0)
			Destroy(gameObject);
	}

	void FixedUpdate () {
		if(move) {
			transform.position += trajectory / 6f;
			return;
		}
		updates++;
		if(updates == framesToDelay) {
			Zelda.Z.shooting = false;
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other) {
		if(move){
			if(other.gameObject.tag == "Wall" || other.gameObject.name == "door" || 
			   	other.gameObject.tag == "Enemy") {
				Zelda.Z.shooting = false;
				Destroy(gameObject);
			}
		}
	}
}
