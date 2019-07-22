using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Console : MonoBehaviour
{
    public TextMeshProUGUI nbText;
    public Monitor monitor;
    public ConsoleMonitor cm;

    public int nb;

    public void Start()
    {
        nb = 0;
        nbText.text = nb.ToString();
    }

    public void UpdateAffichage()
    {
        nbText.text = nb.ToString();
    }

    public void Plus()
    {
        //A voir si y'a une condition
        nb++;
        UpdateAffichage();
    }

    public void Moins()
    {
        //A voir si y'a une condition
        nb--;
        UpdateAffichage();
    }

    public void Valider()
    {
        //nb sur ecran poste se met à jour
        cm.UpdateAffichage(nb);
        nb = 0;
    }
}
