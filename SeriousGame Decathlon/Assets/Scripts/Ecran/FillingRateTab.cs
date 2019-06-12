using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FillingRateTab : MonoBehaviour
{
    public int fillingRate;
    public bool canMecaOpen;

    private string buttonName;

    public void setFillingRate()
    {
        buttonName = EventSystem.current.currentSelectedGameObject.name;

        fillingRate = int.Parse(buttonName);
        Debug.Log(fillingRate);
    }
}
