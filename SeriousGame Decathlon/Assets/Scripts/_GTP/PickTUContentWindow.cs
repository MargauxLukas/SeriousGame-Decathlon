using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickTUContentWindow : MonoBehaviour
{
    public ManagerColisAttendu mca;
    public CartonVideLink      cvl;
    public GameObject ecranDeBase;

    //Emplacement
    //Colis
    //NB article voulu (int)
    //Article en question (Article)

    public void ClosePickTU()
    {
        mca.ClosePickTU(mca.nbEmplacement, cvl.csTab[mca.nbEmplacement]);
    }

    public void CorrectPickedQty()
    {
        mca.CorrectPickQuantity(mca.nbEmplacement, cvl.csTab[mca.nbEmplacement], mca.nbArticleVoulu, cvl.csTab[mca.nbEmplacement].listArticles[0]);
    }

    public void Back()
    {
        ecranDeBase.SetActive(true );
        gameObject .SetActive(false);
    }
}
