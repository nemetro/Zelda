using UnityEngine;
using System.Collections;

public class SpawnDoor : MonoBehaviour {

	public Enemy enemy;
	public float spawntime;
	public int xcoord, ycoord;
	private float timer = 0;
	private Vector3 spawnPnt;
	private bool blocked = false;
	private GameObject block;

	void Start(){
		spawnPnt = new Vector3(this.gameObject.transform.position.x, 
		                       this.gameObject.transform.position.y, 
		                       this.gameObject.transform.position.z - 1);
//		BoxCollider box = this.gameObject.GetComponent<BoxCollider>();
//		Vector3 center, size;
//		center.x = 0.1667f;
//		center.y = -0.31f;
//		center.z = f;
//		size.x = 1f;
//		size.y = 1f;
//		size.z = 2f;
//		box.center = center;
//		box.size = size;
	}
	
	// Update is called once per frame
	void Update () {
		if(block == null)
			blocked = false;
		if(MoveCamera.xcoord == xcoord && MoveCamera.ycoord == ycoord && !blocked){
			timer -= Time.deltaTime;
			if(timer <= 0){
				print ("Spawn");
				Enemy SpwnEnemy = Instantiate(enemy, spawnPnt, Quaternion.Euler(0, 0, 180)) as Enemy;
				timer = spawntime;
				SpwnEnemy.xcoord = xcoord;
				SpwnEnemy.ycoord = ycoord;
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.name == "vanishingBlock(Clone)"){
			blocked = true;
			block = other.gameObject;
		}
	}
	void OnTriggerExit(Collider other){
		if(other.gameObject.name == "vanishingBlock(Clone)")
			blocked = false;
	}
}
