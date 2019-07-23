using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConsoleMonitor : MonoBehaviour
{
    public ManagerColisVider mcv;
    public TextMeshProUGUI text;
    public int nbMonitor = 7;
    public ManagerColisAttendu colisAttenduManage;
    public RemplissageColisGTP colisActuelPoste;

    public void Start()
    {
        text.text = "";   
    }

    public void UpdateAffichage(int nb) 
    {
        text.text = (nbMonitor + nb).ToString();
    }

    public void Envoyer(int emplacement)
    {
        //mcv.FairePartirUnColis(emplacement);
        colisAttenduManage.DetectionColis(colisActuelPoste.colisScriptable, emplacement);
    }
}
