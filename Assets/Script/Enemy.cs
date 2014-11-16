using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int HitPoint = 5;			//ライフ
	public float WalkSpeed = 1.0f;		//移動スピード
	public float RunSpeed = 2.0f;
	public float RotateSpeed = 200.0f;	//回転のスピード
	public float TurnTime = 20.0f;		//回転の時間
	public float AttackDistance = 10.0f;	//攻撃してくる距離

	public GameObject[] targets = new GameObject[10];	//巡回ルート

	public bool alert = false;		//発見されたかどうか
	private bool applyDamage = false;	//ダメージを受けているか

	private Transform PlayChara;			//Player
	private Animator anim;					//Enemyアニメーター
	private CharacterController CharaCon;	//Enemyキャラクターコントローラコンポーネント
	private GameObject enemyPrefab;			//RagDollのPrefab

	private string function = "";			//状態メソッド
	private int targetNumber = 0;

	IEnumerator Start () {
		PlayChara = GameObject.Find("PlayerFolder").transform;	//Playerの参照
		anim = gameObject.GetComponent<Animator>();				//Enemyのアニメーターを参照
		CharaCon = GetComponent<CharacterController> ();		//Enemyのキャラクターコントロールを参照
		enemyPrefab = (GameObject)Resources.Load("Prefab/EnemyRagdoll");	//RagDollの参照

		function = "EnemyIdle";	//初期状態メソッド
		//動作を実行
		while (true) {
			yield return StartCoroutine (function);	//状態メソッドを実行
		}
	}

	void Update () {
		if(alert){
			function = "EnemyAlert";	//Alert状態のメソッドを指定
		}
	}

	#region 被ダメージ処理
	IEnumerator ApplyDamage(){
		HitPoint --;		//ライフの減少

		//被ダメのモーション再生
		if(!applyDamage){
			applyDamage = true;
			anim.SetBool("Damage", true);
			yield return new WaitForSeconds(1.0f);
			applyDamage = false;
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
	IEnumerator EnemyDie(){
		anim.SetBool("Damage", true);	//被ダメのモーション再生
		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);			//gameobjectを削除
		Instantiate (enemyPrefab, this.transform.position, this.transform.rotation);	//RagDollのPrefabを出現

	}
	#endregion

	#region アイドル状態
	IEnumerator EnemyIdle(){
		float angle = 180.0f;
		float time = 0.0f;
		
		Vector3 direction = Vector3.zero;		//進む量
		while ((angle > 5 || time < TurnTime) && !alert) {
			Transform target = targets[targetNumber].transform;			//次の巡回場所
			Vector3 offset = this.transform.position - target.position;	//Enemyを巡回場所の距離
			if(offset.magnitude > 1.0f){
				anim.SetBool("Walk", true);		//歩くモーション再生
				time += Time.deltaTime;
				angle = Mathf.Abs (EnemyRotate (target.position, RotateSpeed));
				float move = Mathf.Clamp01 ((90f - angle) / 90f);
				
				direction = this.transform.TransformDirection (Vector3.forward * WalkSpeed * move);
			}else{
				anim.SetBool("Walk", false);	//歩くモーション停止
				float waittime = Random.Range(1.0f, 5.0f);	//待ち時間をランダムで決定
				float timer = 0.0f;
				//待ち時間を超えるかAlert状態になったら抜ける
				while(timer <= waittime && !alert){
					timer += Time.deltaTime;
					yield return null;
				}
				if(targetNumber > targets.Length || targets[targetNumber+1] == null) {
					targetNumber = 0;	//巡回ルートのリセット
				}else{
					targetNumber ++;	//次の巡回場所
				}
			}
			CharaCon.SimpleMove (direction);	//Enemyを移動
			yield return new WaitForFixedUpdate();
		}
	}
	#endregion

	#region アラート状態
	IEnumerator EnemyAlert(){
		float angle = 180.0f;
		float time = 0.0f;
		
		Vector3 direction = Vector3.zero;
		while (angle > 5 || time < TurnTime) {
			//if(!applyDamage){
				Vector3 offset = this.transform.position - PlayChara.position;	//EnemyをPlayerの距離
				if(offset.magnitude < AttackDistance){
					anim.SetTrigger("Attack");
				}else{
					anim.SetBool("Run", true);
					time += Time.deltaTime;
					angle = Mathf.Abs (EnemyRotate (PlayChara.position, RotateSpeed));
					float move = Mathf.Clamp01 ((90f - angle) / 90f);
					
					direction = this.transform.TransformDirection (Vector3.forward * RunSpeed * move);
					CharaCon.SimpleMove (direction);
				}
			//}
			yield return new WaitForFixedUpdate();
		}
	}

	/* 攻撃メソッド */
	void EnemyAttack(){
		AnimatorStateInfo state = gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
		if(state.nameHash == Animator.StringToHash("Base Layer.Attack")){
			PlayChara.SendMessage("ApplyDamage");
		}
	}
	#endregion
	
	/* 対象の方向へ回転させるメソッド */
	float EnemyRotate(Vector3 targetPos, float rotateSpeed){
		Vector3 relative = this.transform.InverseTransformPoint (targetPos);
		float angle = Mathf.Atan2 (relative.x, relative.z) * Mathf.Rad2Deg;
		
		float maxRotation = rotateSpeed * Time.deltaTime;
		float clampedAngle = Mathf.Clamp (angle, -maxRotation, maxRotation);
		
		this.transform.Rotate (0, clampedAngle, 0);
		return angle;
	}
}
