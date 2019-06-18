using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OngletManager : MonoBehaviour
{
    public GameObject    homeScreen;
    public GameObject listAnomalies;
    public GameObject  ongletButton;
    public GameObject   fillingRate;
    public GameObject       recount;
    public GameObject        repack;

    public bool hasBeenScannedByPistol = false;

    public void Start()
    {
        homeScreen   .SetActive(true );
        ongletButton .SetActive(false);
        fillingRate  .SetActive(false);
        recount      .SetActive(false);
        repack       .SetActive(false);
        listAnomalies.SetActive(false);
    }

    public void Update()
    {
        if(hasBeenScannedByPistol)
        {
            homeScreen   .SetActive(false);
            ongletButton .SetActive(true );
            fillingRate  .SetActive(true );
            recount      .SetActive(false);
            repack       .SetActive(false);
            listAnomalies.SetActive(true );

            listAnomalies.GetComponent<AffichageAnomalie>().AfficherAnomalie();

            hasBeenScannedByPistol = false;
        }
    }

    public void FillingRateOpen()
    {
        fillingRate.SetActive(true );
        recount    .SetActive(false);
        repack     .SetActive(false);
    }

    public void RecountOpen()
    {
        fillingRate.SetActive(false);
        recount    .SetActive(true );
        repack     .SetActive(false);
    }

    public void RepackOpen()
    {
        fillingRate.SetActive(false);
        recount    .SetActive(false);
        repack     .SetActive(true );
    }
}
