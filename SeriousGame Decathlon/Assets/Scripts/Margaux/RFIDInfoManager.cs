using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFIDInfoManager : MonoBehaviour
{
    //Référence RFID
    public RFID rfidComplet;
    public int refIntRFID;
    public string refStringRFID;
    //Total RFID
    public int numIntRFID;
    public string numStringRFID;

    void Start()
    {

    }

    void Update()
    {
        refStringRFID = refIntRFID.ToString();
        numStringRFID = numIntRFID.ToString();
    }
}
