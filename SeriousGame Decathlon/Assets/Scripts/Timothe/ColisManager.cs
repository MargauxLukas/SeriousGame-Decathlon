using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ColisManager : MonoBehaviour
{
    public List<Colis> listeColisTraiter;
    public GameObject colisGameObject;

    public Transform positionApparition;

    public GameObject[] listeColisActuel;

    public List<Article> articleOnTable;

    //A affecté au Colis
    public GameObject menuTourner;
    public GameObject spriteArticleTableUn;
    public GameObject spriteArticleTableDeux;
    public Text textArticleTableNombre;
    public Text textArtcileTableRFID;
    public Image circleImage;

    //Auquel le colis doit être affecté
    public List<BoutonDirection> listeBoutonsMenuTourner;
    public RecountTab recountTab;
    public RepackTab repackTab;
    public PistolScan scanPistol;
    public RotationScript scriptRotation;

    public AnomalieDetection anomDetect;

    private void Awake()
    {
        if(ChargementListeColis.instance != null)
        {
            Debug.Log("Test Instance");
            listeColisTraiter = ChargementListeColis.instance.colisProcessMulti;
        }
        anomDetect.CheckList(listeColisTraiter);
    }


    public void CheckNewColis()
    {
        anomDetect.CheckList(listeColisTraiter);
    }

    public void AppelColis()
    {
        listeColisActuel = new GameObject[0];
        listeColisActuel = GameObject.FindGameObjectsWithTag("Colis");

        if (listeColisActuel.Length <= 0 && listeColisTraiter.Count > 0)
        {
            GameObject colisTemporaire = Instantiate(colisGameObject, positionApparition.position, Quaternion.identity);
            ColisScript scriptColis = colisTemporaire.GetComponent<ColisScript>();

            scriptColis.colisScriptable = Colis.CreateInstance<Colis>();
            scriptColis.colisScriptable = Instantiate(listeColisTraiter[0]);
            scriptColis.doesEntrance = true;
            scriptColis.tournerMenu = menuTourner;
            scriptColis.spriteArticleTableUn = spriteArticleTableUn;
            scriptColis.spriteArticleTableDeux = spriteArticleTableDeux;
            scriptColis.circleImage = circleImage;
            //scriptColis.textArtcileTableRFID = textArtcileTableRFID;
            //scriptColis.textArticleTableNombre = textArticleTableNombre;

            foreach (BoutonDirection bouton in listeBoutonsMenuTourner)
            {
                bouton.scriptColis = colisTemporaire.GetComponent<ColisScript>();
            }

            //scanPistol.scriptColis   = scriptColis;
            recountTab.colis         = colisTemporaire;
            repackTab .colis         = colisTemporaire;
            scriptRotation.cartonObj = colisTemporaire;
            scriptRotation.ColisEnter();
            Debug.Log("Test Colis Manager");
            scriptRotation.UpdateSprite(scriptColis.colisScriptable.carton.spriteCartonsListe, colisTemporaire.GetComponent<SpriteRenderer>());
            if(scriptColis.colisScriptable.estOuvert)
            {
                Color newColo = colisTemporaire.GetComponent<SpriteRenderer>().color;
                newColo.a = 0.3f;
                colisTemporaire.GetComponent<SpriteRenderer>().color = newColo;
            }

            //Debug.Log(listeColisTraiter[0]);
            listeColisTraiter.RemoveAt(0);
            if (listeColisTraiter.Count > 0 && listeColisTraiter[0] == null && listeColisTraiter[1] != null)
            {
                for (int i = 0; i < listeColisTraiter.Count - 1; i++)
                {
                    if (listeColisTraiter[i + 1] != null)
                    {
                        listeColisTraiter[i] = listeColisTraiter[i + 1];
                    }
                    else
                    {
                        listeColisTraiter.RemoveAt(i);
                    }
                }
            }

            /*if(scriptColis.colisScriptable.fillPercent<=50)
            {
                scriptColis.spriteArticleDansColis.sprite = scriptColis.colisScriptable.listArticles[0].spriteList[0];
            }
            else if(scriptColis.colisScriptable.fillPercent >= 125)
            {
                scriptColis.spriteArticleDansColis.sprite = scriptColis.colisScriptable.listArticles[0].spriteList[1];
            }
            else
            {
                scriptColis.spriteArticleDansColis.sprite = scriptColis.colisScriptable.listArticles[0].spriteList[2];
            }

            scriptColis.spriteMaskArticleColis.sprite = scriptColis.colisScriptable.carton.cartonOuvert;*/
        }
        else if(listeColisActuel.Length > 0)
        {
            Scoring.instance.MinorPenalty();
            Scoring.instance.AffichageErreur("Tu a déjà un colis a traiter");
        }

        spriteArticleTableUn.GetComponent<PileArticle>().UpdatePileArticle();
        spriteArticleTableDeux.GetComponent<PileArticle>().UpdatePileArticle();
    }

    public void RenvoieColis(GameObject colisRenvoye)
    {
        colisRenvoye.GetComponent<ColisScript>().colisScriptable.needQualityControl = false;
        anomDetect.CheckColis(colisRenvoye.GetComponent<ColisScript>().colisScriptable);
        if (!listeColisTraiter.Contains(colisRenvoye.GetComponent<ColisScript>().colisScriptable))
        {
            //anomDetect.CheckColis(colisRenvoye.GetComponent<ColisScript>().colisScriptable);
            if (colisRenvoye.GetComponent<ColisScript>().colisScriptable.nbAnomalie > 0)
            {
                listeColisTraiter.Add(colisRenvoye.GetComponent<ColisScript>().colisScriptable);
                Scoring.instance.sendColis();
                Scoring.instance.ResetComboColisSansMalus();
                Scoring.instance.AffichageErreur("Renvoie de colis avec anomalies");
                Scoring.instance.WhatTheFuck();
                
                for (int m = 0; m < colisRenvoye.GetComponent<ColisScript>().colisScriptable.nbAnomalie; m++)
                {
                    Scoring.instance.MajorPenalty();
                }
            }
            else
            {
                Scoring.instance.sendColisWithoutMalus();
            }

            if(colisRenvoye.GetComponent<ColisScript>().colisScriptable.isBadOriented)
            {
                Scoring.instance.MajorPenalty();
            }
            GameObject.Destroy(colisRenvoye);
        }
        spriteArticleTableUn.GetComponent<PileArticle>().UpdatePileArticle();
        spriteArticleTableDeux.GetComponent<PileArticle>().UpdatePileArticle();
    }
}
