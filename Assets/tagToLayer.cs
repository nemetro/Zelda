using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class tagToLayer : MonoBehaviour {

	void Update () {
		GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
		foreach (GameObject obstacle in obstacles) {
			obstacle.transform.parent = null;
		}
		obstacles = GameObject.FindGameObjectsWithTag("Wall");
		foreach (GameObject obstacle in obstacles) {
			obstacle.transform.parent = null;
		}
	}

}