using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConsoleMonitor : MonoBehaviour
{
    public ManagerColisVider mcv;
    public TextMeshProUGUI text;
    public ManagerColisAttendu colisAttenduManage;
    public RemplissageColisGTP colisActuelPoste;
    public int nbMonitor = 0;

    public int phaseActuelle;

    public void Start()
    {
        text.text = "";   
    }

    public void UpdateAffichage(int nb ) 
    {
        nbMonitor = nb;
        text.text = nb.ToString();
    }

    public void UpdateAffichageConsole(int nb)
    {
        text.text = (nbMonitor + nb).ToString();
        nbMonitor =  nbMonitor + nb;
    }

    public void Envoyer(int emplacement) 
    {
        if (mcv.PeutFairePartirColis())
        {
            if (colisActuelPoste.currentPhase < phaseActuelle)
            {
                colisActuelPoste.currentPhase++;
            }
            else
            {
                //Renvoyer le colis qui vient d'être géré
                bool noAnomalie = colisAttenduManage.DetectionAllColis(colisActuelPoste.colisScriptable, emplacement);
                mcv.FairePartirUnColis();
                if (noAnomalie)
                {
                    colisAttenduManage.RenvoieColis(emplacement);
                }
            }
        }
    }
}
