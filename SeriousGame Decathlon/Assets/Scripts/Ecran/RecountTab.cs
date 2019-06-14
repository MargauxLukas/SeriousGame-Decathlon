using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RecountTab : MonoBehaviour
{
    [Header("Texte en enfant")]
    public TextMeshProUGUI rfidNumText;

    [Header("GameObject dans la scène")]
    public GameObject colis;
    public RFIDScan           rfidScan;              //Activation Scan lorsque que j'appuie sur Recount
    public RFIDInfoManager    infoRFID;              //Pour récupérer le nombre de puce RFID
    public AnomalieDetection anomalieD;              //Pour savoir si la ref est connu

    [Header("Scriptable")]
    public WayTicket wayTicketScriptable;
    public RFID rfidTicketScriptable;

    [Header("Prefab")]
    public GameObject ticket;
    public GameObject ticketRFID;
    
    private bool getRFIDNum = false;

    private string refRFID;

    public void Update()
    {
        if(getRFIDNum) 
        {
            rfidNumText.text = infoRFID.numStringRFID;
            refRFID          = infoRFID.refStringRFID;
        }
    }


    /*****************************
    *  Active le scanner RFID    *
    ******************************/
    public void Recount()
    {
        rfidScan.isActive = true;
        getRFIDNum        = true;
    }

    /******************************************************************
    * Enregistre les nouvelles informations sur le ticket à imprimer  *
    *******************************************************************/
    public void Inventory()
    {
        if (colis.GetComponent<ColisScript>().hasBeenScannedByRFID && (int.Parse(infoRFID.numStringRFID) > 0))
        {
            rfidScan.isActive = false;
            getRFIDNum        = false;

            if (anomalieD.RFIDtagKnowned.Contains(int.Parse(refRFID)))
            {
                //Connais déjà
            }
            else
            {
                anomalieD.RFIDtagKnowned.Add(int.Parse(refRFID));
            }

            WayTicket newTicket  = Instantiate(wayTicketScriptable);
            newTicket.PCB        = int.Parse(infoRFID.numStringRFID);                                          //On donne au nouveau ticket le bon nombre de RFID
            newTicket.refArticle = rfidScan.infoRFID.rfidComplet.refArticle;                                   //On donne au nouveau ticket la bonne référence
            wayTicketScriptable  = newTicket;
            ticket.GetComponent<GetIWayFromObject>().IWayTicket = wayTicketScriptable;

            RFID newRFID = Instantiate(rfidTicketScriptable);
            newRFID.refArticle = rfidScan.infoRFID.rfidComplet.refArticle;
            newRFID.estFonctionnel = true;
            rfidTicketScriptable = newRFID;
            ticketRFID.GetComponent<GetRfidFromObject>().newRFID = rfidTicketScriptable;
        }
        else
        {
            Scoring.instance.MinorPenalty();                                                                   //Test de scoring
            return;
        }
    }


    /*********************************************
    * Permet d'imprimer un ticket pour le colis  *
    **********************************************/
    public void PrintHU() 
    {
        Instantiate(ticket, new Vector2(2.89f, 1.64f), Quaternion.identity);
    }

    /************************************
    * Permet d'imprimer des puces RFID  *
    *************************************/
    public void PrintRFID()
    {
        Instantiate(ticketRFID, new Vector2(-3.06f, -1.01f), Quaternion.identity);
    }
}
