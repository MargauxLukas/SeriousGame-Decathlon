using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AffichageAnomalie : MonoBehaviour
{
    public List<TextMeshProUGUI> text;
    public List<string> listAnomalies;

    public void AfficherAnomalie()
    {
        int n = 1;
        foreach(string anomalie in listAnomalies)
        {
            text[n].text = anomalie;
            n++;
        }
    }
}
