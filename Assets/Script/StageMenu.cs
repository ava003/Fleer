using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

enum MenuState{
	Stage,
	Option,
	Exit,
	Resume
}

/*ゲーム設定
 ・明るさ
 ・音量
 ・
 */

public class StageMenu : MonoBehaviour {

	public static bool StagePause = false;

	private bool menuOpen = false;

	private MenuState nowState;
	private Canvas menu;

	private Button stage, option, exit, resume;

	void Start () {
		stage = GameObject.Find("Stage").GetComponent<Button>();
		option = GameObject.Find("Option").GetComponent<Button>();
		exit = GameObject.Find("Exit").GetComponent<Button>();
		resume = GameObject.Find("Resume").GetComponent<Button>();

		menu = GameObject.Find ("MenuUI").GetComponent<Canvas>();
		menu.enabled = false;
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.M)){
			if(!menuOpen){
				StagePause = true;
				menu.enabled = true;
				menuOpen = true;

				b_Stage();
			}else{
				menu.enabled = false;
				menuOpen = false;
				StagePause = false;
			}
		}
	}

	#region 時間の表示
	void TimeText(){
		Text time;
		time = GameObject.Find ("menuTime").GetComponent<Text>();
			
		time.text = Math.Floor(Global.g_Time / 3600f).ToString ("00");
		time.text += Math.Floor(Global.g_Time / 60f).ToString ("\\:00");
		time.text += Math.Floor(Global.g_Time % 60f).ToString ("\\:00");
		//time.text += (Global.g_Time % 1 * 100).ToString ("\\:00");
	}
	#endregion

	#region ボタンの処理
	/* ステージ状況 */
	void b_Stage(){
		nowState = MenuState.Stage;
		ButtonInteractable (nowState);
		TimeText ();
	}
	/* ゲーム設定 */
	void b_Option(){
		nowState = MenuState.Option;
		ButtonInteractable (nowState);
	}
	/* TOPへ戻る */
	void b_Exit(){
		nowState = MenuState.Exit;
		ButtonInteractable (nowState);
	}
	/* ゲームへ戻る */
	void b_Resume(){
		menu.enabled = false;
		menuOpen = false;
		StagePause = false;		
	}

	void ButtonInteractable(MenuState state){
		stage.interactable = true;
		option.interactable = true;
		exit.interactable = true;

		switch(state){
		case MenuState.Stage:
			stage.interactable = false;
			break;
		case MenuState.Option:
			option.interactable = false;
			break;
		case MenuState.Exit:
			exit.interactable = false;
			break;
		}
	}
	#endregion
}
