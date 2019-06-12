using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMonitor : MonoBehaviour
{
    public BigMonitor bMonitor;
    public bool isOpen = false;

    private void OnMouseDown()
    {
        if (!isOpen)
        {
            bMonitor.monitorOpening = true;
            isOpen = true;
        }
        else{ return; }
    }
}
