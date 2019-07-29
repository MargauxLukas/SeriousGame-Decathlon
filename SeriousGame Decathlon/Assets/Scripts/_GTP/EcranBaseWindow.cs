using System.Collections;
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

    public void PickTUWindow(int nb)
    {
        if (PickTUContentWindow.GetComponent<PickTUContentWindow>().mca.colisViderManage.colisActuellementsPose[nb] == null)
        {
            return;
        }
        else
        {
            gameObject.SetActive(false);
            PickTUContentWindow.SetActive(true);
            PickTUContentWindow.GetComponent<PickTUContentWindow>().affichageListe(nb);
        }
    }
}
