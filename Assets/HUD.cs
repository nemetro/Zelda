using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

	Text health;
	Text healthT;
	Text keys;
	Text bombs;
	Text level;
	Image map;
	Text xy;
	Text compass;
	Text deity;

	void Start () {
		health = transform.Find("Life").gameObject.GetComponent<Text>(); 
		health.text= "";
		healthT = transform.Find("LifeTotal").gameObject.GetComponent<Text>(); 
		healthT.text= "";
		keys = transform.Find("Keys").gameObject.GetComponent<Text>(); 
		keys.text= Zelda.keys.ToString();
		bombs = transform.Find("Bombs").gameObject.GetComponent<Text>(); 
		bombs.text= Zelda.bombs.ToString();
		xy = transform.Find("Coords").gameObject.GetComponent<Text>();
		map = transform.Find("Map").gameObject.GetComponent<Image>();
		deity = transform.Find("Deity").gameObject.GetComponent<Text>(); 
	}
	
	void Update () {
		//life
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
		//Deity
		if(Zelda.deity)
			deity.color = Color.yellow;
		else
			deity.color = Color.gray;

		//items
		keys.text = Zelda.keys.ToString();
		bombs.text = Zelda.bombs.ToString();

		//map
		string coords = "(" + MoveCamera.xcoord + "," + MoveCamera.ycoord + ")" ;
		xy.text = coords;
		if(Zelda.map)
			Destroy(map);
	}
}
