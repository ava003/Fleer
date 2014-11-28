using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PointerChange : MonoBehaviour {

	private Vector3 MousePosition;		//マウス位置
	private GameObject Pointer;			//カーソル

	void Start () {
		Screen.showCursor = false;								//マウスカーソルの非表示
		Pointer = GameObject.Find ("Pointer");
	}

	void Update () {
		MousePosition = Input.mousePosition;	//マウス位置の取得
		MousePosition.z = 10f;					//z方向の距離を固定
		Pointer.transform.position = MousePosition;
		//Pointer.transform.position = Camera.main.ScreenToWorldPoint(MousePosition);	//ワールド座標に変更しカーソルを移動
	}
}
