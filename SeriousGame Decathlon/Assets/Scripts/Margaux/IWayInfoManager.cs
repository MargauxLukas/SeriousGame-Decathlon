using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWayInfoManager : MonoBehaviour
{
    public int refIntIWay;
    public string refStringIWay;
    public int pcbIntIWay;
    public string pcbStringIWay;
    
    void Start()
    {
        
    }

    void Update()
    {
        refStringIWay = refIntIWay.ToString();
        pcbStringIWay = pcbIntIWay.ToString();
    }
}
