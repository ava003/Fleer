using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	public float scrollSpeed = 0.5f;

	void Start () {
	
	}

	void Update () {
		float offset = Time.time * scrollSpeed;
		this.renderer.material.SetTextureOffset ("_MainTex", new Vector2(0f, -offset));
	}

	private void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			Application.LoadLevel("result");
		}
	}
}
