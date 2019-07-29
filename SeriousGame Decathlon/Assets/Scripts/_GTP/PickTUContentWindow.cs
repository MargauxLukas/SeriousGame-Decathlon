using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickTUContentWindow : MonoBehaviour
{
    public ManagerColisAttendu mca;
    public CartonVideLink      cvl;
    public GameObject  ecranDeBase;

    public List<ArticleAffichage> listArticleAffichage;

    //Emplacement
    //Colis
    //NB article voulu (int)
    //Article en question (Article)

    public void affichageListe(int nb)
    {
        string ArticleName1 = "";
        string ArticleName2 = "";
        string ArticleName3 = "";
        int nb1 = 0;
        int nb2 = 0;
        int nb3 = 0;

        foreach (Article art in mca.colisActuellementTraite[nb].listArticles)
        {
            if (art.name == ArticleName1 || ArticleName1 == "")
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

        listArticleAffichage[0].TName.text = ArticleName1;
        listArticleAffichage[1].TName.text = ArticleName2;
        listArticleAffichage[2].TName.text = ArticleName3;

        listArticleAffichage[0].TTarget.text = nb1.ToString();
        listArticleAffichage[1].TTarget.text = nb2.ToString();
        listArticleAffichage[2].TTarget.text = nb3.ToString();

        nb1 = 0;
        nb2 = 0;
        nb3 = 0;

        foreach (Article art in mca.colisViderManage.colisActuellementsPose[nb].GetComponent<RemplissageColisGTP>().colisScriptable.listArticles)
        {
            if (art.name == ArticleName1)
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

        listArticleAffichage[0].TActual.text = nb1.ToString();
        listArticleAffichage[1].TActual.text = nb2.ToString();
        listArticleAffichage[2].TActual.text = nb3.ToString();
    }

    public void ClosePickTU()
    {
        mca.ClosePickTU(mca.nbEmplacement, cvl.csTab[mca.nbEmplacement].colisScriptable, cvl.csTab[mca.nbEmplacement]);

    }

    public void CorrectPickedQty()
    {
        mca.CorrectPickQuantity(mca.nbEmplacement, cvl.csTab[mca.nbEmplacement].colisScriptable, mca.nbArticleVoulu, cvl.csTab[mca.nbEmplacement].colisScriptable.listArticles[0]);
    }

    public void Back()
    {
        ecranDeBase.SetActive(true );
        gameObject .SetActive(false);
    }
}
