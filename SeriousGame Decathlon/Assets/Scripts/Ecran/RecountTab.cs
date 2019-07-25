using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RecountTab : MonoBehaviour
{
    [Header("GameObject dans la scène")]
    public GameObject            colis;
    public RFIDScan           rfidScan;              //Activation Scan lorsque que j'appuie sur Recount
    public RFIDInfoManager    infoRFID;              //Pour récupérer le nombre de puce RFID
    public AnomalieDetection anomalieD;              //Pour savoir si la ref est connu

    [Header("Prefab")]
    public GameObject ticket    ;
    public GameObject ticketRFID;

    [Header("Liste des REFArticle connu")]
    public List<RefArticle> listRefArticles = new List<RefArticle>();

    [HideInInspector]
    public int refRFID1;
    [HideInInspector]
    public int refRFID2;

    WayTicket newTicket;

    GameObject ticketgo;
    GameObject ticketRFIDgo;

    public bool isInventory = false ;
    /*****************************
    *  Active le scanner RFID    *
    ******************************/
    public void Recount()
    {
        rfidScan.isActive = true;
        colis.GetComponent<ColisScript>().colisScriptable.hasBeenRecount = true;
        if(!colis.GetComponent<ColisScript>().hasBeenScannedByPistol)
        {
            Scoring.instance.MinorPenalty();
            Scoring.instance.AffichageErreur("Colis non scanné");
        }
        if (TutoManagerMulti.instance != null) { TutoManagerMulti.instance.Manager(6); }
    }

    /******************************************************************
    * Enregistre les nouvelles informations sur le ticket à imprimer  *
    *******************************************************************/
    public void Inventory()
    {
        if(TutoManagerMulti.instance != null) {TutoManagerMulti.instance.Manager(16);}
        if (colis != null && colis.GetComponent<ColisScript>().hasBeenScannedByRFID 
                          && (int.Parse(infoRFID.numStringRFID) > 0) 
                          && (int.Parse(infoRFID.numStringRFID) < colis.GetComponent<ColisScript>().colisScriptable.wayTicket.PCB))
        {
            rfidScan.isActive = false;

            //A mettre dans FillingRate
            //if  (anomalieD.RFIDtagKnowned.Contains(int.Parse(infoRFID.refStringRFID))){}                                         //On connait déjà la référence
            //else{anomalieD.RFIDtagKnowned.Add     (int.Parse(infoRFID.refStringRFID)) ;}                                         //On rajoute la référence dans notre base de donnée

            WayTicket newTicket       = WayTicket.CreateInstance<WayTicket>();
            newTicket.PCB             = int.Parse(infoRFID.numStringRFID);                                                         //On donne au nouveau ticket le bon nombre de RFID
            newTicket.refArticle      = rfidScan.infoRFID.rfidComplet.refArticle;                                                  //On donne au nouveau ticket la bonne référence
            newTicket.poids           = colis.GetComponent<ColisScript>().colisScriptable.wayTicket.poids          ;
            newTicket.numeroCodeBarre = colis.GetComponent<ColisScript>().colisScriptable.wayTicket.numeroCodeBarre;

            ticket.    GetComponent<GetIWayFromObject>().IWayTicket = newTicket;                                                   //IWayTicket
        }
        else
        {
            WayTicket newTicket = WayTicket.CreateInstance<WayTicket>();
            newTicket.PCB       = int.Parse(infoRFID.numStringRFID);                                                               //On donne au nouveau ticket le bon nombre de RFID
            if (rfidScan.infoRFID.rfidComplet != null) {newTicket.refArticle = rfidScan.infoRFID.rfidComplet.refArticle;}          //On donne au nouveau ticket la bonne référence
            newTicket.poids           = 0;
            newTicket.numeroCodeBarre = 0;

            ticket.    GetComponent<GetIWayFromObject>().IWayTicket = newTicket;
        }

        isInventory = true;
    }

    /*********************************************
    * Permet d'imprimer un ticket pour le colis  *
    **********************************************/
    public void PrintHU() 
    {
        if (TutoManagerMulti.instance != null) {TutoManagerMulti.instance.Manager(17);}
        if (isInventory)
        {
            Destroy(ticketgo);
            ticketgo = Instantiate(ticket, new Vector2(73.34f, 1.74f), Quaternion.identity);
        }
        else
        {
            Destroy(ticketgo);
            ticket.GetComponent<GetIWayFromObject>().IWayTicket = colis.GetComponent<ColisScript>().colisScriptable.wayTicket;
            ticketgo = Instantiate(ticket, new Vector2(73.34f, 1.74f), Quaternion.identity);
        }
    }

    public void PrintHU(int pcb, int refArticle, float poids = 0)
    {
        bool refAlreadyExist = false;
        RefArticle refArt = null;

        foreach (RefArticle refArticleTemporaire in listRefArticles)                   //Vérification si la RefArticle Existe Déjà
        {
            if(refArticleTemporaire.numeroRef == refArticle)
            {
                refArt = refArticleTemporaire;
                refAlreadyExist = true;
            }
        }

        if (!refAlreadyExist)
        {
            refArt = RefArticle.CreateInstance<RefArticle>();
            refArt.numeroRef = refArticle;
        }

        WayTicket newTicket                 = WayTicket.CreateInstance<WayTicket>();
                  newTicket.PCB             = pcb;
                  newTicket.refArticle      = refArt;
                  newTicket.poids           = refArt.poids*pcb;
                  newTicket.numeroCodeBarre = 0;
        
        ticket.GetComponent<GetIWayFromObject>().IWayTicket = newTicket;

        Destroy(ticketgo);
        ticketgo = Instantiate(ticket, new Vector2(73.34f, 1.74f), Quaternion.identity);
    }

    /************************************
    * Permet d'imprimer des puces RFID  *
    *************************************/
    public void PrintRFID1()
    {
        if (TutoManagerMulti.instance != null) {TutoManagerMulti.instance.Manager(30);}
        bool refAlreadyExist = false;
        RefArticle refArt = null;

        if (refRFID1 != 0)
        {
            foreach (RefArticle refArticleTemporaire in listRefArticles)                   //Vérification si la RefArticle Existe Déjà
            {
                if (refArticleTemporaire.numeroRef == refRFID1)
                {
                    refArt = refArticleTemporaire;
                    refAlreadyExist = true;
                    Debug.Log("Je connais");
                }
            }

            if (!refAlreadyExist)
            {
                refArt = RefArticle.CreateInstance<RefArticle>();
                refArt.numeroRef = refRFID1;
                Debug.Log("Je connais pas");
            }
        }
        else
        {
            refArt = RefArticle.CreateInstance<RefArticle>();
            refArt.numeroRef = colis.GetComponent<ColisScript>().colisScriptable.wayTicket.refArticle.numeroRef;
        }

        RFID newRFID                = RFID.CreateInstance<RFID>();
             newRFID.refArticle     = refArt;
             newRFID.estFonctionnel = true;

        ticketRFID.GetComponent<GetRfidFromObject>().newRFID = newRFID;

        Destroy(ticketRFIDgo);
        ticketRFIDgo = Instantiate(ticketRFID, new Vector2(69.91f, 1.81f), Quaternion.identity);
    }

    public void PrintRFID2()
    {
        bool refAlreadyExist = false;
        RefArticle refArt = null;

        foreach (RefArticle refArticleTemporaire in listRefArticles)                   //Vérification si la RefArticle Existe Déjà
        {
            if (refArticleTemporaire.numeroRef == refRFID2)
            {
                refArt = refArticleTemporaire;
                refAlreadyExist = true;
                Debug.Log("Je connais");
            }
        }

        if (!refAlreadyExist)
        {
            refArt = RefArticle.CreateInstance<RefArticle>();
            refArt.numeroRef = refRFID2;
            Debug.Log("Je connais pas");
        }

        RFID newRFID                = RFID.CreateInstance<RFID>();
             newRFID.refArticle     = refArt;
             newRFID.estFonctionnel = true;

        ticketRFID.GetComponent<GetRfidFromObject>().newRFID = newRFID;

        Destroy(ticketRFIDgo);
        ticketRFIDgo = Instantiate(ticketRFID, new Vector2(69.91f, 1.81f), Quaternion.identity);
    }
}
