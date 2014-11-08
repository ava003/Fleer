using UnityEngine;
using System.Collections;

public class PlayerKnife : MonoBehaviour {

	#region ナイフのヒット判定
	void OnTriggerStay(Collider other){
		if(other.tag == "Enemy"){
			if(Input.GetKeyDown(KeyCode.R)){
				Debug.Log("Knife");
			}
		}
	}
	#endregion
}