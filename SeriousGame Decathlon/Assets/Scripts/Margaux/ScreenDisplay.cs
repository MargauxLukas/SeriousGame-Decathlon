using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenDisplay : MonoBehaviour
{
    public RFIDInfoManager infoRFID;
    public Text textInfoRFID;

    void Start()
    {

    }

    void Update()
    {
        textInfoRFID.text = "Reference : ART# " + infoRFID.refStringRFID + " /n Total Articles : " + infoRFID.numStringRFID;
    }
}
