using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Console : MonoBehaviour
{
    [Header("Monitor et ConsoleMonitor lié")]
    public Monitor monitor;
    public ConsoleMonitor cm;

    [Header("Texte Nb")]
    public TextMeshProUGUI nbText;

    public GameObject    moins;
    public GameObject     plus;
    public GameObject validate;

    public int emplacementConsole;
    public int nb = 0;

    private bool canActivate = false;

    public void Start()
    {
        nbText.text = "0";
    }

    public void UpdateAffichage() //Toujours 0 au commencement
    {
        nbText.text = nb.ToString();
    }

    public void Plus()
    {
        if (canActivate)
        {
            nb++;
            UpdateAffichage();
        }
    }

    public void Moins()
    {
        if (canActivate)
        {
            nb--;
            UpdateAffichage();
        }
    }

    public void Valider() 
    {
        if (canActivate)
        {
            monitor.UpdateAffichage(monitor.nbMonitor + nb);
            cm.UpdateAffichageConsole(nb, emplacementConsole);
            nb = 0;
            UpdateAffichage();
        }
    }

    public void IsActivate(bool activate)
    {
        if(activate)
        {
            canActivate = true;
        }
        else
        {
            canActivate = false;
        }
    }
}
