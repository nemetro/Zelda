using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

	Text health;
	Text healthT;
	Text keys;
	Text bombs;
	Text level;
	Text map;

	void Start () {
		health = transform.Find("Life").gameObject.GetComponent<Text>(); 
		health.text= "";
		healthT = transform.Find("LifeTotal").gameObject.GetComponent<Text>(); 
		healthT.text= "";
		keys = transform.Find("Keys").gameObject.GetComponent<Text>(); 
		keys.text= Zelda.keys.ToString();
		bombs = transform.Find("Bombs").gameObject.GetComponent<Text>(); 
		bombs.text= Zelda.bombs.ToString();
		map = transform.Find("Map").gameObject.GetComponent<Text>();
	}
	
	void Update () {

		string newLife = "";
		for(int i = 0; i < Zelda.health; i++){
			if(i%2f == 0)
				newLife += "<";
			else
				newLife += "3";
		}
		health.text = newLife;
		string newLifeT = "";
		for(int i = 0; i < Zelda.MAX_HEALTH; i++){
			if(i%2f == 0)
				newLifeT += "<";
			else
				newLifeT += "3";
		}
		healthT.text = newLifeT;
		keys.text = Zelda.keys.ToString();
		bombs.text = Zelda.bombs.ToString();

		string coords = "(" + MoveCamera.xcoord + "," + MoveCamera.ycoord + ")" ;
		map.text = coords;
	}
}
