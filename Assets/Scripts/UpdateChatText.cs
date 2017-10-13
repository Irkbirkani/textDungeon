using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpdateChatText : MonoBehaviour {

    public Text OutputText;
    public InputField input;
    public ScrollRect scroll;

    private string ChatText;

    // Update is called once per frame
    void Update() {
        OutputText.text = ChatText;
    }


    public void UpdateText() {
        if (input.text.Length > 0)
        {
            ChatText = ChatText + "   " + input.text + "\n";
            input.text = "";
            input.ActivateInputField();
            scroll.verticalNormalizedPosition = 0f;
        }
    }
}
