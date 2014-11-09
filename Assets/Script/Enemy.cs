using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float WalkSpeed = 1.0f;
	public float RunSpeed = 2.0f;
	public GameObject[] target = new GameObject[5];

	private bool alert = false;

	private GameObject PlayChara;

	void Start () {
		PlayChara = GameObject.Find("Player");
	}

	void Update () {
//		if(!alert){
//			EnemyIdle();
//		}else{
//			EnemyAlert();
//		}
	}

	#region アイドル状態
	void EnemyIdle(GameObject target){

	}
	#endregion

	#region アラート状態
	void EnemyAlert(){
		if(Vector3.Distance(this.transform.position, PlayChara.transform.position) < 5.0f){
			Vector3 targetPosition = PlayChara.transform.position;
			targetPosition.y = 0;
			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(targetPosition - this.transform.position), Time.time * 0.1f);
		
			this.transform.position += this.transform.forward * RunSpeed;
		}
	}
	#endregion

	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			alert = true;
		}
	}
}
