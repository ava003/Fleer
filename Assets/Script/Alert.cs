using UnityEngine;
using System.Collections;

public class Alert : MonoBehaviour {

	public float AlertDistance = 10.0f;

	private Transform Root;
	private Enemy script;

	private Transform CharaSpine;
	private LayerMask layerMask;


	void Start(){
		CharaSpine = GameObject.Find("PlayerFolder/chara/Hips/spine").transform;
		Root = this.gameObject.transform.root;
		script = Root.GetComponent<Enemy>();

		layerMask = 1 << 9;
	}


	private void OnTriggerStay(Collider other){
		if(other.tag == "Player"){
			if(!Physics.Raycast(this.transform.position, CharaSpine.position, AlertDistance, layerMask)){
				script.alert = true;
				gameObject.GetComponent<SphereCollider>().enabled = false;
			}
		}
	}
}
