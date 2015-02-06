using UnityEngine;
using System.Collections;

public class vanishingBlockScript : MonoBehaviour {
	private int frames = 0;
	// Use this for initialization
	void Start () {
	}

	void FixedUpdate () {
		frames++;
		float fade = -1f * (frames/500f) * (frames/500f) + 1f;
		renderer.material.color = new Color(1f, 1f, 1f, fade);
		if(fade <= 0) {
			Destroy(gameObject);
		}
	}
}
