using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpdateChatText : MonoBehaviour {

    public Text[] OutputText;
    public InputField input;

    private string[] ChatText;
    private bool wasFocused;

    // Use this for initialization
    void Start() {
        ChatText = new string[9] { "\n", "\n", "\n", "\n", "\n", "\n", "\n", "\n", "\n" };
    }

    // Update is called once per frame
    void Update() {
        
        if (input.isFocused)
        {
            printChatText();
        }
        wasFocused = input.isFocused;
    }


    public void UpdateText() {
        updateChatText(input.text + "\n");
        input.text = "";
        input.ActivateInputField();
    }

    void updateChatText(string newText)
    {
        if (newText != "")
        {
            for (int i = 0; i <= ChatText.Length - 2; i++)
            {
                ChatText[i] = ChatText[i + 1];
            }
            ChatText[ChatText.Length - 1] = newText;
        }
    }

	 void printChatText () {
        for (int i = 0; i <= ChatText.Length-1; i++)
            OutputText[i].text = ChatText[i];
	}

    void OnMouseDown()
    {
        
    }
}
