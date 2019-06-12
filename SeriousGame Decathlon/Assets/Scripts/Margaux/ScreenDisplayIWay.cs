using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenDisplayIWay : MonoBehaviour
{
    public IWayInfoManager iWayInfoManager;
    public Text textInfoIWay;

    void Start()
    {
        
    }

    void Update()
    {
        textInfoIWay.text = "Reference : ART# " + iWayInfoManager.refStringIWay + "\nPCB : " + iWayInfoManager.pcbStringIWay;
    }
}
