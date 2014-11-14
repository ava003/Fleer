using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public int HitPoint = 5;		//ライフ
	public float WalkSpeed = 1.0f;	//移動スピード
	public float RunSpeed = 2.0f;
	public float TurnTime = 20.0f;	//回転のスピード
	public float attackSpeed = 5.0f;
	public GameObject[] target = new GameObject[5];	//巡回ルート

	public bool alert = false;		//発見されたかどうか
	private bool damage = false;	//ダメージを受けているか

	private Transform PlayChara;
	private Animator anim;
	private CharacterController characterController;	//キャラクターコントローラコンポーネントへの参照
	private GameObject enemyPrefab;

	void Start () {
		PlayChara = transform.Find("PlayerFolder");
		anim = gameObject.GetComponent<Animator>();
		characterController = GetComponent<CharacterController> ();

		enemyPrefab = (GameObject)Resources.Load("Prefab/EnemyRagdoll");
	}

	void Update () {
		if(!alert){
			EnemyIdle();
		}else{
			EnemyAlert();
		}
	}

	#region ダメージ処理
	IEnumerator ApplyDamage(){
		HitPoint --;

		if(!alert){
			alert = true;
		}

		if(!damage){
			damage = true;
			anim.SetBool("Damage", true);
			yield return new WaitForSeconds(0.5f);
			damage = false;
			anim.SetBool("Damage", false);
		}

		if(HitPoint <= 0){
			EnemyDie();
		}
		Debug.Log(HitPoint);
	}

	//死亡処理
	void EnemyDie(){
		Destroy(gameObject);
		GameObject Doll = Instantiate (enemyPrefab, this.transform.position, this.transform.rotation) as GameObject;
		//Doll.transform.rigidbody.AddForce(new Vector3(1.0f, -10.0f, 100.0f), ForceMode.Impulse);
	}
	#endregion

	#region アイドル状態
	void EnemyIdle(){

	}
	#endregion

	#region アラート状態
	IEnumerator EnemyAlert(){
		float angle = 180.0f;
		float time = 0.0f;
		
		Vector3 direction;
		while (angle > 5 || time < TurnTime) {
			
			time += Time.deltaTime;
			angle = Mathf.Abs (EnemyRotate (PlayChara.position, 120f));
			float move = Mathf.Clamp01 ((90f - angle) / 90f);
			
			direction = transform.TransformDirection (Vector3.forward * attackSpeed * move);
			characterController.SimpleMove (direction);
			
			yield return new WaitForFixedUpdate ();
		}
	}

	float EnemyRotate(Vector3 targetPos, float rotateSpeed){
		Vector3 relative = transform.InverseTransformPoint (targetPos);
		float angle = Mathf.Atan2 (relative.x, relative.z) * Mathf.Rad2Deg;
		
		float maxRotation = rotateSpeed * Time.deltaTime;
		float clampedAngle = Mathf.Clamp (angle, -maxRotation, maxRotation);
		
		transform.Rotate (0, clampedAngle, 0);
		return angle;
	}
	#endregion

}
