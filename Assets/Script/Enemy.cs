using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int HitPoint = 5;			//ライフ
	public float WalkSpeed = 1f;		//移動スピード
	public float RunSpeed = 2f;
	public float AttackDistance = 10f;//攻撃してくる距離
	public float DamageWait = 1f;		//ダメージ待ち

	public Transform[] targets;			//巡回ルート

	public bool alert = false;			//発見されたかどうか
	private bool applyDamage = false;	//ダメージを受けているか

	private Transform PlayChara;		//Player
	private Animator anim;				//Enemyアニメーター
	private NavMeshAgent CharaNav;		//Enemyナビエージェント
	private GameObject enemyPrefab;		//RagDollのPrefab

	private string function = "";		//状態メソッド
	private int currentRoot = 0;		//現在のターゲット

	IEnumerator Start () {
		PlayChara = GameObject.Find("PlayerFolder").transform;	//Playerの参照
		anim = gameObject.GetComponent<Animator>();				//Enemyのアニメーターを参照
		CharaNav = GetComponent<NavMeshAgent> ();				//Enemyのナビエージェントを参照
		enemyPrefab = (GameObject)Resources.Load("Prefab/EnemyRagdoll");	//RagDollの参照

		CharaNav.speed = WalkSpeed;
		CharaNav.stoppingDistance = AttackDistance;

		function = "EnemyIdle";	//初期状態メソッド
		//動作を実行
		while (true) {
			yield return StartCoroutine (function);	//状態メソッドを実行
		}
	}

	void Update () {
		if(alert){
			function = "EnemyAlert";	//Alert状態のメソッドを指定
			CharaNav.speed = RunSpeed;
		}
	}

	#region 被ダメージ処理
	IEnumerator ApplyDamage(){
		HitPoint --;		//ライフの減少

		if(HitPoint <= 0){
			yield return StartCoroutine (EnemyDie());	//ライフが0になったら死亡
		}

		if(!alert){
			alert = true;	//発見されていなかったらAlert状態に
		}
		//被ダメのモーション再生
		if(!applyDamage){
			applyDamage = true;
			anim.SetBool("Damage", true);
			yield return new WaitForSeconds(DamageWait);
			applyDamage = false;
			anim.SetBool("Damage", false);
		}
		Debug.Log(HitPoint);
	}

	/* 死亡処理 */
	IEnumerator EnemyDie(){
<<<<<<< HEAD
=======
		Global.g_Kill ++;
>>>>>>> origin/master
		CharaNav.Stop();
		anim.SetBool("Damage", true);	//被ダメのモーション再生
		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);			//gameobjectを削除
		Instantiate (enemyPrefab, this.transform.position, this.transform.rotation);	//RagDollのPrefabを出現

	}
	#endregion

	#region アイドル状態
	IEnumerator EnemyIdle(){
		Vector3 pos = targets[currentRoot].position;	//ターゲットのポジション
		anim.SetBool("Walk", true);		//歩くモーション再生
		if(Vector3.Distance(this.transform.position, pos) < AttackDistance){
			anim.SetBool("Walk", false);	//歩くモーション停止
			float waittime = Random.Range(1f, 5f);		//待ち時間をランダムで決定
			float timer = 0f;
			//待ち時間を超えるかAlert状態になったら抜ける
			while(timer <= waittime && !alert){
				timer += Time.deltaTime;
				yield return null;
			}
			currentRoot = (currentRoot < targets.Length - 1) ? currentRoot + 1 : 0;
		}
		CharaNav.SetDestination(pos);
	}
	#endregion


	#region アラート状態
	void EnemyAlert(){
		Vector3 pos = PlayChara.position;	//Playerのポジション
		if(!applyDamage){
			if(Vector3.Distance(this.transform.position, pos) < AttackDistance){
				CharaNav.Stop();
				anim.SetTrigger("Attack");
			}else{
				anim.SetBool("Run", true);			//走るモーション再生
				CharaNav.SetDestination(pos);
			}
		}else{
			CharaNav.Stop();
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

}
