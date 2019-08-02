using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConsoleMonitor : MonoBehaviour
{
    [Header("Script a assigner")]
    public ManagerColisVider mcv;
    public CartonVideLink cvl;
    public ManagerColisAttendu colisAttenduManage;
    public RemplissageColisGTP colisActuelPoste;

    [Header("Texte en enfant")]
    public TextMeshProUGUI text;

    [Header("Phase du colis")]
    public int phaseActuelle;

    private int nbMonitor = 0;

    private bool canRemove;

    public DetectionColisBombe neuviemePos;

    public void Start() 
    {
        text.text = "";   
    }

    /*****************************************
    *   Update le chiffre d'écran console    *
    *****************************************/
    public void UpdateAffichage(int nb ) 
    {
        nbMonitor = nb;
        text.text = nb.ToString();
    }

    /*************************************************************
    *   Update le chiffre d'écran console apres un changement    *
    **************************************************************/
    public void UpdateAffichageConsole(int nb, int emplacement)
    {
        text.text = (nbMonitor + nb).ToString();
        nbMonitor =  nbMonitor + nb;

        if(nb > 0){canRemove = false;}
        else      {canRemove = true ;}

        int nbArticleEnQuestion = 0;
        int nbArticleEnCours    = 0;

        Article reference = mcv.emplacementsScripts[mcv.emplacement].GetComponent<AffichagePileArticleGTP>().artReference;          //L'article dont on est entrain de changer le nombre

        if (canRemove)
        {
            foreach (Article art in colisAttenduManage.colisVoulus[emplacement].listArticles)
            {
                if (art == reference)
                {
                    nbArticleEnQuestion++;
                }
            }
            for (int compteur = 0; compteur < colisAttenduManage.colisVoulus[emplacement].listArticles.Count; compteur++)
            {
                if (colisAttenduManage.colisVoulus[emplacement].listArticles[compteur] == reference)
                {
                    nbArticleEnCours++;
                    if (nbArticleEnCours > nbMonitor)
                    {
                        colisAttenduManage.colisVoulus[emplacement].listArticles.RemoveAt(compteur);                                                    //Si il dépasse le compteur, c'est que y'en a trop du coup on delete
                    }
                }
            }

            nbArticleEnQuestion = 0;
            nbArticleEnCours = 0;

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
                        colisAttenduManage.colisActuellementTraite[emplacement].listArticles.RemoveAt(compteur);
                    }
                }
            }
        }
        else
        {
            for (int compteur = 0; compteur < nb; compteur++)
            {
                colisAttenduManage.colisVoulus[emplacement].listArticles.Add(reference);
            }
        }
        colisAttenduManage.AjoutArticleColisVoulu(emplacement, nbMonitor);
    }

    /******************************************
     *      Bouton envoyer (Bouton noir)      *
     ******************************************/
    public void Envoyer(int emplacement) 
    {
        if (mcv.PeutFairePartirColis() && !neuviemePos.haveAlreadySomething)
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
