using UnityEngine;
using System.Collections;

public class CameraAspect : MonoBehaviour {

	// 縦横比
	public float m_x_aspect = 4f;
	public float m_y_aspect = 3f;

	void Start () {
		//Screen.fullScreen = false;							// フルスクリーンモード
		//Screen.SetResolution (Screen.currentResolution.width, Screen.currentResolution.height, true);

		Camera mainCamera;
		GameObject[] cameras = GameObject.FindGameObjectsWithTag("MainCamera");
		Rect rect = calcAspect(m_x_aspect, m_y_aspect);	// 指定された比率からサイズ算出
		foreach(GameObject go in cameras){
			mainCamera = go.GetComponent<Camera>();
			mainCamera.rect = rect;							//カメラのサイズを指定
		}
	}

	void Update () {
	
	}
	
	public Rect calcAspect(float width, float height){
		float target_aspect = width / height;
		//float window_aspect = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
		float window_aspect = (float)Screen.width / (float)Screen.height;
		float scale_height = window_aspect / target_aspect;
		Rect rect = new Rect(0f, 0f, 1f, 1f);
		if(1f > scale_height){
			//縦が長い場合
			rect.x = 0;
			rect.y = (1f - scale_height) / 2f;
			rect.width = 1f;
			rect.height = scale_height;
		}else{
			//横が長い場合
			float scale_width = 1f / scale_height;
			rect.x = (1f - scale_width) / 2f;
			rect.y = 0f;
			rect.width = scale_width;
			rect.height = 1f;
		}
		return rect;
	}
}
