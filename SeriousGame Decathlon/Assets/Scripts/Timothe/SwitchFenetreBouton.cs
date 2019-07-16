using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchFenetreBouton : MonoBehaviour
{
    public List<GameObject> fenetreListe;
    public GameObject imageADesactiver;

    public void ChangeFenetre(int currentFenetre)
    {
        imageADesactiver.SetActive(false);
        foreach (GameObject gm in fenetreListe)
        {
            gm.SetActive(false);
        }
        fenetreListe[currentFenetre].SetActive(true);
    }
}
