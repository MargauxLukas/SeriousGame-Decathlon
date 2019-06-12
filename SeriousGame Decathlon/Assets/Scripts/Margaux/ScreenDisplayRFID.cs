using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenDisplayRFID : MonoBehaviour
{
    public RFIDInfoManager infoRFID;
    public Text textInfoRFID;

    void Start()
    {

    }

    void Update()
    {
        textInfoRFID.text = "Reference : ART# " + infoRFID.refStringRFID + "\nTotal Articles : " + infoRFID.numStringRFID;
    }
}
