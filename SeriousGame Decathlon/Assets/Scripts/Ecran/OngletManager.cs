using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OngletManager : MonoBehaviour
{
    public GameObject fillingRate;
    public GameObject recount;
    public GameObject repack;
    public GameObject ongletButton;
    public GameObject homeScreen;

    public GameObject listAnomalies;

    public bool hasBeenScannedByPistol = false;

    public void Start()
    {
        homeScreen.SetActive(true);
        fillingRate.SetActive(false);
        recount.SetActive(false);
        repack.SetActive(false);
        ongletButton.SetActive(false);
        listAnomalies.SetActive(false);
    }

    public void Update()
    {
        if(hasBeenScannedByPistol)
        {
            fillingRate.SetActive(true);
            recount.SetActive(false);
            repack.SetActive(false);
            ongletButton.SetActive(true);
            homeScreen.SetActive(false);
            listAnomalies.SetActive(true);
            listAnomalies.GetComponent<AffichageAnomalie>().AfficherAnomalie();

            hasBeenScannedByPistol = false;
        }
    }

    public void FillingRateOpen()
    {
        //Debug.Log("FillingRate Open");
        fillingRate.SetActive(true);
        recount.SetActive(false);
        repack.SetActive(false);
    }

    public void RecountOpen()
    {
        //Debug.Log("Recount Open");
        fillingRate.SetActive(false);
        recount.SetActive(true);
        repack.SetActive(false);
    }

    public void RepackOpen()
    {
        //Debug.Log("Repack Open");
        fillingRate.SetActive(false);
        recount.SetActive(false);
        repack.SetActive(true);
    }
}
