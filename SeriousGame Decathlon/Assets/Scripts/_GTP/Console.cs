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

    public int emplacementConsole;
    public int nb = 0;

    private bool canActivate = false;

    public void Start()
    {
        nbText.text = "0";
    }

    /*****************************************
     *   Update le chiffre sur la console    *
     *****************************************/
    public void UpdateAffichage()
    {
        nbText.text = nb.ToString();

        if(TutoManagerGTP.instance != null && nbText.text == TutoManagerGTP.instance.consoleInputValue)
        {
            TutoManagerGTP.instance.Manager(22);
        }
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

        if (TutoManagerGTP.instance != null) { TutoManagerGTP.instance.Manager(21); }
    }

    public void Valider() 
    {
        if (canActivate)
        {
            monitor.UpdateAffichage(monitor.nbMonitor + nb);
            cm.UpdateAffichageConsole(nb, emplacementConsole);
            nb = 0;
            nbText.text = nb.ToString();

            if(TutoManagerGTP.instance != null) { TutoManagerGTP.instance.Manager(23); }
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
