using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenDisplayIWay : MonoBehaviour
{
    public IWayInfoManager iWayInfoManager;
    public Text textArt;
    public Text textPCB;

    public void UpdateAffichage()
    {
        textArt.text = "" + iWayInfoManager.refIntIWay;
        textPCB.text = "" + iWayInfoManager.pcbIntIWay;
    }

    public void EndAffichage()
    {
        textArt.text = "";
        textPCB.text = "";
    }
}
