using UnityEngine;
using System.Collections;

public class intro : MonoBehaviour {
	
	void Update () {
		if(Input.GetKey (KeyCode.Alpha1))
			Application.LoadLevel("_Dungeon1");
		else if(Input.GetKey(KeyCode.Alpha2))
			Application.LoadLevel ("_NewDungeon");
	}
}
