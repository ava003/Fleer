using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float HitPoint = 10f;	//最大ライフ
	public float Stamina = 100f;	//最大スタミナ
	public float WalkSpeed = 0.2f;	// プレイヤー移動スピード
	public float RunSpeed = 0.5f;
	public float HitDistance = 10f;	//弾が届く範囲

	public float NowStamina = 0f;	//スタミナ

	private Camera Maincamera;		//カメラ
	private GameObject PlayChara;	//PlayerFolder
	private GameObject chara;		//Player
	private GameObject Canvas;		//GameUI
	
	private Animator anim;			//Playerのアニメーター
	private CharacterController CharaCon;	//PlayerFolderのキャラクターコントロール

	private Vector3 PlayerPosition;	//Playerの初期位置
	private float animDefaultSpeed;		//デフォルトのアニメーション速度

	private int layerMask;			//レイヤーマスク
	private RaycastHit hit;			//レイキャストのヒットオブジェクト
	
	void Start () {
		PlayChara = GameObject.Find("PlayerFolder");	//PlayerFolderの格納
		Canvas = GameObject.Find("GameUI");				//GameUIの格納
		chara = GameObject.FindWithTag ("chara");		//Playerの格納

		anim = chara.GetComponent<Animator>();			//Playerのアニメーターを格納
		CharaCon = PlayChara.GetComponent<CharacterController> ();	//PlayerFolderのキャラクターコントロールを格納

		NowStamina = Stamina;							//スタミナの初期化
		PlayerPosition = chara.transform.localPosition;	//Playerの初期位置を格納

		Maincamera = GameObject.Find("Main Camera").GetComponent<Camera>();	//メインカメラの格納
		layerMask = ((1 << 9) + (1 << 10));				//レイヤーマスクを設定
	}

	void Update () {
		//ポーズしていない場合にループ
		if (!StageMenu.StagePause) {
			PlayerMove ();
			RayAttack ();
		}
		AnimPause();

		//モーションによる移動をリセット
		float offset = Vector3.Distance(PlayerPosition, chara.transform.localPosition);
		if(offset > 0.05f){
			float posX = Mathf.SmoothStep(chara.transform.localPosition.x, PlayerPosition.x, 0.1f);
			float posZ = Mathf.SmoothStep(chara.transform.localPosition.z, PlayerPosition.z, 0.1f);
			chara.transform.localPosition = new Vector3(posX, 0f, posZ);
		}
	}
	
	#region プレイヤーの移動
	private void PlayerMove(){
		/* プレイヤーの移動モーションについて
		 * +1……正面
		 * -1……後方
		 * +2＜……右
		 * -2＞……左
		 * 0……アイドル状態
		 */
		int move = 0;
		float speed = WalkSpeed;		//移動スピード
		bool Walk =false, Run = false;	//各フラグ

		Vector3 MoveDirection = Vector3.zero;	//移動量

		//カメラから見た方向の取得
		Vector3 forward = Maincamera.transform.TransformDirection(Vector3.forward);
		forward.y = 0;
		forward = forward.normalized;
		Vector3 right = new Vector3(forward.z, 0, -forward.x);

		if(Input.GetKey(KeyCode.Z) && NowStamina > 0){
			speed = RunSpeed;
			Run = true;
		}else if(!Input.GetKey(KeyCode.Z) && NowStamina < Stamina){
			NowStamina ++;
		}

		if (Input.GetKey (KeyCode.UpArrow)) {
			Walk = true;
			move += 1;
			MoveDirection += forward * speed;
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			Walk = true;
			move -= 1;
			MoveDirection -= forward * speed;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			Walk = true;
			move += 4;
			MoveDirection += right * speed;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			Walk = true;
			move -= 4;
			MoveDirection -= right * speed;
		}

		CharaCon.SimpleMove(MoveDirection);

		//Animatorへフラグと数値を送る
		if(Run && move != 0){
			NowStamina --;
			anim.SetBool("Run_Start", true);
			anim.SetBool("Walk_Start", false);
			anim.SetInteger("Move", move);
		}else if(Walk && move != 0){
			anim.SetBool("Run_Start", false);
			anim.SetBool("Walk_Start", true);
			anim.SetInteger("Move", move);
		}else{
			anim.SetBool("Run_Start", false);
			anim.SetBool("Walk_Start", false);
		}
	}
	#endregion

	#region 被ダメージ処理
	IEnumerator ApplyDamage(){
		HitPoint --;
		Canvas.SendMessage("HitPointDown");
		anim.SetBool("Damage", true);
		float timer = 0.0f;
		//待ち時間を超えるかAlert状態になったら抜ける
		while(timer <= 0.8f){
			timer += Time.deltaTime;
			yield return null;
		}
		anim.SetBool("Damage", false);
	}
	#endregion

	#region 銃のヒット判定
	private void RayAttack(){
		if(Input.GetMouseButtonDown(0)){
			Global.g_airShot ++;
			Ray ray = Maincamera.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast(ray, out hit, HitDistance, layerMask)){
				if(hit.collider.tag == "Enemy"){
					Global.g_Shot ++;
					string funstion = "";
					if(hit.collider.name == "head"){
						Global.g_HS ++;
						funstion = "EnemyDie";
					}else{
						funstion = "ApplyDamage";
					}
					Transform enemy = hit.collider.gameObject.transform.root;
					enemy.SendMessage(funstion);
				}
				Debug.Log(hit.collider.name);
			}
		}
	}
	#endregion

	void AnimPause(){
		if (StageMenu.StagePause) {
			if(anim.speed != 0f){
				animDefaultSpeed = anim.speed;
			}
			anim.speed = 0f;
		} else if(anim.speed == 0f && !StageMenu.StagePause){
			anim.speed = animDefaultSpeed;
		}
	}
}