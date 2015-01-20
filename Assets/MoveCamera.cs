using UnityEngine;
using System.Collections;

public enum direction {
	north,
	south,
	east,
	west,
	up,
	down,
}

public class MoveCamera : MonoBehaviour {

	static public MoveCamera S;
	public Vector3 trans;
	public Vector3 init;
	public float easing = 0.05f;
	
	void Awake(){
		S = this;
		this.transform.position = init;
	}

	void Update(){
//		if(Input.GetKeyDown(KeyCode.UpArrow))
//			nextRoom (direction.north);
//		if(Input.GetKeyDown(KeyCode.RightArrow))
//			nextRoom (direction.east);
//		if(Input.GetKeyDown(KeyCode.DownArrow))
//			nextRoom (direction.south);
//		if(Input.GetKeyDown(KeyCode.LeftArrow))
//			nextRoom (direction.west);
	}
	
	public void nextRoom (direction dir) {

		Vector3 dest = transform.position;
		switch (dir){
		case direction.north:
			dest.y += trans.y;
			break;
		case direction.south:
			dest.y -= trans.y;
			break;
		case direction.east:
			dest.x += trans.x;
			break;
		case direction.west:
			dest.x -= trans.x;
			break;
		case direction.up:
			dest.z += trans.z;
			break;
		case direction.down:
			dest.z += trans.z;
			break;
		default:
			print ("Broke");
			break;
		}
		transform.position = dest;
	}
}
