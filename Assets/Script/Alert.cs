using UnityEngine;
using System.Collections;

public class Alert : MonoBehaviour {

	private Transform Root;	//一番上の親
	private Enemy script;	//親のスクリプト

	private Transform CharaSpine;	//Player
	private LayerMask layerMask;
	private SphereCollider spCollider;
	private float Distance;


	void Start(){
		CharaSpine = GameObject.FindWithTag("Player").transform;	//Playerの参照
		Root = this.gameObject.transform.root;	//一番上の親の参照
		script = Root.GetComponent<Enemy>();	//親のスクリプトを参照
		spCollider = gameObject.GetComponent<SphereCollider>();

		layerMask = 1 << 10;	//レイヤーマスクの指定
		Distance = (spCollider.radius * Mathf.Sqrt(3));
	}


	private void OnTriggerStay(Collider other){
		//トリガー内にPlayerが入ってきた時,直線状に障害物がなければAlert状態に
		if(other.tag == "Player"){
			if(!Physics.Raycast(this.transform.position, CharaSpine.position, Distance, layerMask)){
				if(Vector3.Distance(this.transform.position, CharaSpine.position) < Distance){
					script.alert = true;
					gameObject.GetComponent<SphereCollider>().enabled = false;	//コライダーの無効化
				}
			}
		}
	}
}
