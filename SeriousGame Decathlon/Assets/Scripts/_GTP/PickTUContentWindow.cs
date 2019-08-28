using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickTUContentWindow : MonoBehaviour
{
    [Header("ManagerColisAttendu /CartonVideLink")]
    public ManagerColisAttendu mca;
    public CartonVideLink      cvl;

    [Header("ecranCorrectPickedQty")]
    public GameObject ecranCorrectPickedQty;

    [Header("Article dans ListArticle en Enfant")]
    public List<ArticleAffichage> listArticleAffichage;

    private int emplacement;

    public Button anomalyButton;

    /********************************************************************************
    *      **Affichage de la Liste d'article à mettre dans le colis à Remplir**    *
    *                                                                              *
    *      (On y accède en appuyant sur les loupes sur l'écran, il faut qu'il      *
    *          y est un colis à l'emplacement pour pouvoir voir la liste)          *
    ********************************************************************************/
    public void affichageListe(int nb)
    {
        emplacement = nb;
        string ArticleName1 = "";                                                             // Je Reset à chaque fois pour éviter tous problèmes.
        string ArticleName2 = "";
        string ArticleName3 = "";
        int nb1 = 0;
        int nb2 = 0;
        int nb3 = 0;

        if (mca.colisActuellementTraite[nb] != null)
        {
            foreach (Article art in mca.colisActuellementTraite[nb].listArticles)                 // Je regarde à quoi le colis doit ressembler
            {
                if (art.name == ArticleName1 || ArticleName1 == "")                               // Je regarde si le colis est pareil ou si le nom est vide(Dans ce cas, je lui met le nom de l'article)
                {
                    ArticleName1 = art.name;
                    nb1++;
                }
                else if (art.name == ArticleName2 || ArticleName2 == "")
                {
                    ArticleName2 = art.name;
                    nb2++;
                }
                else if (art.name == ArticleName3 || ArticleName3 == "")
                {
                    ArticleName3 = art.name;
                    nb3++;
                }
            }
        }

        listArticleAffichage[0].TName.text = ArticleName1;                                     // Affichage de la liste (Nom + Nombre voulu)                            
        listArticleAffichage[1].TName.text = ArticleName2;
        listArticleAffichage[2].TName.text = ArticleName3;

        listArticleAffichage[0].TTarget.text = nb1.ToString();
        listArticleAffichage[1].TTarget.text = nb2.ToString();
        listArticleAffichage[2].TTarget.text = nb3.ToString();

        nb1 = 0;                                                                               // Je réutilise une variable qui me sert plus (#Recyclage)
        nb2 = 0;
        nb3 = 0;

        foreach (Article art in mca.colisViderManager.colisActuellementsPose[nb].GetComponent<RemplissageColisGTP>().colisScriptable.listArticles)   //Je regarde ce qu'il y'a dans le colis
        {
            if (art.name == ArticleName1)                                                     // Pour chaque article, j'incrémente
            {
                nb1++;
            }
            else if (art.name == ArticleName2)
            {
                nb2++;
            }
            else if (art.name == ArticleName3)
            {
                nb3++;
            }
        }

        listArticleAffichage[0].TActual.text = nb1.ToString();                               //Affichage de la liste (Nombre Actuel) 
        listArticleAffichage[1].TActual.text = nb2.ToString();
        listArticleAffichage[2].TActual.text = nb3.ToString();
    }

    public void ClosePickTU()
    {
        mca.ClosePickTU(mca.nbEmplacement, cvl.csTab[mca.nbEmplacement].colisScriptable, cvl.csTab[mca.nbEmplacement]);

        if(TutoManagerGTP.instance != null) { TutoManagerGTP.instance.Manager(24); }
    }

    /****************************************************************************************
     *   Permet de changer le nombre de l'article actuel qu'on met dans le colis à remplir  *
     ****************************************************************************************/
    public void CorrectPickedQty()
    {
        //Soucis si le carton est vide, il faudrait prendre l'article en cours comme reference
        //ecranCorrectPickedQty.GetComponent<CorrectPickedQtyWindow>().AffichageStart(mca.colisViderManage.emplacementsScripts[mca.colisViderManage.emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.listArticles[0].name, mca.nbArticleVoulu);    //Je lui donne le nom de l'article et le nombre prévu
        ecranCorrectPickedQty.GetComponent<CorrectPickedQtyWindow>().AffichageStart(mca.colisViderManager.emplacementsScripts[mca.colisViderManager.emplacement].GetComponent<AffichagePileArticleGTP>().artReference.name, mca.nbArticleVoulu);
        ecranCorrectPickedQty.SetActive(true);

        if(TutoManagerGTP.instance != null) { TutoManagerGTP.instance.Manager(17); }
    }

    /************************************
    *   Update l'affichage de la liste  *
    *************************************/
    public void UpdatingArticle(int nbUpdate)
    {
        mca.cm[emplacement].UpdateAffichageConsole(nbUpdate, emplacement);

        int nbReference = 0;

        if (nbUpdate != 0)
        {
            if (nbReference < nbUpdate)
            {
                while (nbUpdate != 0)
                {
                    mca.colisActuellementTraite[emplacement].listArticles.Add(mca.colisActuellementTraite[emplacement].listArticles[0]);
                    nbUpdate--;
                }
            }
            else if (nbReference > nbUpdate)
            {
                while (nbUpdate != 0)
                {
                    nbUpdate++;
                }
            }
        }

        affichageListe(emplacement);
    }

    public void Back()
    {
        gameObject.SetActive(false);
        anomalyButton.interactable = true;

        if (TutoManagerGTP.instance != null) { TutoManagerGTP.instance.Manager(3); }
    }
}
