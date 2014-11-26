using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
/* ステージごとのスコア基準 */
public struct Stagescore{
	public float timeScore;
	public int killScore;
	public int HSScore;
	public float accuracyScore;
};

public class ResultText : MonoBehaviour {
	
	public float enableTime = 1f;	//各項目の表示速度

	public Stagescore[] stageScore;	//ステージごとのスコア基準
	
	private Text time, kill, HS, accuracy, rank;	//各テキストコンポーネントの格納
	private int score = 0, stage = 0;		//スコアの記録

	void Start () {
		switch(Global.Stage){
		case enumStage.stage1:
			stage = 0;
			break;
		case enumStage.stage2:
			stage = 1;
			break;
		}

		//各テキストのコンポーネントを参照
		time = GameObject.Find("Time").GetComponent<Text>();
		kill = GameObject.Find("Kill").GetComponent<Text>();
		HS = GameObject.Find("HS").GetComponent<Text>();
		accuracy = GameObject.Find("Accuracy").GetComponent<Text>();
		rank = GameObject.Find("Rank").GetComponent<Text>();
		//各テキストの初期化
		time.text = "";
		kill.text = "";
		HS.text = "";
		accuracy.text = "";
		rank.text = "";
		Invoke ("TimeText", enableTime);	//時間テキストの表示
	}

	void Update () {
	
	}

	#region スコアの表示
	/* タイム */
	void TimeText(){
		if(Global.g_Time > stageScore[stage].timeScore){
			score += 25;
		}else if(Global.g_Time > stageScore[stage].timeScore){
			score += 20;
		}else if(Global.g_Time > stageScore[stage].timeScore){
			score += 15;
		}else if(Global.g_Time > stageScore[stage].timeScore){
			score += 10;
		}
		time.text = Global.g_Time.ToString ("##\\:##\\:##");
		Invoke ("KillText", enableTime);
	}
	/* キル数 */
	void KillText(){
		if(Global.g_Kill > stageScore[stage].killScore){
			score += 25;
		}else if(Global.g_Kill > stageScore[stage].killScore){
			score += 20;
		}else if(Global.g_Kill > stageScore[stage].killScore){
			score += 15;
		}else if(Global.g_Kill > stageScore[stage].killScore){
			score += 10;
		}
		kill.text = Global.g_Kill.ToString ("D") + "/";
		Invoke ("HSText", enableTime);
	}
	/* ヘッドショット数 */
	void HSText(){
		if(Global.g_HS > stageScore[stage].HSScore){
			score += 25;
		}else if(Global.g_HS > stageScore[stage].HSScore){
			score += 20;
		}else if(Global.g_HS > stageScore[stage].HSScore){
			score += 15;
		}else if(Global.g_HS > stageScore[stage].HSScore){
			score += 10;
		}
		HS.text = Global.g_HS.ToString ("D") + "/" + Global.g_Kill.ToString ("D");
		Invoke ("AccuracyText", enableTime);
	}
	/* 命中率 */
	void AccuracyText(){
		float acc = Global.g_Shot / Global.g_airShot;
		if(acc > stageScore[stage].accuracyScore){
			score += 25;
		}else if(acc > stageScore[stage].accuracyScore){
			score += 20;
		}else if(acc > stageScore[stage].accuracyScore){
			score += 15;
		}else if(acc > stageScore[stage].accuracyScore){
			score += 10;
		}
		if(acc.ToString ("0.00%") == "NaN"){
			accuracy.text = "NoShot";
		}else{
			accuracy.text = acc.ToString ("0.00%");
		}
		Invoke ("RankText", enableTime);
	}
	/* ステージランク */
	void RankText(){
		if(score > 90){
			rank.text = "S";
		}else if(score > 70){
			rank.text = "A";
		}else if(score > 50){
			rank.text = "B";
		}else if(score > 30){
			rank.text = "C";
		}else{
			rank.text = "D";
		}
	}
	#endregion
}
