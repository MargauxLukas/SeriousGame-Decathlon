using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConsoleMonitor : MonoBehaviour
{
    public ManagerColisVider mcv;
    public TextMeshProUGUI text;
    public int nbMonitor = 0;

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
        nbMonitor = nbMonitor + nb;
    }

    public void Envoyer(int emplacement)
    {
        //mcv.FairePartirUnColis(emplacement);
    }
}
