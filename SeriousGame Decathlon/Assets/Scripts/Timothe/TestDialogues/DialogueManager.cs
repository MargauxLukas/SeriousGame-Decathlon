using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //Infos
    public Dialogue actualDialogue;
    private Dialogue previousDialogue;

    //Canvas
    public GameObject dialogueGlobal;
    public Text texteDialogue;
    public Image portraitUn;
    public Image portraitDeux;
    public List<GameObject> bouttonsChoix;
    public List<Text> bouttonTextes;

    //Variables
    private int actualDialogueLine = 0;

    public bool isDialogueOpen;

    private void Update()
    {
        if (isDialogueOpen && actualDialogue != null)
        {
            portraitUn.sprite = actualDialogue.persoParlant.sprite;
            portraitDeux.sprite = actualDialogue.persoEcoutant.sprite;
            if (actualDialogue != previousDialogue || previousDialogue == null)
            {
                previousDialogue = actualDialogue;
                actualDialogueLine = 0;
                foreach (GameObject obj in bouttonsChoix)
                {
                    obj.SetActive(false);
                }
                AffichageDialogue(actualDialogueLine);
            }
            else if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Debug.Log(actualDialogue.phraseDites.Count);
                Debug.Log(actualDialogueLine);

                if(actualDialogueLine < actualDialogue.phraseDites.Count-1)
                {
                    Debug.Log("PréTest");
                    if (touch.phase == TouchPhase.Began)
                    {
                        Debug.Log("Test");
                        actualDialogueLine++;
                        AffichageDialogue(actualDialogueLine);
                    }
                }
                else
                {
                    for (int k = 0; k < actualDialogue.dialoguesSuivant.Count; k++)
                    {
                        if (actualDialogue.dialoguesSuivant[k] == null && k == 2)
                        {
                            isDialogueOpen = false;
                            dialogueGlobal.SetActive(false);
                        }
                    }
                }
            }

            /*for (int i = 0; i < bouttonsChoix.Count - 1; i++)
            {
                bouttonsChoix[i].GetComponent<Button>().onClick.AddListener(() =>
               {
                   DialogueChoice(i);
                   Debug.Log("Test :" + i);
               });
            }*/
        }
    }

    void AffichageDialogue(int nbLigne)
    {
        texteDialogue.text = actualDialogue.phraseDites[nbLigne];
        if(nbLigne >= actualDialogue.phraseDites.Count-1)
        {
            for (int j = 0; j < bouttonsChoix.Count; j++)
            {
                bouttonsChoix[j].SetActive(true);
                if(actualDialogue.choixPossibles[j] != null)
                {
                    bouttonTextes[j].text = actualDialogue.choixPossibles[j];
                }
                else
                {
                    bouttonTextes[j].text = " ";
                }
            }
        }
    }

    public void DialogueChoice(int nbDialogueChoisit)
    {
        if(actualDialogue.dialoguesSuivant[nbDialogueChoisit] != null)
        {
            Dialogue newDialogue = actualDialogue.dialoguesSuivant[nbDialogueChoisit];
            actualDialogue = newDialogue;
        }
        else
        {
            isDialogueOpen = false;
            dialogueGlobal.SetActive(false);
        }
    }

}
