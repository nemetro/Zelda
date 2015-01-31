using UnityEngine;
using System.Collections;

public class Doors : MonoBehaviour {

	public GameObject door;
	bool open;


	void Start(){
		door = this.gameObject;
		switch(door.name){
		case "BombDoor(Clone)":
			open = true;
			break;
		case "Locked":
			open = false;
			door.tag = "Wall";
			break;
		case "BlockedDoor":
			open = true;
			//door.tag = "Wall";
			break;
		default:
			open = true;
			break;
		}
	}

	void OnTriggerEnter(Collider that) {

		if(that.gameObject.tag != "Link")
			return;
		float dist = 0;
		switch(door.name){
		case "BombDoor(Clone)":
			dist = 4.5f;
			break;
		case "Locked":
			if(that.gameObject.tag == "Link" && Zelda.keys > 0)
				Destroy(door);
			break;
		default:
			break;
		}

		if(!open)
			return;

		switch (door.tag){
		case "North":
			MoveCamera.S.nextRoom (direction.north);
			Zelda.Z.MoveLink(direction.north, dist);
			break; 
		case "East":
			MoveCamera.S.nextRoom (direction.east);
			Zelda.Z.MoveLink(direction.east, dist);
			break;
		case "South":
			MoveCamera.S.nextRoom (direction.south);
			Zelda.Z.MoveLink(direction.south, dist);
			break; 
		case "West":
			MoveCamera.S.nextRoom (direction.west);
			Zelda.Z.MoveLink(direction.west, dist);
			break;
		case "Stairs":
			if(door.name == "Down"){
				MoveCamera.S.nextRoom (direction.down);
				Zelda.Z.MoveLink(direction.down, dist);
			}
			else{
				MoveCamera.S.nextRoom (direction.up);
				Zelda.Z.MoveLink(direction.up, dist);
			}
			break;
		default:
			print ("Broke");
			break;
		}
	}


}
