using UnityEngine;
using System.Collections;

public class WiiRemote : MonoBehaviour {

	private string FilePass = ".\\Assets\\Resources\\WiiRemote\\Wiiremote.exe";

	void Start () {
		// カレントディレクトリを取得する
		string stCurrentDir = System.IO.Directory.GetCurrentDirectory();
		// カレントディレクトリを表示する
		Debug.Log(stCurrentDir);
		System.Diagnostics.Process p = System.Diagnostics.Process.Start(FilePass);
	}

	void Update () {
	
	}
}