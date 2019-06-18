﻿using System.Collections;
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

    private string refRFID;

    WayTicket newTicket;

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
    }

    /******************************************************************
    * Enregistre les nouvelles informations sur le ticket à imprimer  *
    *******************************************************************/
    public void Inventory()
    {
        if (colis != null && colis.GetComponent<ColisScript>().hasBeenScannedByRFID && (int.Parse(infoRFID.numStringRFID) > 0))
        {
            rfidScan.isActive = false;

            if  (anomalieD.RFIDtagKnowned.Contains(int.Parse(refRFID))){}                                           //On connait déjà la référence
            else{anomalieD.RFIDtagKnowned.Add     (int.Parse(refRFID)) ;}                                           //On rajoute la référence dans notre base de donnée

            WayTicket newTicket       = WayTicket.CreateInstance<WayTicket>();
            newTicket.PCB             = int.Parse(infoRFID.numStringRFID);                                          //On donne au nouveau ticket le bon nombre de RFID
            newTicket.refArticle      = rfidScan.infoRFID.rfidComplet.refArticle;                                   //On donne au nouveau ticket la bonne référence
            newTicket.poids           = colis.GetComponent<ColisScript>().colisScriptable.wayTicket.poids          ;
            newTicket.numeroCodeBarre = colis.GetComponent<ColisScript>().colisScriptable.wayTicket.numeroCodeBarre;

            RFID newRFID           = RFID.CreateInstance <RFID>();
            newRFID.refArticle     = rfidScan.infoRFID.rfidComplet.refArticle;
            newRFID.estFonctionnel = true;

            ticket.    GetComponent<GetIWayFromObject>().IWayTicket = newTicket;                                    //IWayTicket
            ticketRFID.GetComponent<GetRfidFromObject>().newRFID    = newRFID;                                      //RFIDTicket
        }
        else
        {
            newTicket = WayTicket.CreateInstance<WayTicket>();
            newTicket.PCB = int.Parse(infoRFID.numStringRFID);                                          //On donne au nouveau ticket le bon nombre de RFID
            if(rfidScan.infoRFID.rfidComplet != null)
                newTicket.refArticle = rfidScan.infoRFID.rfidComplet.refArticle;                                   //On donne au nouveau ticket la bonne référence
            newTicket.poids = 0;
            newTicket.numeroCodeBarre = 0;
            ticket.GetComponent<GetIWayFromObject>().IWayTicket = newTicket;

            RFID newRFID = RFID.CreateInstance<RFID>();
            if (rfidScan.infoRFID.rfidComplet != null)
            {
                newRFID.refArticle = rfidScan.infoRFID.rfidComplet.refArticle;
            }
            else if (colis.GetComponent<ColisScript>().colisScriptable.wayTicket != null)
            {
                newRFID.refArticle = colis.GetComponent<ColisScript>().colisScriptable.wayTicket.refArticle;
            }
            else
            {
                newRFID.refArticle = RefArticle.CreateInstance<RefArticle>();
            }
            newRFID.estFonctionnel = true;
            ticketRFID.GetComponent<GetRfidFromObject>().newRFID = newRFID;

            //Scoring.instance.MinorPenalty();                                                                   //Test de scoring
            return;
        }
    }

    /*********************************************
    * Permet d'imprimer un ticket pour le colis  *
    **********************************************/
    public void PrintHU() 
    {
        GameObject leTicket =  Instantiate(ticket, new Vector2(2.89f, 1.64f), Quaternion.identity);
        leTicket.GetComponent<GetIWayFromObject>().IWayTicket = newTicket;
    }

    /************************************
    * Permet d'imprimer des puces RFID  *
    *************************************/
    public void PrintRFID()
    {
        Instantiate(ticketRFID, new Vector2(-3.06f, -1.01f), Quaternion.identity);
    }
}
