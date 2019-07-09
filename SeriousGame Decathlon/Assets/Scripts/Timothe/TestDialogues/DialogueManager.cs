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
    public Image fond;
    public Text texteDialogue;
    public Image portraitUn;
    public List<GameObject> bouttonsChoix;
    public List<Text> bouttonTextes;

    public Sprite sprAmpoule;

    //Variables
    private int actualDialogueLine = 0;

    public bool isDialogueOpen;

    private void Awake()
    {
        if(TutoManager.instance != null)
        {
            fond.enabled = false;
        }
    }
    private void Update()
    {
        if (isDialogueOpen && actualDialogue != null)
        {
            if (actualDialogue.persoParlant != null)
            {
                portraitUn.sprite = actualDialogue.persoParlant.sprite;
            }
            else
            {
                portraitUn.sprite = sprAmpoule;
            }
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
                        else if(actualDialogue.dialoguesSuivant[k] != null)
                        {
                            return;
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
                if(actualDialogue.choixPossibles[j] != null)
                {
                    bouttonTextes[j].text = actualDialogue.choixPossibles[j];
                    bouttonsChoix[j].SetActive(true);
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
        Debug.Log("Test");
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

    public void LoadDialogue(Dialogue dialogueToLoad)
    {
        dialogueGlobal.SetActive(true);
        isDialogueOpen = true;
        actualDialogue = dialogueToLoad;
    }
}
