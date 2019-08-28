using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EcranBaseWindow : MonoBehaviour
{
    public GameObject anomalyWindow;
    public GameObject PickTUContentWindow;
    public Button anomalyButton;
    public Button Loupe1;
    public Button Loupe2;
    public Button Loupe3;


    public void AnomalyWindow()
    {
        anomalyWindow.SetActive(true);
        Loupe1.interactable = false;
        Loupe2.interactable = false;
        Loupe3.interactable = false;

        if (TutoManagerGTP.instance != null) { TutoManagerGTP.instance.Manager(10); }
    }

    public void PickTUWindow(int nb)
    {
        if (PickTUContentWindow.GetComponent<PickTUContentWindow>().mca.colisViderManager.colisActuellementsPose[nb] == null)
        {
            return;
        }
        else
        {
            PickTUContentWindow.SetActive(true);
            anomalyButton.interactable = false;
            PickTUContentWindow.GetComponent<PickTUContentWindow>().affichageListe(nb);

            if(TutoManagerGTP.instance != null) { TutoManagerGTP.instance.Manager(2); }
        }
    }
}
