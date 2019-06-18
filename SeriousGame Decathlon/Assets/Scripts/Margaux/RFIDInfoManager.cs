using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFIDInfoManager : MonoBehaviour
{
    //Référence RFID
    [HideInInspector]
    public RFID   rfidComplet  ;
    [HideInInspector]
    public int    refIntRFID   ;
    [HideInInspector]
    public string refStringRFID;

    //Total RFID
    [HideInInspector]
    public int    numIntRFID   ;
    [HideInInspector]
    public string numStringRFID;

    void Update()
    {
        refStringRFID = refIntRFID.ToString();
        numStringRFID = numIntRFID.ToString();
    }
}
