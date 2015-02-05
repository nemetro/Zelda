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
	public string Level;
	public Vector3 trans;
	public Vector3 init;
	public static int xcoord, ycoord;
	public float easing = 0.05f;
	
	void Start(){
		S = this;
		this.transform.position = init;
		if(Level == "1"){
			xcoord = 2;
			ycoord = 5;
		}
		else{
			xcoord = 1;
			ycoord = 3;
		}
	}

	public void nextRoom (direction dir) {

		Vector3 dest = transform.position;
		switch (dir){
		case direction.north:
			dest.y += trans.y;
			ycoord--;
			break;
		case direction.south:
			dest.y -= trans.y;
			ycoord++;
			break;
		case direction.east:
			dest.x += trans.x;
			xcoord++;
			break;
		case direction.west:
			dest.x -= trans.x;
			xcoord--;
			break;
		case direction.up:
			dest.y += trans.y;
			break;
		case direction.down:
			dest.y -= trans.y;
			break;
		default:
			//print ("Broke");
			break;
		}
		transform.position = dest;
	}
}
