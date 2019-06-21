using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AffichageAnomalie : MonoBehaviour
{
    public List<TextMeshProUGUI> text;

    [HideInInspector]
    public List<string> listAnomalies;

    public void AfficherAnomalie()
    {
        foreach(TextMeshProUGUI textMesh in text)
        {
            textMesh.text = "";
        }

        int n = 0;
        foreach(string anomalie in listAnomalies)
        {
            if (n == 4){break;}
            else
            {
                text[n].text = anomalie;
                n++;
            }
        }
    }
}
