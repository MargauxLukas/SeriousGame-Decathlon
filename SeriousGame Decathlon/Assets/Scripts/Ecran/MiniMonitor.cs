using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMonitor : MonoBehaviour
{
    public BigMonitor bMonitor;

    private void OnMouseDown()
    {
        bMonitor.monitorOpening = true;
    }
}
