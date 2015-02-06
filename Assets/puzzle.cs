using UnityEngine;
using System.Collections;

public class puzzle : MonoBehaviour {

	public static direction facing = direction.north;
	public GameObject raft;

	void Start(){
		arrowTiles.hydrant = this.gameObject;
	}

	void Update(){
		switch(facing){
			case direction.north:
				break;
			case direction.east:
				break;
			case direction.west:
				break;
			case direction.south:
				break;
			default: break;
		}
	}
}
