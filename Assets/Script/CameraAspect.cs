using UnityEngine;
using System.Collections;

public class CameraAspect : MonoBehaviour {

	// 縦横比
	public float m_x_aspect = 4.0f;
	public float m_y_aspect = 3.0f;

	public Camera[] camera = new Camera[5];	//シーン内のカメラを格納

	void Start () {
		//Screen.fullScreen = false;							// フルスクリーンモード
		//Screen.SetResolution (Screen.currentResolution.width, Screen.currentResolution.height, true);

		for(int i=0; i<camera.Length; i++){
			Rect rect = calcAspect(m_x_aspect, m_y_aspect);	// 指定された比率からサイズ算出
			camera[i].rect = rect;							//カメラのサイズを指定
		}
	}

	void Update () {
	
	}
	
	public Rect calcAspect(float width, float height){
		float target_aspect = width / height;
		//float window_aspect = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
		float window_aspect = (float)Screen.width / (float)Screen.height;
		float scale_height = window_aspect / target_aspect;
		Rect rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
		if(1.0f > scale_height){
			//縦が長い場合
			rect.x = 0;
			rect.y = (1.0f - scale_height) / 2.0f;
			rect.width = 1.0f;
			rect.height = scale_height;
		}else{
			//横が長い場合
			float scale_width = 1.0f / scale_height;
			rect.x = (1.0f - scale_width) / 2.0f;
			rect.y = 0.0f;
			rect.width = scale_width;
			rect.height = 1.0f;
		}
		return rect;
	}
}
