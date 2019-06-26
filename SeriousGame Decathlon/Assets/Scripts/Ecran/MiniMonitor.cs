using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMonitor : MonoBehaviour
{
    public BigMonitor bMonitor;
    public bool monitorClosing = false;

    private void OnMouseDown()
    {
        if (!monitorClosing)
        {
            bMonitor.monitorOpening = true;
        }
    }
}
