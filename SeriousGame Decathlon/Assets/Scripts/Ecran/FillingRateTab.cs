using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FillingRateTab : MonoBehaviour
{
    public int fillingRate = 0;
    public bool bCanMecaOpen = true;
    [Header("Can Meca Open Toggle")]
    public Toggle toggle;

    private string buttonName;
    [HideInInspector]
    public Button ancientButton;
    public RecountTab tabRecount;

    public void setFillingRate()
    {
        if (TutoManager.instance != null) {TutoManager.instance.Manager(21);}
        if (ancientButton != null)
        {
            ancientButton.interactable = true;
        }
        Debug.Log(EventSystem.current.currentSelectedGameObject != null);
        ancientButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        ancientButton.interactable = false;
        buttonName = EventSystem.current.currentSelectedGameObject.name;
        fillingRate = int.Parse(buttonName);
        if(fillingRate != tabRecount.colis.GetComponent<ColisScript>().colisScriptable.fillPercent)
        {
            Scoring.instance.MidPenalty();
            Scoring.instance.AffichageErreur("Mauvais filling rate");
        }
    }

    public void canMecaOpen()
    {
        if (TutoManager.instance != null) {TutoManager.instance.Manager(22);}
        if (!toggle.isOn)
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
