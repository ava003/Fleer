using UnityEngine;
using System.Collections;

public class MouseCamera : MonoBehaviour {
	public float speed = 2f;		//カメラスピード
	
	public float maximumY = 60f;	//Y方向の最大角度
	public float minimumY = -60f;	//Y方向の最小角度
	
	private Camera Maincamera;
	private GameObject PlayChara;
	private int layerMask;
	private RaycastHit hit;

	void Start () {
		PlayChara = GameObject.Find("PlayerFolder");
		Maincamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		layerMask = 1 << 8;
	}

	void Update () {
		Ray ray = Maincamera.ScreenPointToRay(Input.mousePosition);

		if (!Physics.Raycast(ray, out hit, 10, layerMask))
		{
			//if(this.transform.localEulerAngles.y < maximumY && Input.mousePosition.y > 10)
			//{
				Vector3 vec = Input.mousePosition;
				vec.z = 10f;
			PlayChara.transform.rotation = Quaternion.Slerp(PlayChara.transform.rotation, Quaternion.LookRotation(Maincamera.ScreenToWorldPoint(vec) - PlayChara.transform.position), speed);
			//}
			//Debug.Log(this.transform.localEulerAngles.y + ":" + Input.mousePosition.y);
		}
		
	}
}
