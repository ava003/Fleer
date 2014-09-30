using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 6.0f;	// プレイヤー移動スピード

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		// プレイヤーの移動
		if (Input.GetKey (KeyCode.UpArrow)) {
			this.rigidbody.velocity += this.transform.forward * speed;
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			this.rigidbody.velocity -= this.transform.forward * speed;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			this.rigidbody.velocity += this.transform.right * speed;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			this.rigidbody.velocity -= this.transform.right * speed;
		}
	}
}
