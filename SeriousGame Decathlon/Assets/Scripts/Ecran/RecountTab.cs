using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RecountTab : MonoBehaviour
{
    public GameObject player;
    public int score;

    public TextMeshProUGUI rfidNumText;
    private string refRFID;
    public RFIDScan        rfidScan; //Activation Scan
    public RFIDInfoManager infoRFID; //Pour récupérer le nombre de puce RFID
    public AnomalieDetection anomalieD;

    public GameObject ticket;

    public GameObject colis;

    public WayTicket wayTicketScriptable;

    private bool getRFIDNum = false;

    private void Start()
    {
        score = player.GetComponent<PlayerTest>().player.score;
    }

    public void Update()
    {
        if(getRFIDNum) //Faut l'arrêter à un moment mais quand ? Comment marche le vrai Scan RFID ? Il s'arrête à un moment ?
        {
            rfidNumText.text = infoRFID.numStringRFID;
            refRFID = infoRFID.refStringRFID;
            //rfidNumText.text = "Reference : ART# " + infoRFID.refStringRFID + "\nTotal Articles : " + infoRFID.numStringRFID;
        }
    }

    public void Recount() //Button OnClick 
    {
        rfidScan.isActive = true;
        getRFIDNum        = true;
    }

    public void Inventory() //Button OnClick
    {
        //Je considère que si on appuie sur Inventory, c'est qu'on veut plus scan car on a le nombre qui nous interesse. Donc je desactive le SCAN RFID.
        if (colis.GetComponent<ColisScript>().hasBeenScannedByRFID && (int.Parse(infoRFID.numStringRFID) > 0))
        {
            rfidScan.isActive = false;
            getRFIDNum = false;

            if (anomalieD.RFIDtagKnowned.Contains(int.Parse(infoRFID.refStringRFID)))
            {
                //Connais déjà
            }
            else
            {
                Debug.Log("ADD");
                anomalieD.RFIDtagKnowned.Add(int.Parse(infoRFID.refStringRFID));
            }

            WayTicket newTicket = Instantiate(wayTicketScriptable);
            newTicket.PCB = int.Parse(infoRFID.numStringRFID);
            newTicket.refArticle = rfidScan.infoRFID.rfidComplet.refArticle;
            Debug.Log(newTicket.PCB);
            wayTicketScriptable = newTicket;
            ticket.GetComponent<GetIWayFromObject>().IWayTicket = wayTicketScriptable;

        }
        else
        {
            score = score - 5;
            player.GetComponent<PlayerTest>().player.score = score;
            return;
        }
    }

    public void PrintHU() //Button OnClick
    {
        Instantiate(ticket, new Vector2(2.89f, 1.64f), Quaternion.identity);
    }

    public void PrintRFID() //Button OnClick
    {
        //Instantiate(ticket, new Vector2(2.89f, 1.64f), Quaternion.identity);
    }
}
