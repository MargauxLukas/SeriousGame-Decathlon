using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FillingRateTab : MonoBehaviour
{
    public int fillingRate;
    public bool bCanMecaOpen = true;
    public Toggle toggle;

    private string buttonName;

    public void setFillingRate()
    {
        buttonName = EventSystem.current.currentSelectedGameObject.name;
        fillingRate = int.Parse(buttonName);
    }

    public void canMecaOpen()
    {
        if(!toggle.isOn)
        {
            bCanMecaOpen = false;
            //Debug.Log("PAS OUVRIR");
        }
        else if(toggle.isOn)
        {
            bCanMecaOpen = true;
            //Debug.Log("Ok il peut");
        }
    }
}
