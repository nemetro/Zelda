using UnityEngine;
using System.Collections;

public class vanishingBlockScript : MonoBehaviour {
	private int frames = 0;
	// Use this for initialization
	void Start () {
		renderer.material.color = new Color(1, 1, 1, 0);
	}

	void FixedUpdate () {
		frames++;
		float fade = -1f * (frames/150f) * (frames/80f) + 1f;
		renderer.material.color = new Color(1, 1, 1, fade);
		if(fade <= 0) {
			Destroy(gameObject);
		}
	}
}
