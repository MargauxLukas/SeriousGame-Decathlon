using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenDisplayIWay : MonoBehaviour
{
    public IWayInfoManager iWayInfoManager;
    public Text textInfoIWay;

    public void UpdateAffichage()
    {
        textInfoIWay.text = "Reference : ART# " + iWayInfoManager.refIntIWay + "\nPCB : " + iWayInfoManager.pcbIntIWay;
    }

    public void EndAffichage()
    {
        textInfoIWay.text = "Reference : ART# " +  "\nPCB : " ;
    }
}
