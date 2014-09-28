using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 6.0f;	// プレイヤー移動スピード

	// Use this for initialization
	void Start () {
		// フルスクリーンモード
		Screen.fullScreen = true;
	}
	
	// Update is called once per frame
	void Update () {
		// プレイヤーの移動
		this.transform.localPosition += new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime);
	}
}
