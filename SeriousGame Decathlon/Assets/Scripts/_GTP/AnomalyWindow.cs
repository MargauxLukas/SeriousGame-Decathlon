using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyWindow : MonoBehaviour
{
    [Header("EcranCorrectQuantity")]
    public GameObject ecranCorrectQuantity;

    [Header("ManagerColisVider")]
    public ManagerColisVider mcv;

    /***********************************************
     *   Permet d'ouvrir l'écran EcranCorrectQty   *
     ***********************************************/
    public void CorrectQuantityInSourceTU()
    {
        ecranCorrectQuantity.SetActive(true );
        gameObject          .SetActive(false);

        if(TutoManagerGTP.instance != null) { TutoManagerGTP.instance.Manager(11); }
    }

    /****************************************************
    *  Pour dire si il y'a un probleme avec le scanner  *
    *****************************************************/
    public void RFIDScanningError()
    {
        gameObject.SetActive(false);
    }

    /****************************************************************************************************************
    *  Permet de dire que le colis contient le mauvais article (On compare la photo et ce qu'il y'a dans le colis)  *
    *****************************************************************************************************************/
    public void WrongProduct()
    {
        if (mcv.PeutFairePartirColis())
        {
            if (mcv.emplacementsScripts[mcv.emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.gtpSupposedToBe == null || mcv.emplacementsScripts[mcv.emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.listArticles[0] == mcv.emplacementsScripts[mcv.emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.gtpSupposedToBe)
            {
                Debug.Log("Erreur Wrong product");
                Scoring.instance.LosePointGTP(100, "Renvoi d'un colis sans problème en Multifonction");
            }
            mcv.FairePartirUnColis();
            gameObject.SetActive(false);
        }
        else
        {
            Scoring.instance.LosePointGTP(0, "Renvoi de colis impossible pour l'instant");
        }

        if(TutoManagerGTP.instance != null) { TutoManagerGTP.instance.Manager(15); }
    }

    /*************
     *  Useless  *
     *************/
    public void CuttingDepth()
    {
        gameObject.SetActive(false);
    }

    public void Back()
    {
        gameObject.SetActive(false);
    }
}
