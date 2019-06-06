using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputFieldController : MonoBehaviour
{
    //Keyboard
    private TouchScreenKeyboard keyboard;
    //InputField
    [SerializeField]
    private InputField inputField;
    //Text displaying input
    [SerializeField]
    private Text text;
    //Number of character in a code
    private readonly int codeNum = 6;
    //Number of character entered by player
    private int inputNum;

    //Open a specific type of keyboard on click
    void OnPointerClick(PointerEventData pointerEventData)
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

    public void GetInput(string code)
    {
        //Number of character entered
        inputNum = code.Length;
        //Set text to Uppercase
        inputField.text = inputField.text.ToUpper();

        if (inputNum >= codeNum)
        {
            //Display the text entered under the Input Field
            text.text = "You entered " + code;
            //Reset text in Input Field
            inputField.text = "";
        }
    }
}
