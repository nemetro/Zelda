using UnityEngine;
using System.Collections;

public class intro : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey (KeyCode.Alpha1))
			Application.LoadLevel("_Dungeon1_Troy2");
		else if(Input.GetKey(KeyCode.Alpha2))
			Application.LoadLevel ("_DungeonTroy_");
	}
}
