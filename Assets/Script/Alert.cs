using UnityEngine;
using System.Collections;

public class Alert : MonoBehaviour {

	public float AlertDistance = 10.0f;

	private Transform Root;	//一番上の親
	private Enemy script;	//親のスクリプト

	private Transform CharaSpine;	//Player
	private LayerMask layerMask;


	void Start(){
		CharaSpine = GameObject.Find("PlayerFolder/chara/Hips/PlayerHit").transform;	//Playerの参照
		Root = this.gameObject.transform.root;	//一番上の親の参照
		script = Root.GetComponent<Enemy>();	//親のスクリプトを参照

		layerMask = 1 << 10;	//レイヤーマスクの指定
	}


	private void OnTriggerStay(Collider other){
		//トリガー内にPlayerが入ってきた時,直線状に障害物がなければAlert状態に
		if(other.tag == "Player"){
			if(!Physics.Raycast(this.transform.position, CharaSpine.position, AlertDistance, layerMask)){
				script.alert = true;
				gameObject.GetComponent<SphereCollider>().enabled = false;	//コライダーの無効化
			}
		}
	}
}
