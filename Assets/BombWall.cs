using UnityEngine;
using System.Collections;

public class BombWall : MonoBehaviour {

	public GameObject bombdoor;
	public GameObject newdoor1, newdoor2;
	public direction dir;

	void Start(){
	}

	void OnTriggerEnter (Collider that) {
		if(that.gameObject.tag == "Bomb" || that.name == "Boom(Clone)"){
			//print ("BOMB");
			Vector3 dest = this.transform.position;
			switch (dir){
			case direction.north:
				dest.y-=1.5f;
				newdoor1 = Instantiate(bombdoor, dest, Quaternion.AngleAxis(270, Vector3.up)) as GameObject;
				dest.y+=3f;
				newdoor2 = Instantiate(bombdoor, dest, Quaternion.identity) as GameObject;
				newdoor1.tag = "North";
				newdoor2.tag = "South";
				break;
			case direction.south:
				dest.y+=1.5f;
				newdoor1 = Instantiate(bombdoor, dest, Quaternion.identity) as GameObject;
				dest.y-=3f;
				newdoor2 = Instantiate(bombdoor, dest, Quaternion.AngleAxis(270, Vector3.up)) as GameObject;
				newdoor1.tag = "South";
				newdoor2.tag = "North";
				break;
			case direction.east:
				dest.x+=1.5f;
				newdoor1 = Instantiate(bombdoor, dest, Quaternion.identity) as GameObject;
				dest.x-=3f;
				newdoor2 = Instantiate(bombdoor, dest, Quaternion.identity) as GameObject;
				newdoor1.tag = "East";
				newdoor2.tag = "West";
				break;
			case direction.west:
				dest.x+=1.5f;
				newdoor1 = Instantiate(bombdoor, dest, Quaternion.identity) as GameObject;
				dest.x-=3f;
				newdoor2 = Instantiate(bombdoor, dest, Quaternion.identity) as GameObject;
				newdoor1.tag = "West";
				newdoor2.tag = "East";
				break;
			default:
				//print ("Broke");
				break;
			}

			Destroy (this.gameObject);
		}
	}
}
