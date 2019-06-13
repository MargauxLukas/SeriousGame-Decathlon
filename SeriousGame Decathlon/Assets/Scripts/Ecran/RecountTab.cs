using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RecountTab : MonoBehaviour
{
    public TextMeshProUGUI rfidNumText;
    public RFIDScan        rfidScan; //Activation Scan
    public RFIDInfoManager infoRFID; //Pour récupérer le nombre de puce RFID

    public WayTicket wayTicketScriptable;

    private bool getRFIDNum = false;

    public void Update()
    {
        if(getRFIDNum) //Faut l'arrêter à un moment mais quand ? Comment marche le vrai Scan RFID ? Il s'arrête à un moment ?
        {
            rfidNumText.text = infoRFID.numStringRFID;
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
        rfidScan.isActive = false;
        getRFIDNum = false;

        WayTicket newTicket = Instantiate(wayTicketScriptable);
        wayTicketScriptable = newTicket;
    }

    public void PrintRFID() //Button OnClick
    {
        //Acces méthode imprimante pour imprimer
    }
}
