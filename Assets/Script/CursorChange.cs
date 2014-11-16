using UnityEngine;
using System.Collections;

public class CursorChange : MonoBehaviour {

	private Vector3 MousePosition;		//マウス位置
	private Camera Maincamera;			//カメラ
	private GameObject Pointer;			//カーソル

	void Start () {
		Screen.showCursor = false;								//マウスカーソルの非表示
		Pointer = GameObject.Find("Pointer");					//カーソルの発見
		Maincamera = GameObject.Find("P-Camera").GetComponent<Camera>();	//カメラを発見
	}

	void Update () {
		MousePosition = Input.mousePosition;					//マウス位置の取得
		MousePosition.z = 10.0f;								//z方向の距離を固定
		Pointer.transform.position = Maincamera.ScreenToWorldPoint(MousePosition);	//ワールド座標に変更しカーソルを移動
	}
}
