using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OngletManager : MonoBehaviour
{
    [Header("GameObject lié à l'écran")]
    public GameObject    homeScreen;
    public GameObject listAnomalies;
    public GameObject  ongletButton;
    public GameObject   fillingRate;
    public GameObject       recount;
    public GameObject        repack;
    public GameObject      createHU;

    public GameObjectsManagerMulti gameObjectsManager;

    private bool needFillingRate = false;
    private bool needRecount = false;
    private bool needRepack = false;

    public Button onglet1;
    public Button onglet2;
    public Button onglet3;

    [Header("Return to Meca button")]
    public Button returnToMeca;

    [Header("ScreenDisplay sur InfoIWay")]
    public ScreenDisplayIWay screenDisplay;

    [Header("GameObject lié à la gestion d'anomalie")]
    public PistolScan pistolScan;
    public AnomalieDetection anomalieDetect;

    public ColliderRenvoieColis colliderRenvoieColis;

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

        returnToMeca.interactable = false;
    }

    public void Update()
    {
        if(hasBeenScannedByPistol)
        {
            homeScreen   .SetActive(false);
            ongletButton .SetActive(true );
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
        if (TutoManagerMulti.instance != null) {TutoManagerMulti.instance.Manager(20);}
    }

    public void RecountOpen()
    {
        fillingRate.SetActive(false);
        recount    .SetActive(true );
        repack     .SetActive(false);
        if (TutoManagerMulti.instance != null) {TutoManagerMulti.instance.Manager(5);}
    }

    public void RepackOpen()
    {
        fillingRate.SetActive(false);
        recount    .SetActive(false);
        repack     .SetActive(true );
    }

    public void ReturnToMeca()
    {
        if (TutoManagerMulti.instance != null) {TutoManagerMulti.instance.Manager(23);}
        if (allValidate)
        {
            recount.GetComponent<RecountTab>().rfidScan.artFind.ResetAll();

            homeScreen   .SetActive(true );
            ongletButton .SetActive(false);
            fillingRate  .SetActive(false);
            recount      .SetActive(false);
            repack       .SetActive(false);
            listAnomalies.SetActive(false);

            //ACTION
            pistolScan.fiche.ficheIsClosing = true;                                            //Fiche se ferme
            colliderRenvoieColis.canReturn = true;

            //RESET
            listAnomalies.GetComponent<AffichageAnomalie>().listAnomalies.Clear();             //Reset list anomalies
            screenDisplay.EndAffichage();                                                      //Reset Affichage HU
            pistolScan.scriptColis.hasBeenScannedByPistol = false;                             //Reset scan pistolet
            repack.GetComponent<RepackTab>().Reset();

            //CHECKCOLIS
            anomalieDetect.isAwakning = true;
            anomalieDetect.CheckColis(pistolScan.scriptColis.colisScriptable);
        }
    }

    public void CreateHU()
    {
        if (TutoManagerMulti.instance != null) {TutoManagerMulti.instance.Manager(34);}
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
        if(TutoManagerMulti.instance != null)
        {
            allValidate = true;
            ColorBlock cb = gameObjectsManager.GameObjectToButton(gameObjectsManager.returnMecaButton).colors;
            cb.disabledColor = new Color32(255, 255, 255, 255);
        }
        else if (TutoManagerMulti.instance == null)
        {
            returnToMeca.interactable = true;
            allValidate = true;
        }
    }

    public void CantReturnToMeca()
    {
        returnToMeca.interactable = false;
        allValidate = false;
    }

    public void DesactivateAll()
    {
        onglet1.interactable = false;
        onglet2.interactable = false;
        onglet3.interactable = false;
        needFillingRate = false;
        needRecount = false;
        needRepack = false;
    }
    
    public void ActivateOngletFillingRate()
    {
        onglet1.interactable = true;
        needFillingRate = true;
        fillingRate.SetActive(true);
        if(fillingRate.GetComponent<FillingRateTab>() != null && fillingRate.GetComponent<FillingRateTab>().ancientButton != null)
        {
            fillingRate.GetComponent<FillingRateTab>().ancientButton.interactable = true;
        }
    }

    public void ActivateOngletRecount()
    {
        onglet2.interactable = true;
        needRecount = true;
        
    }

    public void ActivateOngletRepack()
    {
        onglet3.interactable = true;
        needRepack = true;
        
    }

    public void Priority()
    {
        if(needFillingRate)
        {
            fillingRate.SetActive(true);
        }
        else
        {
            if(needRecount)
            {
                recount.SetActive(true);
            }
            else
            {
                repack.SetActive(true);
            }
        }
    }
}
