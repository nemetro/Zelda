using UnityEngine;
using System.Collections;

public class SpawnDoor : MonoBehaviour {

	public Enemy enemy;
	public float spawntime;
	public EnemyTypes type;
	public int xcoord, ycoord;
	private float timer = 0;
	private Vector3 spawnPnt;
	private bool blocked = false;
	private GameObject block;

	void Start(){
		spawnPnt = new Vector3(this.gameObject.transform.position.x, 
		                       this.gameObject.transform.position.y, 
		                       this.gameObject.transform.position.z - 1);
		timer = spawntime * 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
		if(block == null)
			blocked = false;
		if(MoveCamera.xcoord == xcoord && MoveCamera.ycoord == ycoord && !blocked){
			timer -= Time.deltaTime;
			if(timer <= 0){
				Enemy SpwnEnemy;
				if(type == EnemyTypes.Skelleton) {
					SpwnEnemy = Instantiate(enemy, spawnPnt, Quaternion.Euler(0, 0, 180)) as Enemy;
				}
				else {
					SpwnEnemy = Instantiate(enemy, spawnPnt, Quaternion.Euler(0, 0, 0)) as Enemy;
				}
				timer = spawntime;
				SpwnEnemy.xcoord = xcoord;
				SpwnEnemy.ycoord = ycoord;
				SpwnEnemy.type = type;
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.name == "vanishingBlock(Clone)"){
			blocked = true;
			block = other.gameObject;
		}
	}

	void OnTriggerStay(Collider other){
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
