using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalieDetection : MonoBehaviour
{
    public List<int> RFIDtagKnowned;

    public void CheckColis(Colis colis)
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

        /* Résolution des problèmes de RFID
         * - Scan colis. Scan RFID. Bouton Inventaire. Crée nouveau ticket IWAY dans système. PrintHU pour imprimer. Mettre le ticket sur le colis.
         * - Scan colis. Scan RFID. Vider colis. Voir nombre Article. Bouton Inventaire. Crée un nouveau RFID. Mettre RFID sur pile Article.
         */

        if (RFIDnb != colis.wayTicket.PCB)
        {
            colis.nbAnomalie++;
            colis.listAnomalies.Add("RFID tag over Tolerance");
        }

        bool isBreakable = false;
        foreach (Article article in colis.listArticles) //Scanner le colis. Scanner les RFID. Vider le colis. Imprimer le RFID. Mettre le nouveau RFID.
        {
            if (article.rfid.refArticle.numeroRef != colis.wayTicket.refArticle.numeroRef && !isBreakable)
            {
                colis.nbAnomalie++;
                colis.listAnomalies.Add("RFID tag for unexpected product");
                isBreakable = true;
            }
        }

        if (colis.poids > 20) //A voir comment rectifier en jeu ET à dupliquer pour le supp 35
        {
            colis.nbAnomalie++;
            colis.listAnomalies.Add("TU too heavy (20-25)");
        }

        bool isCompatible = false; //Scanner le colis, Scanner les RFID, faire un inventaire pour ajouter les nouveaux RFID.
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

        if (colis.estAbime) //A voir comment rectifier en jeu
        {
            colis.nbAnomalie++;
            colis.listAnomalies.Add("Quality Control");
        }

        if (colis.isBadOriented) //A voir comment rectifier en jeu (Voir avec les graph)
        {
            colis.nbAnomalie++;
            colis.listAnomalies.Add("Wrong carton orientation");
        }


        //A la fin
        if(colis.nbAnomalie <= 0)
        {
            Debug.Log("Tout est bon dans le colis " + colis.name);
        }
    }

    public void CheckList(List<Colis> listColis)
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
