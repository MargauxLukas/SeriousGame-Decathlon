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

    public void UpdateAffichage() //Toujours 0 au commencement
    {
        nbText.text = nb.ToString();
    }

    public void Plus()
    {
        //A voir si y'a une condition empêchant d'aller trop bas
        nb++;
        UpdateAffichage();
    }

    public void Moins()
    {
        //A voir si y'a une condition empêchant d'aller trop bas
        nb--;
        UpdateAffichage();
    }

    public void Valider() 
    {
        //monitor.UpdateAffichage();
        cm.UpdateAffichage(nb);
        nb = 0;
    }
}
