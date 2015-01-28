using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

	Text health;

	void Start () {
		health = transform.Find("Life").gameObject.GetComponent<Text>(); 
		health.text= "";
	}
	
	void Update () {
		//		if(health == null){
		//			print ("NULL");
		//			return;
		//		}
		string newLife = "";
		for(int i = 0; i < Zelda.health; i++){
			if(i%2f == 0)
				newLife += "<";
			else
				newLife += "3";
		}
		health.text = newLife;
	}
}
