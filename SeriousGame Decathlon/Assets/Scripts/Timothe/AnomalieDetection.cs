using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalieDetection : MonoBehaviour
{
    public List<Colis> listColis;
    public List<int> RFIDtagKnowned;

    private void Start()
    {

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            CheckList();
            Debug.Log("Appuie sur A");
        }
    }

    void CheckList()
    {
        foreach (Colis colis in listColis)
        {
            colis.nbAnomalie = 0;
            colis.listAnomalies = new List<string>();

            int RFIDnb = 0;
            foreach (Article article in colis.listArticles)
            {
                if (article.rfid.estFonctionnel && article.rfid != null)
                {
                    RFIDnb++;
                }
            }

            if (RFIDnb != colis.wayTicket.PCB)
            {
                colis.nbAnomalie++;
                colis.listAnomalies.Add("RFID tag over Tolerance");
            }

            bool isBreakable = false;
            foreach (Article article in colis.listArticles)
            {
                if (article.rfid.refArticle.numeroRef != colis.wayTicket.refArticle.numeroRef && !isBreakable)
                {
                    colis.nbAnomalie++;
                    colis.listAnomalies.Add("RFID tag for unexpected product");
                    isBreakable = true;
                }
            }

            if (colis.poids > 20)
            {
                colis.nbAnomalie++;
                colis.listAnomalies.Add("TU too heavy (20-25)");
            }

            bool isCompatible = false;
            for (int i = 0; i < RFIDtagKnowned.Count; i++)
            {
                if (colis.listArticles[0].rfid.refArticle.numeroRef == RFIDtagKnowned[i])
                {
                    isCompatible = true;
                }
            }
            if (!isCompatible)
            {
                colis.nbAnomalie++;
                colis.listAnomalies.Add("RFID tag scanned for unknown product");
            }

            if (colis.estAbime)
            {
                colis.nbAnomalie++;
                colis.listAnomalies.Add("Quality Control");
            }

            if (colis.isBadOriented)
            {
                colis.nbAnomalie++;
                colis.listAnomalies.Add("Wrong carton orientation");
            }
        }
    }
}
