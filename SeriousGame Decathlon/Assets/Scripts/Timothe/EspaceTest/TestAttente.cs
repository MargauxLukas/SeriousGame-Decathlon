using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TestAttente : MonoBehaviour
{
    public Text nbText;
    public void ChooseNumber(int nb)
    {
        int newNb = -1;
        newNb = TestAttentreInstance.instance.ReturnIntSecond(nb);
        nbText.text = newNb.ToString();
    }
}
