using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	void Start () {
		Global.g_Time = 0f;
	}

	void Update () {
		if (StageMenu.StagePause) return;

		Global.g_Time += Time.deltaTime;
		//Debug.Log (Global.g_Time);
	}
}
