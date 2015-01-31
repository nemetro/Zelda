using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {
	private float timer;
	public Transform boom;
	public GameObject splode;

	// Use this for initialization
	void Start () {
		timer = 1f;
		if(this.name == "Boom(Clone)"){
			timer = .5f;
		}

	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if(timer < 0){
			if(this.name == "Bomb(Clone)"){
				splode = Instantiate(boom, transform.position, Quaternion.identity) as GameObject;
				Zelda.Z.bombing = false;
			}
			Destroy(this.gameObject);

		}
	}

}
