using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public int HitPoint = 5;			//ライフ
	public float WalkSpeed = 1.0f;		//移動スピード
	public float RunSpeed = 2.0f;
	public float RotateSpeed = 200.0f;	//回転のスピード
	public float TurnTime = 20.0f;		//回転の時間
	public float AttackDistance = 10.0f;	//攻撃してくる距離

	public GameObject[] target = new GameObject[5];	//巡回ルート

	public bool alert = false;		//発見されたかどうか
	private bool damage = false;	//ダメージを受けているか

	private Transform PlayChara;			//Player
	private Animator anim;					//Enemyアニメーター
	private CharacterController CharaCon;	//Enemyキャラクターコントローラコンポーネント
	private GameObject enemyPrefab;			//RagDollのPrefab

	private string function = "";			//状態メソッド

	IEnumerator Start () {
		PlayChara = GameObject.Find("PlayerFolder").transform;	//Playerの参照
		anim = gameObject.GetComponent<Animator>();				//Enemyのアニメーターを参照
		CharaCon = GetComponent<CharacterController> ();		//Enemyのキャラクターコントロールを参照
		enemyPrefab = (GameObject)Resources.Load("Prefab/EnemyRagdoll");	//RagDollの参照

		function = "EnemyIdle";	//初期メソッド
		//動作を実行(無限ループ)
		while (true) {
			yield return StartCoroutine (function);	//攻撃状態を実行

		}
	}

	void Update () {
		if(alert){
			function = "EnemyAlert";	//Alert状態のメソッドを指定
		}
	}

	#region ダメージ処理
	IEnumerator ApplyDamage(){
		HitPoint --;		//ライフの減少

		if(!damage){
			damage = true;
			anim.SetBool("Damage", true);
			yield return new WaitForSeconds(1.0f);
			damage = false;
			anim.SetBool("Damage", false);
		}
		
		if(!alert){
			alert = true;	//発見されていなかったらAlert状態に
		}

		if(HitPoint <= 0){
			EnemyDie();		//ライフが0になったら死亡
		}
		Debug.Log(HitPoint);
	}

	/* 死亡処理 */
	void EnemyDie(){
		Destroy(gameObject);	//gameobjectを削除
		GameObject Doll = Instantiate (enemyPrefab, this.transform.position, this.transform.rotation) as GameObject;
		//Doll.transform.rigidbody.AddForce(new Vector3(1.0f, -10.0f, 100.0f), ForceMode.Impulse);
	}
	#endregion

	#region アイドル状態
	IEnumerator EnemyIdle(){
		
		yield return new WaitForFixedUpdate();
//		//クロックとの距離が近づくまでループ
//		while (true) {
//			CharaCon.SimpleMove (Vector3.zero);
//			yield return new WaitForSeconds (0.2f);
//			
//			//クロックとの距離が近づいたらループを抜ける
//			Vector3 offset = transform.position - PlayChara.position;
//			if (offset.magnitude < 1.0f)
//				break;
//		}
	}
	#endregion

	#region アラート状態
	IEnumerator EnemyAlert(){
		float angle = 180.0f;
		float time = 0.0f;
		
		Vector3 direction;
		while (angle > 5 || time < TurnTime) {

			Vector3 offset = this.transform.position - PlayChara.position;
			if(offset.magnitude < AttackDistance){
				anim.SetTrigger("Attack");
				yield return new WaitForSeconds(0.5f);
			}else{
				anim.SetBool("Run", true);
				time += Time.deltaTime;
				angle = Mathf.Abs (EnemyRotate (PlayChara.position, RotateSpeed));
				float move = Mathf.Clamp01 ((90f - angle) / 90f);
				
				direction = this.transform.TransformDirection (Vector3.forward * RunSpeed * move);
				CharaCon.SimpleMove (direction);
			}
			
			yield return new WaitForFixedUpdate();
		}
	}

	/* 回転させるメソッド */
	float EnemyRotate(Vector3 targetPos, float rotateSpeed){
		Vector3 relative = this.transform.InverseTransformPoint (targetPos);
		float angle = Mathf.Atan2 (relative.x, relative.z) * Mathf.Rad2Deg;
		
		float maxRotation = rotateSpeed * Time.deltaTime;
		float clampedAngle = Mathf.Clamp (angle, -maxRotation, maxRotation);
		
		this.transform.Rotate (0, clampedAngle, 0);
		return angle;
	}
	#endregion

}
