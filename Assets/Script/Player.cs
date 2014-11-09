using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float WalkSpeed = 0.2f;	// プレイヤー移動スピード
	public float RunSpeed = 0.5f;
	public float HitDistance = 10.0f;	//弾が届く範囲

	public Material[] mat = new Material[5];

	private Camera Maincamera;
	private GameObject PlayChara;
	private int layerMask;
	private RaycastHit hit;

	private Animator anim;
	
	void Start () {
		PlayChara = GameObject.Find("Player");
		anim = GameObject.Find("Playchara").GetComponent<Animator>();
		Maincamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		layerMask = 1 << 9;
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
		float speed = WalkSpeed;
		bool Walk =false, Run = false;

		if(Input.GetKey(KeyCode.Z)){
			speed = RunSpeed;
			Run = true;
		}
		if(Input.GetKeyUp(KeyCode.Z)){
			PlayChara.rigidbody.velocity = Vector3.zero;
		}

		if (Input.GetKey (KeyCode.UpArrow)) {
			Walk = true;
			move += 1;
			PlayChara.rigidbody.velocity += PlayChara.transform.forward * speed;
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			Walk = true;
			move -= 1;
			PlayChara.rigidbody.velocity -= PlayChara.transform.forward * speed;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			Walk = true;
			move += 4;
			PlayChara.rigidbody.velocity += PlayChara.transform.right * speed;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			Walk = true;
			move -= 4;
			PlayChara.rigidbody.velocity -= PlayChara.transform.right * speed;
		}

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

		Debug.Log(speed);
	}
	#endregion

	#region 銃のヒット判定
	private void RayAttack(){
		if(Input.GetMouseButtonDown(0)){
			
			Ray ray = Maincamera.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast(ray, out hit, HitDistance, layerMask)){
				if(hit.collider.tag == "Enemy"){
					hit.collider.gameObject.renderer.material = mat[0];
				}
			}
		}
	}
	#endregion
}