using UnityEngine;
using System.Collections;
using System.Timers; 

public class Sword : MonoBehaviour {

	public static int framesToDelay = 10;

	private int updates = 0;

	// Use this for initialization
	void Start () {
		print(transform.position);
	}

	void FixedUpdate () {
		updates++;
		if(updates == framesToDelay) {
			Destroy(gameObject);
		}
	}
}
