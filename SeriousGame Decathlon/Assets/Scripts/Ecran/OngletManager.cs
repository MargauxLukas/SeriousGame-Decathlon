using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OngletManager : MonoBehaviour
{
    [Header("GameObject")]
    public GameObject    homeScreen;
    public GameObject listAnomalies;
    public GameObject  ongletButton;
    public GameObject   fillingRate;
    public GameObject       recount;
    public GameObject        repack;
    public GameObject      createHU;

    [Header("Return to Meca button")]
    public Button returnToMeca;

    [Header("ScreenDisplay sur InfoIWay")]
    public ScreenDisplayIWay screenDisplay;

    public PistolScan pistolScan;

    [HideInInspector]
    public bool hasBeenScannedByPistol = false;
    [HideInInspector]
    public bool allValidate = false;

    public void Start()
    {
        homeScreen   .SetActive(true );
        ongletButton .SetActive(false);
        fillingRate  .SetActive(false);
        recount      .SetActive(false);
        repack       .SetActive(false);
        listAnomalies.SetActive(false);
        createHU     .SetActive(false);

        returnToMeca.GetComponent<Image>().color = Color.red;
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
            createHU     .SetActive(false);

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

    public void ReturnToMeca()
    {
        if (allValidate)
        {
            homeScreen   .SetActive(true );
            ongletButton .SetActive(false);
            fillingRate  .SetActive(false);
            recount      .SetActive(false);
            repack       .SetActive(false);
            listAnomalies.SetActive(false);

            //ACTION
            pistolScan.fiche.ficheIsClosing = true;                                            //Fiche se ferme

            //RESET
            listAnomalies.GetComponent<AffichageAnomalie>().listAnomalies.Clear();             //Reset list anomalies
            screenDisplay.EndAffichage();                                                      //Reset Affichage HU
            pistolScan.scriptColis.hasBeenScannedByPistol = false;                             //Reset scan pistolet

            //CheckColis
        }
    }

    public void CreateHU()
    {
        homeScreen   .SetActive(false);
        ongletButton .SetActive(false);
        fillingRate  .SetActive(false);
        recount      .SetActive(false);
        repack       .SetActive(false);
        listAnomalies.SetActive(false);
        createHU     .SetActive(true );
    }

    public void CreateHUOK(int pcb, int reference)
    {
        recount.GetComponent<RecountTab>().PrintHU(pcb, reference, 0);
        homeScreen   .SetActive(true);
        ongletButton .SetActive(false);
        fillingRate  .SetActive(false);
        recount      .SetActive(false);
        repack       .SetActive(false);
        listAnomalies.SetActive(false);
        createHU     .SetActive(false);
    }

    public void CreateHUCancel()
    {
        homeScreen   .SetActive(true);
        ongletButton .SetActive(false);
        fillingRate  .SetActive(false);
        recount      .SetActive(false);
        repack       .SetActive(false);
        listAnomalies.SetActive(false);
        createHU     .SetActive(false);
    }

    public void CanReturnToMeca()
    {
        returnToMeca.GetComponent<Image>().color = Color.green;
        allValidate = true;
    }

    public void CantReturnToMeca()
    {
        returnToMeca.GetComponent<Image>().color = Color.red;
        allValidate = false;
    }
}
