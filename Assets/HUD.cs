﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

	private Text health;
	private Text healthT;
	private Text keys;
	private Text bombs;
	private Image map;
	private Text xy;
	private Text compass;
	private Text deity;
	private Image C;

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
		C = transform.Find("C").gameObject.GetComponent<Image>();

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
		if(Zelda.obst)
			Destroy(C);

		//map
		string coords = "(" + MoveCamera.xcoord + "," + MoveCamera.ycoord + ")" ;
		xy.text = coords;
		if(Zelda.map)
			Destroy(map);
	}
}
