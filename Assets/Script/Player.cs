using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float WalkSpeed = 0.2f;	// プレイヤー移動スピード
	public float RunSpeed = 0.5f;
	public float HitDistance = 10.0f;	//弾が届く範囲

	private Camera Maincamera;
	private Transform cameraT;
	private GameObject PlayChara;
	private int layerMask;
	private RaycastHit hit;

	private Animator anim;
	private GameObject enemyPrefab;
	private CharacterController CharaCon;
	
	void Start () {
		PlayChara = GameObject.Find("PlayerFolder");
		anim = GameObject.Find("chara").GetComponent<Animator>();
		CharaCon = PlayChara.GetComponent<CharacterController> ();

		Maincamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		cameraT = Maincamera.transform;
		layerMask = ((1 << 9) + (1 << 10));
	}

	void Update () {
		PlayerMove();
		RayAttack();
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
		Vector3 forward = cameraT.TransformDirection(Vector3.forward);
		forward.y = 0;
		forward = forward.normalized;
		Vector3 right = new Vector3(forward.z, 0, -forward.x);

		if(Input.GetKey(KeyCode.Z)){
			speed = RunSpeed;
			Run = true;
		}

		if (Input.GetKey (KeyCode.UpArrow)) {
			Walk = true;
			move += 1;
			//PlayChara.rigidbody.velocity += PlayChara.transform.forward * speed;
			MoveDirection += forward * speed;
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			Walk = true;
			move -= 1;
			//PlayChara.rigidbody.velocity -= PlayChara.transform.forward * speed;
			MoveDirection -= forward * speed;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			Walk = true;
			move += 4;
			//PlayChara.rigidbody.velocity += PlayChara.transform.right * speed;
			MoveDirection += right * speed;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			Walk = true;
			move -= 4;
			//PlayChara.rigidbody.velocity -= PlayChara.transform.right * speed;
			MoveDirection -= right * speed;
		}

		CharaCon.SimpleMove(MoveDirection);

		//Animatorへフラグと数値を送る
		if(Run && move != 0){
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

	#region 銃のヒット判定
	private void RayAttack(){
		if(Input.GetMouseButtonDown(0)){
			
			Ray ray = Maincamera.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast(ray, out hit, HitDistance, layerMask)){
				if(hit.collider.tag == "Enemy"){
					string funstion = "";
					if(hit.collider.name == "head"){
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
}