using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	private Transform Root;
	private Enemy script;

	void Start () {
		Root = this.gameObject.transform.root;
		script = Root.GetComponent<Enemy>();
	}
	
	private void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			Root.SendMessage("EnemyAttack");
		}
	}
}
