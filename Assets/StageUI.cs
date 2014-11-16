using UnityEngine;
using System.Collections;

public class StageUI : MonoBehaviour {

	private Player script;		//Playerのスクリプト
	private GameObject HP,ST;	//HitPoint, Stamina
	private float HPpar;		//HP減少の大きさ

	void Start () {
		script = GameObject.Find("PlayerFolder").GetComponent<Player>();	//Playerのスクリプトを参照
		HP = GameObject.Find("HP");		//HPオブジェクトを参照
		ST = GameObject.Find("ST");		//STオブジェクトを参照

		HPpar = HP.transform.localScale.x / script.HitPoint;	//Playerのライフ分で等分
	}

	void Update(){
		ST.transform.localScale = new Vector3(script.NowStamina / script.Stamina, 1f, 1f);
	}

	void HitPointDown(){
		HP.transform.localScale -= new Vector3(HPpar, 0f, 0f);	//ダメージ分小さくする
	}
}
