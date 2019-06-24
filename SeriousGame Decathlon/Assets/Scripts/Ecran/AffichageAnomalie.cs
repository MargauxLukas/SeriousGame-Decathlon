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

    public AnomalieDetection detectAnomalie;
    public IWayInfoManager managerIway;
    private List<Dialogue> actualUsableDialogue;
    public DialogueManager manageDialog;

    [HideInInspector]
    public List<string> listAnomalies;
    [HideInInspector]
    public int n = 0;
    [HideInInspector]
    public int toggleOnNb = 0;


    public void AfficherAnomalie()
    {
        foreach(TextMeshProUGUI textMesh in text)
        {
            textMesh.text = "";
        }
        foreach (Toggle toggle in toggleList)
        {
            toggle.gameObject.SetActive(false);
        }
        foreach(Button button in infoList)
        {
            button.gameObject.SetActive(false);
        }
        actualUsableDialogue = new List<Dialogue>();


            n = 0;
        foreach(string anomalie in listAnomalies)
        {
            if (n == 4){break;}
            else
            {
                text[n].text = anomalie;
                toggleList[n].gameObject.SetActive(true);
                infoList[n].gameObject.SetActive(true);
                {
                    switch(listAnomalies[n])
                    {
                        case "Quality control":
                            actualUsableDialogue[n] = dialogueList[0];
                            break;
                        case "Repacking from FP":
                            actualUsableDialogue[n] = dialogueList[1];
                            break;
                        case "RFID tags to be applied":
                            actualUsableDialogue[n] = dialogueList[2];
                            break;
                        case "RFID tag over Tolerance":
                            actualUsableDialogue[n] = dialogueList[3];
                            break;
                        case "RFID tag under Tolerance":
                            actualUsableDialogue[n] = dialogueList[4];
                            break;
                        case "RFID tag for unexpected product":
                            actualUsableDialogue[n] = dialogueList[5];
                            break;
                        case "TU too heavy (20-25)":
                            actualUsableDialogue[n] = dialogueList[6];
                            break;
                        case "RFID tag scanned for unknown product":
                            actualUsableDialogue[n] = dialogueList[7];
                            break;
                        case "Dimensions out of tolerance":
                            actualUsableDialogue[n] = dialogueList[8];
                            break;
                        case "Dimensions out of dimmension for tray":
                            actualUsableDialogue[n] = dialogueList[9];
                            break;

                    }
                }
                n++;
            }
        }
    }

    public void ValidateAnomalie(int nbBouton)
    {
        if(listAnomalies[nbBouton] == "RFID tag scanned for unknown product")
        {
            detectAnomalie.RFIDtagKnowned.Add(managerIway.refIntIWay);
        }

        toggleOnNb = 0;
        foreach(Toggle toggle in toggleList)
        {
            if(toggle.isOn)
            {
                toggleOnNb++;
            }
        }

        if(toggleOnNb == n)
        {
            ongletManager.CanReturnToMeca();
        }
        else
        {
            ongletManager.CantReturnToMeca();
        }
    }

    public void ShowHelp(int nbHelp)
    {
        manageDialog.LoadDialogue(actualUsableDialogue[nbHelp]);
    }
}
