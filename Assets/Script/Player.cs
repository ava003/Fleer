using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 6.0f;	// プレイヤー移動スピード
	public float gravity = 2.0f; // 重力の設定

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		// プレイヤーの移動
		this.rigidbody.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * speed, -1 * gravity, Input.GetAxisRaw("Vertical") * speed);
	}
}
