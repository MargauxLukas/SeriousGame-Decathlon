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
        //colisAttenduManage.nbArticleVoulu
    }

    public void UpdateAffichageConsole(int nb, int emplacement)
    {
        text.text = (nbMonitor + nb).ToString();
        nbMonitor =  nbMonitor + nb;

        for (int compteur = 0; compteur < colisAttenduManage.colisVoulus[emplacement].listArticles.Count; compteur++)
        {
            if (compteur > colisAttenduManage.colisVoulus[emplacement].listArticles.Count-nb)
            {
                colisAttenduManage.colisVoulus[emplacement].listArticles.RemoveAt(compteur);
            }
        }
        for(int compteur = 0; compteur < colisAttenduManage.colisActuellementTraite[emplacement].listArticles.Count; compteur++)
        {
            if (compteur > colisAttenduManage.colisActuellementTraite[emplacement].listArticles.Count - nb)
            {
                colisAttenduManage.colisVoulus[emplacement].listArticles.RemoveAt(compteur);
            }
        }
        colisAttenduManage.AjoutArticleColisVoulu(emplacement, nbMonitor);
    }

    public void Envoyer(int emplacement) 
    {
        if (mcv.PeutFairePartirColis())
        {
            mcv.FairePartirUnColis();
            UpdateAffichage(0);
            Debug.Log("La phase actuelle est : " + colisActuelPoste.currentPhase);
            Debug.Log("Et il me faut la phase : " + phaseActuelle);
            if (colisActuelPoste.currentPhase < phaseActuelle)
            {
                colisActuelPoste.currentPhase++;
            }
            else
            {
                //Renvoyer le colis qui vient d'être géré
                bool noAnomalie = colisAttenduManage.DetectionAllColis(colisActuelPoste.colisScriptable, emplacement);

                if (noAnomalie)
                {
                    colisAttenduManage.RenvoieColis(emplacement);
                }

            }
        }
    }
}
