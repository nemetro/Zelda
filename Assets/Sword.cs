using UnityEngine;
using System.Collections;
using System.Timers; 

public class Sword : MonoBehaviour {

	public static int framesToDelay = 10;
	private Vector3 trajectory;
	private int updates = 0;
	private bool move = false;
	// Use this for initialization
	void Start () {
		trajectory = Zelda.trajectory;
		if(trajectory.y == 0) trajectory *= -1;
//		print (move);
//		print (Zelda.Z.shooting);
		if(Zelda.health >= Zelda.MAX_HEALTH && !Zelda.Z.shooting){ 
			Zelda.Z.shooting = true;
			move = true;
		}
//		print(move);
	}

	void Update(){
		if(Zelda.Z.bounce > 0)
			Destroy(gameObject);
	}

	void FixedUpdate () {
		////print (trajectory);
		if(move) {
			transform.position += trajectory / 6f;
			//transform.Translate (trajectory / 8f); // Two pixels per frame
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
			if(other.gameObject.tag == "Wall" || other.gameObject.name == "door" || other.gameObject.tag == "Enemy") {
				Zelda.Z.shooting = false;
				Destroy(gameObject);
			}
		}
	}
}
