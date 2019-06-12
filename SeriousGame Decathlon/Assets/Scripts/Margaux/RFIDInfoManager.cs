using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFIDInfoManager : MonoBehaviour
{
    public int refIntRFID;
    public string refStringRFID;
    public int numIntRFID;
    public string numStringRFID;

    void Start()
    {

    }

    void Update()
    {
        Debug.Log(refIntRFID);
        refStringRFID = refIntRFID.ToString();
        numStringRFID = numIntRFID.ToString();
    }
}
