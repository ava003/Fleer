using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 6.0f;	// プレイヤー移動スピード
	public float HitDistance = 10.0f;	//弾が届く範囲

	public Material[] mat = new Material[5];

	private Camera Maincamera;
	private GameObject PlayChara;
	private GameObject knife;
	private int layerMask;
	private RaycastHit hit;
	
	void Start () {
		PlayChara = GameObject.Find("Player");
		knife = GameObject.Find("knife");
		Maincamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		layerMask = 1 << 9;
	}

	void Update () {
		PlayerMove();
		RayAttack();
	}
	
	#region プレイヤーの移動
	private void PlayerMove(){
		if (Input.GetKey (KeyCode.UpArrow)) {
			PlayChara.rigidbody.velocity += PlayChara.transform.forward * speed;
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			PlayChara.rigidbody.velocity -= PlayChara.transform.forward * speed;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			PlayChara.rigidbody.velocity += PlayChara.transform.right * speed;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			PlayChara.rigidbody.velocity -= PlayChara.transform.right * speed;
		}
	}
	#endregion

	#region 銃のヒット判定
	private void RayAttack(){
		if(Input.GetMouseButtonDown(0)){
			
			Ray ray = Maincamera.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast(ray, out hit, HitDistance, layerMask)){
				if(hit.collider.tag == "Enemy"){
					hit.collider.gameObject.renderer.material = mat[0];
				}
			}
		}
	}
	#endregion
	
	#region ナイフのヒット判定
	private void OnCollisionEnter(Collision collision){

	}
	#endregion
}