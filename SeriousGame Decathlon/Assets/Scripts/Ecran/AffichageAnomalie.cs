using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AffichageAnomalie : MonoBehaviour
{
    [Header("Texte en enfant")]
    public List<TextMeshProUGUI> text;
    [Header("Toggle en enfant")]
    public List<Toggle> toggleList;
    [Header("Info en enfant")]
    public List<Button> infoList;
    [Header("OngletManager")]
    public OngletManager ongletManager;

    [Header("Liste des dialogues")]
    public List<Dialogue> dialogueList;

    [Header("GameObject")]
    public AnomalieDetection detectAnomalie;
    public IWayInfoManager managerIway;
    private List<Dialogue> actualUsableDialogue;
    public DialogueManager manageDialog;
    public Button CB01;
    public Button CB02;


    [HideInInspector]
    public List<string> listAnomalies;
    [HideInInspector]
    public int n = 0;
    [HideInInspector]
    public int toggleOnNb = 0;

    bool toggleCanActivate = false;


    public void AfficherAnomalie()
    {
        foreach(TextMeshProUGUI textMesh in text)
        {
            textMesh.text = "";
        }
        foreach (Toggle toggle in toggleList)
        {
            toggle.isOn = false;
            toggle.gameObject.SetActive(false);
            toggleCanActivate = false;
        }
        foreach(Button button in infoList)
        {
            button.gameObject.SetActive(false);
        }
        actualUsableDialogue = new List<Dialogue>();

        n = 0;
        if (listAnomalies != null)
        {
            if (TutoManagerMulti.instance == null)
            {
                CB01.interactable = true;
                CB02.interactable = true;
            }   
            ongletManager.DesactivateAll();
            foreach (string anomalie in listAnomalies)
            {
                if (n == 4) { break; }
                else
                {
                    text[n].text = anomalie;
                    toggleList[n].gameObject.SetActive(true);
                    infoList[n].gameObject.SetActive(true);
                    toggleCanActivate = true;

                    switch (anomalie)
                    {
                        case "Quality control":
                            actualUsableDialogue.Add(dialogueList[0]);
                            ongletManager.ActivateOngletFillingRate();
                            break;
                        case "Repacking from FP":
                            actualUsableDialogue.Add(dialogueList[1]);
                            ongletManager.ActivateOngletRepack();
                            ongletManager.ActivateOngletRecount();
                            CB01.interactable = false;
                            CB02.interactable = false;
                            break;
                        case "RFID tags to be applied":
                            actualUsableDialogue.Add(dialogueList[2]);
                            ongletManager.ActivateOngletRecount();
                            break;
                        case "RFID tag over Tolerance":
                            actualUsableDialogue.Add(dialogueList[3]);
                            ongletManager.ActivateOngletRecount();
                            break;
                        case "RFID tag under Tolerance":
                            actualUsableDialogue.Add(dialogueList[4]);
                            ongletManager.ActivateOngletRecount();
                            break;
                        case "RFID tag for unexpected product":
                            actualUsableDialogue.Add(dialogueList[5]);
                            ongletManager.ActivateOngletRecount();
                            break;
                        case "TU too heavy (20-25)":
                            actualUsableDialogue.Add(dialogueList[6]);
                            ongletManager.ActivateOngletRepack();
                            
                            break;
                        case "RFID tag scanned for New product":
                            actualUsableDialogue.Add(dialogueList[7]);
                            ongletManager.ActivateOngletFillingRate();
                            break;
                        case "Dimensions out of tolerance":
                            actualUsableDialogue.Add(dialogueList[8]);
                            ongletManager.ActivateOngletRepack();
                            CB01.interactable = false;
                            CB02.interactable = false;
                            break;
                        case "Dimensions out of dimension for tray":
                            actualUsableDialogue.Add(dialogueList[9]);
                            ongletManager.ActivateOngletRepack();
                            CB01.interactable = false;
                            CB02.interactable = false;
                            break;
                    }
                    n++;
                }
            }
            ongletManager.Priority();
        }
    }

    public void ValidateAnomalie(int nbBouton)
    {
        toggleOnNb = 0;
        if (toggleCanActivate)
        {
            if (TutoManagerMulti.instance != null) { TutoManagerMulti.instance.Manager(19); }
            if (listAnomalies[nbBouton] == "RFID tag scanned for New product" && ongletManager.fillingRate.GetComponent<FillingRateTab>().fillingRate != 0)

            if (listAnomalies[nbBouton] == "RFID tag scanned for New product")
            {
                detectAnomalie.RFIDtagKnowned.Add(managerIway.refIntIWay);
            }
            foreach (Toggle toggle in toggleList)
            {
                if (toggle.isOn)
                {
                    toggleOnNb++;
                }
            }

            if (toggleOnNb == n)
            {
                ongletManager.CanReturnToMeca();
            }
            else
            {
                ongletManager.CantReturnToMeca();
            }

            Scoring.instance.solveAnomalieWithoutMalus();
        }
    }

    public void ShowHelp(int nbHelp)
    {
        manageDialog.LoadDialogue(actualUsableDialogue[nbHelp]);
    }
}
