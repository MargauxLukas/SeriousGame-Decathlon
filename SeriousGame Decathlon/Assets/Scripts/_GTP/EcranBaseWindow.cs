using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcranBaseWindow : MonoBehaviour
{
    public GameObject anomalyWindow;

    public void AnomalyWindow()
    {
        gameObject   .SetActive(false);
        anomalyWindow.SetActive(true);
    }
}
