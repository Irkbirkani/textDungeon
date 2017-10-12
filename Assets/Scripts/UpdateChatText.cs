using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpdateChatText : MonoBehaviour {

	public Text OutputText;
	public InputField input;

	private string[] ChatText;

	// Use this for initialization
	void Start () {
		ChatText = new string[7] { "\n", "\n", "\n", "\n", "\n", "\n", "\n", };
	}
	
	// Update is called once per frame
	void Update () {
		OutputText.text = printChatText();
	}


	public void UpdateText(){

		updateChatText(input.text+ "\n");
		input.text = "";
		input.ActivateInputField ();
	}

	void updateChatText(string newText){
		for (int i = 0; i <= ChatText.Length - 2; i++) {
			ChatText [i] = ChatText [i + 1];
		}
	
		ChatText [ChatText.Length-1] = newText;
}

	 string printChatText () {
		string temp = "";
		for (int i = 0; i <= ChatText.Length-1; i++)
		{
			temp = temp + ChatText[i];
		}
		return temp;
	}
}
