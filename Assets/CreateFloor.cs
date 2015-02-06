using UnityEngine;
using System.Collections;

public class CreateFloor : MonoBehaviour {

	public GameObject tilePrefab;
	float xWall = 4f;
	float yWall = 4f;

	void Start () {
		float x=xWall/2f, y=yWall/2f;
		int xBlocks=0, yBlocks=0;
		while(xBlocks < 12*6){
			if(xBlocks != 0 && xBlocks % 12f == 0){
				x += xWall;
			}
			while(yBlocks < 7*6){
				if(yBlocks != 0 && yBlocks % 7f == 0f){
					y += yWall;
				}

				Instantiate(tilePrefab, new Vector3(x, y, 0f), Quaternion.identity);
				y += 1f;
				yBlocks++;
			}
			yBlocks = 0;
			y = yWall/2f;
			
			x += 1f;
			xBlocks++;
		}
	}
}
