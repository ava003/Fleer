using UnityEngine;
using System.Collections;

public class CameraWH : MonoBehaviour {

	// 縦横比
	public float m_x_aspect = 4.0f;
	public float m_y_aspect = 3.0f;

	// Use this for initialization
	void Start () {
		// フルスクリーンモード
		Screen.fullScreen = true;
		// カメラの検索
		Camera camera = GetComponent<Camera>();
		// 指定された比率からサイズ算出
		Rect rect = calcAspect(m_x_aspect, m_y_aspect);
		// カメラの比率を変更
		camera.rect = rect;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/* 指定された縦横比からカメラの縦横比を変更 */
	public Rect calcAspect(float width, float height){
		float target_aspect = width / height;
		float window_aspect = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
		float scale_height = window_aspect / target_aspect;
		Rect rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
		if(1.0f > scale_height){
			//縦が長い場合
			rect.x = 0;
			rect.y = (1.0f - scale_height) / 2.0f;
			rect.width = 1.0f;
			rect.height = scale_height;
		}
		else{
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
