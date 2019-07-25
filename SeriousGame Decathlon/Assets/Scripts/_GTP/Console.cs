using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Console : MonoBehaviour
{
    public TextMeshProUGUI nbText;
    public Monitor monitor;
    public ConsoleMonitor cm;

    public int nb = 0;

    public void Start()
    {
        nbText.text = "";
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
        monitor.UpdateAffichage(monitor.nbMonitor + nb);
        cm.UpdateAffichageConsole(nb);
        nb = 0;
        UpdateAffichage();
    }
}
