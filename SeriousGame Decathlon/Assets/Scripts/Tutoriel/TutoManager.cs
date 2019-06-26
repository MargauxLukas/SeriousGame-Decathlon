using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
    public static TutoManager instance;
    public DialogueManager dialogueManager;
    public int phaseNum = 0;
    public int dialogNum = 0;
    public List<Dialogue> listDialogues;
    public GameObjectsManager gameObjectsManager;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    private void Start()
    {
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
        Phase00();
    }

    public void Manager(int interaction)
    {
        switch (interaction)
        {
            //Interaction Pédale
            case (1):
                switch (phaseNum)
                {
                    case (1):
                        Phase01();
                        break;
                }
                break;

            //Scan HU
            case (2):
                switch (phaseNum)
                {
                    case (2):
                        Phase02();
                        break;
                }
                break;

            //Ouverture Ecran
            case (3):
                switch (phaseNum)
                {
                    case (3):
                        Phase03();
                        break;
                }
                break;

            //Skip automatique
            case (4):
                switch (phaseNum)
                {
                    case (4):
                        Phase04();
                        break;

                    case (5):
                        Phase05();
                        break;

                    case (6):
                        Phase06();
                        break;

                    case (7):
                        Phase07();
                        break;

                    case (8):
                        Phase08();
                        break;

                    case (9):
                        Phase09();
                        break;

                    case (11):
                        Phase11();
                        break;
                }
                break;

            //Interaction onglet Recount
            case (5):
                switch (phaseNum)
                {
                    case (10):
                        Phase10();
                        break;
                }
                break;

            //Interaction bouton Recount
            case (6):
                switch (phaseNum)
                {
                    case (12):
                        Phase12();
                        break;
                }
                break;

            //Fermeture Ecran
            case (7):
                switch (phaseNum)
                {
                    case (13):
                        Phase13();
                        break;
                }
                break;
        }
    }

    void Phase00()
    {
        phaseNum++;
        //Prefab Dézoom disabled
        //Prefab Zoom enabled
        //Enable fond noir
        //New position (pédale) + Enable Spritemask
        //Doigt click new position (pédale) + set active
        gameObjectsManager.GameObjectToButton(gameObjectsManager.pedal).interactable = true;
    }

    void Phase01()
    {
        phaseNum++;
        //Disable fond noir
        //Disable Spritemask
        //Disable doigt
        gameObjectsManager.GameObjectToButton(gameObjectsManager.pedal).interactable = false;
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
        //Enable Fond noir
        //New position (pistolet + HU) + Enable Spritemask x2 
        //Doigt slide new position (pistolet > HU) + set active
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pistolet).enabled = true;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis).enabled = true;
    }

    void Phase02()
    {
        phaseNum++;
        //Disable fond noir
        //Disable Spritemask
        //Disable doigt
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pistolet).enabled = false;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis).enabled = false;
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
        //Enable Fond noir
        //New position (écran) + Enable Spritemask
        //Doigt click new position (écran) + set active
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = true;
    }

    void Phase03()
    {
        phaseNum++;
        //Disable fond noir
        //Disable Spritemask
        //Disable doigt
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = false;
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
        //Enable Fond noir
        //New position (liste anomalie) + Enable Spritemask
        Manager(4);
    }

    void Phase04()
    {
        phaseNum++;
        //Disable fond noir
        //Disable Spritemask
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
        //Enable Fond noir
        //New position (pcb HU) + Enable Spritemask
        Manager(4);
    }

    void Phase05()
    {
        phaseNum++;
        //Disable Fond noir
        //Disable Spritemask
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
        Manager(4);
    }

    void Phase06()
    {
        phaseNum++;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().closeBigMonitor();
        //Enable Fond noir
        //New position (scan RFID) + Enable Spritemask
        Manager(4);
    }

    void Phase07()
    {
        phaseNum++;
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
        Manager(4);
    }

    void Phase08()
    {
        phaseNum++;
        //Disable Fond noir
        //Disable Spritemask
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().monitorOpening = true;
        //Enable Fond noir
        //New position (onglet Recount) + Enable Spritemask
        Manager(4);
    }

    void Phase09()
    {
        phaseNum++;
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
        //Doigt click new position (onglet Recount) + set active
        gameObjectsManager.GameObjectToButton(gameObjectsManager.recountTab).interactable = true;
    }

    void Phase10()
    {
        phaseNum++;
        //Disable doigt
        //Enable Fond noir
        //New position (inputfield Recount) + Enable Spritemask
        Manager(4);
    }

    void Phase11()
    {
        phaseNum++;
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
        //New position (bouton Recount) Spritemask (end of dialogue ?)
        //Doigt click new position (onglet Recount) + set active
        gameObjectsManager.GameObjectToButton(gameObjectsManager.recountButton).interactable = true;
    }

    void Phase12()
    {
        phaseNum++;
        //Disable Fond noir
        //Disable Spritemask
        //Disable doigt
        gameObjectsManager.GameObjectToButton(gameObjectsManager.recountTab).interactable = false;
        gameObjectsManager.GameObjectToButton(gameObjectsManager.recountButton).interactable = false;
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
        //Doigt slide new position (écran zoom > droite) + set active
    }

    void Phase13()
    {
        phaseNum++;

    }
}
