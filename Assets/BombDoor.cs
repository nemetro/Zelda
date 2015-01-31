using UnityEngine;
using System.Collections;

public class BombDoor : MonoBehaviour {
	public bool open = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider that) {
		print ("ENTER");
		OnTriggerStay(that);
	}
	void OnTriggerExit (Collider that){
		print ("EXIT");
		OnTriggerStay(that);
	}

	void OnTriggerStay(Collider that) {

		print ("COLLIDE");

		if(that.gameObject.tag == "Bomb" || that.name == "Boom(Clone)"){
			print ("BOMB");
			open = true;
//			this. = Color.black;
		}

		if(open && that.gameObject.tag == "Link"){
			switch (this.tag){
			case "North":
				MoveCamera.S.nextRoom (direction.north);
				Zelda.Z.MoveLink(direction.north);
				break; 
			case "East":
				MoveCamera.S.nextRoom (direction.east);
				Zelda.Z.MoveLink(direction.east);
				break;
			case "South":
				MoveCamera.S.nextRoom (direction.south);
				Zelda.Z.MoveLink(direction.south);
				break; 
			case "West":
				MoveCamera.S.nextRoom (direction.west);
				Zelda.Z.MoveLink(direction.west);
				break;
			case "Down":
				MoveCamera.S.nextRoom (direction.down);
				Zelda.Z.MoveLink(direction.down);
				break;
			case "Up":
				MoveCamera.S.nextRoom (direction.up);
				Zelda.Z.MoveLink(direction.up);
				break;
			default:
				print ("Broke");
				break;
			}
		}
	}
}
