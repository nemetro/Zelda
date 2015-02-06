using UnityEngine;
using System.Collections;

public class arrowTiles : MonoBehaviour {
	
	public GameObject raft;
	public GameObject hydrantPre;
	public static GameObject hydrant;
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.name == "vanishingBlock(Clone)"){
			if(this.gameObject.name == "Up"){
				GameObject nH = Instantiate(hydrantPre, hydrant.transform.position, Quaternion.Euler(0, 0, 270f)) as GameObject;
				Destroy (hydrant);
				hydrant = nH;
				puzzle.facing = direction.north;
			}
			else if(this.gameObject.name == "Right"){
				GameObject nH = Instantiate(hydrantPre, hydrant.transform.position, Quaternion.Euler(0, 0, 180f)) as GameObject;
				Destroy (hydrant);
				hydrant = nH;
				puzzle.facing = direction.east;
			}
			else if(this.gameObject.name == "Left"){
				GameObject nH = Instantiate(hydrantPre, hydrant.transform.position, Quaternion.Euler(0, 0, 0f)) as GameObject;
				Destroy (hydrant);
				hydrant = nH;
				puzzle.facing = direction.west;
			}
			else if(this.gameObject.name == "Down"){
				GameObject nH = Instantiate(hydrantPre, hydrant.transform.position, Quaternion.Euler(0, 0, 90f)) as GameObject;
				Destroy (hydrant);
				hydrant = nH;
				puzzle.facing = direction.south;
			}
			print (puzzle.facing);
		}
	}
}
