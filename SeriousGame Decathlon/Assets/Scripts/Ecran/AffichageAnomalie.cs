using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AffichageAnomalie : MonoBehaviour
{
    [Header("Texte en enfant")]
    public List<TextMeshProUGUI> text;
    [Header("Toggle en enfant")]
    public List<Toggle> toggleList;
    [Header("OngletManager")]
    public OngletManager ongletManager;

    [HideInInspector]
    public List<string> listAnomalies;
    [HideInInspector]
    public int n = 0;
    [HideInInspector]
    public int toggleOnNb = 0;


    public void AfficherAnomalie()
    {
        foreach(TextMeshProUGUI textMesh in text)
        {
            textMesh.text = "";
        }
        foreach (Toggle toggle in toggleList)
        {
            toggle.gameObject.SetActive(false);
        }


            n = 0;
        foreach(string anomalie in listAnomalies)
        {
            if (n == 4){break;}
            else
            {
                text[n].text = anomalie;
                toggleList[n].gameObject.SetActive(true);
                n++;
            }
        }
    }

    public void ValidateAnomalie()
    {
        toggleOnNb = 0;
        foreach(Toggle toggle in toggleList)
        {
            if(toggle.isOn)
            {
                toggleOnNb++;
            }
        }

        if(toggleOnNb == n)
        {
            ongletManager.CanReturnToMeca();
        }
        else
        {
            ongletManager.CantReturnToMeca();
        }
    }
}
