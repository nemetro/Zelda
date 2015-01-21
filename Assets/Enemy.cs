using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int health = 1;
	public int damage; // Amount of damage to deal

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(health < 1)
			Destroy(this.gameObject);
	}

	void OnTriggerEnter(Collider other) {
		
		if (other.gameObject.tag == "Sword") {
			print ("hit");
			health--;
		}
	}
}
