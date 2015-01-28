using UnityEngine;
using System.Collections;

public class Doors : MonoBehaviour {

	public GameObject door;

	void Start(){
		door = this.gameObject;
	}

	void OnTriggerEnter(Collider that) {

		if(that.gameObject.tag != "Link")
			return;

		switch (door.tag){
		case "North":
			MoveCamera.S.nextRoom (direction.north);
			Zelda.Z.MoveLink(direction.north, 0);
			break; 
		case "East":
			MoveCamera.S.nextRoom (direction.east);
			Zelda.Z.MoveLink(direction.east, 0);
			break;
		case "South":
			MoveCamera.S.nextRoom (direction.south);
			Zelda.Z.MoveLink(direction.south, 0);
			break; 
		case "West":
			MoveCamera.S.nextRoom (direction.west);
			Zelda.Z.MoveLink(direction.west, 0);
			break;
		case "Down":
			MoveCamera.S.nextRoom (direction.down);
			Zelda.Z.MoveLink(direction.down, 0);
			break;
		case "Up":
			MoveCamera.S.nextRoom (direction.up);
			Zelda.Z.MoveLink(direction.up, 0);
			break;
		default:
			print ("Broke");
			break;
		}
	}


}
