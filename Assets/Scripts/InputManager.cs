using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {
	char[] keys = {'q', 'w', 'e'};
	KeyCode[] keysArray = {KeyCode.LeftControl, KeyCode.R};

	string[] reservedWords = {"if", "else", "do"};
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Multi_Key_Detection(keys))
			Debug.Log ("Q:W:E was pressed");

		if (Input.GetKeyDown(KeyCode.Space))
			Debug.Log ("Q:W:E was pressed");
	}

	bool Multi_Key_Detection(char[] keysArray){
		string s = Input.inputString;
		string k = "";
		foreach (char c in keysArray) {
			k += char.ToString(c);
		}
		return s == k;
	}

	List<string> codeDetection(string text, string[] codeList){
		List<string> listCodes = new List<string> ();
		char[] textAsChar = text.ToCharArray();
		for (int i=0; i<textAsChar.Length; i++){
			for (int j=0; j< codeList.Length; j++){
				if ( text.Length == codeList[j].Length ){/*
					if (textAsChar[i] == codeList[j].ToCharArray[i]){

					}*/
				}
			}
		}

		return listCodes;
	}


	void OnGUI() {
	}
}
