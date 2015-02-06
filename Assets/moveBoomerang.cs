using UnityEngine;
using System.Collections;
using System.Timers; 

public class moveBoomerang : MonoBehaviour {
	public static int framesToDelay = 10;
	public Vector3 trajectory;
	private int updates = 0;
	public bool outgoing = true;
	//private bool move = false;
	// Use this for initialization
	void Start () {
		if(trajectory.y == 0) trajectory *= -1;
		//if(Zelda.health >= Zelda.MAX_HEALTH) move = true;
		//print(trajectory);
	}
	
	void FixedUpdate () {
		//print (trajectory);
		//if(move) {
			////print (trajectory);
			transform.position += trajectory / 8f;
			//transform.Translate (trajectory / 8f); // Two pixels per frame
			return;
		//}
		updates++;
		//if(updates == framesToDelay) {
			//Destroy(gameObject);
		//}
	}
	
	void OnTriggerEnter(Collider other) {
		//if(move){
			if(other.gameObject.tag == "Wall" || other.gameObject.name == "door" || other.gameObject.tag == "Link") {
				if(outgoing) {
					trajectory = -1 * trajectory;
					outgoing = false;
				}
			}
		//}
	}
}
