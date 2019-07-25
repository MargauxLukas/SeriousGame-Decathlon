﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcranBaseWindow : MonoBehaviour
{
    public GameObject anomalyWindow;
    public GameObject PickTUContentWindow;

    public void AnomalyWindow()
    {
        gameObject   .SetActive(false);
        anomalyWindow.SetActive(true );
    }

    public void PickTUWindow()
    {
        gameObject         .SetActive(false);
        PickTUContentWindow.SetActive(true );
    }
}
