using UnityEngine;
using System.Collections;

public enum enumStage{
	stage1,
	stage2
}

public class Global : MonoBehaviour {

	//各プレイデータ
	public static int g_Kill, g_HS;
	public static float g_Shot, g_airShot;
	public static float g_Time;
	
	public static enumStage Stage;	//プレイしたステージ

	void Start () {
	
	}

	void Update () {

	}
}
