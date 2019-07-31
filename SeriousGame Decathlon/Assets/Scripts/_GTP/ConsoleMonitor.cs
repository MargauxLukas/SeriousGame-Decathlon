using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConsoleMonitor : MonoBehaviour
{
    public ManagerColisVider mcv;
    public CartonVideLink cvl;
    public ManagerColisAttendu colisAttenduManage;
    public TextMeshProUGUI text;
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
        int nbArticleEnQuestion = 0;
        int nbArticleEnCours = 0;
        Article reference = mcv.emplacementsScripts[mcv.emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.listArticles[0];
        foreach (Article art in colisAttenduManage.colisVoulus[emplacement].listArticles)
        {
            if(art == reference)
            {
                nbArticleEnQuestion++;
            }
        }
        for (int compteur = 0; compteur < colisAttenduManage.colisVoulus[emplacement].listArticles.Count; compteur++)
        {
            if (colisAttenduManage.colisVoulus[emplacement].listArticles[compteur] == reference)
            {
                nbArticleEnCours++;
                if(nbArticleEnCours > nbMonitor)
                {
                    colisAttenduManage.colisVoulus[emplacement].listArticles.RemoveAt(compteur);
                }
            }
        }

        nbArticleEnQuestion = 0;
        nbArticleEnCours    = 0;

        foreach (Article art in colisAttenduManage.colisActuellementTraite[emplacement].listArticles)
        {
            if (art == reference)
            {
                nbArticleEnQuestion++;
            }
        }
        for (int compteur = 0; compteur < colisAttenduManage.colisActuellementTraite[emplacement].listArticles.Count; compteur++)
        {
            if (colisAttenduManage.colisActuellementTraite[emplacement].listArticles[compteur] == reference)
            {
                nbArticleEnCours++;
                if (nbArticleEnCours > nbMonitor)
                {
                    colisAttenduManage.colisVoulus[emplacement].listArticles.RemoveAt(compteur);
                }
            }
        }
        colisAttenduManage.AjoutArticleColisVoulu(emplacement, nbMonitor);
    }

    public void Envoyer(int emplacement) 
    {
        //Si j'appuie 2 fois, ça duplique le colis
        if (mcv.PeutFairePartirColis())
        {
            Scoring.instance.StopComboGTP(15);
            mcv.FairePartirUnColis();
            UpdateAffichage(0);
            if (colisActuelPoste.currentPhase < phaseActuelle - 1)
            {
                colisActuelPoste.currentPhase++;
            }
            else
            {
                if(colisAttenduManage.DetectionColis(colisActuelPoste.colisScriptable, emplacement))
                {
                    Debug.Log("There were no error in this colis");
                }
                switch(emplacement)
                {
                    case 0:
                        cvl.isFree1 = true;
                        cvl.tapis1GameObject.GetComponent<BoxCollider2D>().enabled = true;
                        break;
                    case 1:
                        cvl.isFree2 = true;
                        cvl.tapis2GameObject.GetComponent<BoxCollider2D>().enabled = true;
                        break;
                    case 2:
                        cvl.isFree3 = true;
                        cvl.tapis3GameObject.GetComponent<BoxCollider2D>().enabled = true;
                        break;
                    default:
                        break;
                }
                colisAttenduManage.RenvoieColis(emplacement);
            }
        }
    }
}
