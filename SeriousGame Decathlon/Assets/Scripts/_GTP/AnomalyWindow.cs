using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyWindow : MonoBehaviour
{
    public GameObject ecranCorrectQuantity;

    public ManagerColisVider mcv;
    
    public void CorrectQuantityInSourceTU()
    {
        ecranCorrectQuantity.SetActive(true );
        gameObject          .SetActive(false);
    }

    public void RFIDScanningError()
    {
        gameObject.SetActive(false);
    }

    public void WrongProduct()
    {
        if (mcv.emplacementsScripts[mcv.emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.listArticles[0] == mcv.emplacementsScripts[mcv.emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.gtpSupposedToBe)
        {
            Scoring.instance.LosePointGTP(100, "Tu as envoyé un colis sans problème en Multifonction");
        }
        mcv.FairePartirUnColis();
        gameObject.SetActive(false);
    }

    public void CuttingDepth()
    {
        gameObject.SetActive(false);
    }

    public void Back()
    {
        gameObject .SetActive(false);
    }
}
