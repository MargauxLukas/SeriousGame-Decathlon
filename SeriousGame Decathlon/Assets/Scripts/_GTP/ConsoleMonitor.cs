using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConsoleMonitor : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int nbMonitor = 7;

    public void Start()
    {
        text.text = "7";   
    }

    public void UpdateAffichage(int nb) 
    {
        text.text = (nbMonitor + nb).ToString();
    }

    public void Envoyer()
    {
        //OnClick colis part
    }
}
