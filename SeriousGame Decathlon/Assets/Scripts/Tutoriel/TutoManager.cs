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
   
    //Menu circulaire colis
    public bool canJeter = false;
    public bool canVider = false;
    public bool canOuvrirFermer = false;
    public bool canOpenTurnMenu = false;

    //Menu circulaire articles
    public bool canColis1 = false;
    public bool canColis2 = false;
    public bool canInfo = false;

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

                    case (15):
                        Phase15();
                        break;

                    case (30):
                        Phase30();
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

                    case (16):
                        Phase16();
                        break;

                    case (18):
                        Phase18();
                        break;

                    case (19):
                        Phase19();
                        break;

                    case (26):
                        Phase26();
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

                    case (17):
                        Phase17();
                        break;
                }
                break;

            //Scan RFID
            case (8):
                switch (phaseNum)
                {
                    case (14):
                        Phase14();
                        break;
                }
                break;

            //Ouverture Menu circulaire Colis
            case (9):
                switch (phaseNum)
                {
                    case (20):
                        Phase20();
                        break;

                    case (22):
                        Phase22();
                        break;
                }
                break;

            //Interaction bouton Ouvrir
            case (10):
                switch (phaseNum)
                {
                    case (21):
                        Phase21();
                        break;
                }
                break;

            //Interaction bouton Vider
            case (11):
                switch (phaseNum)
                {
                    case (23):
                        Phase23();
                        break;
                }
                break;

            //Ouverture Menu circulaire Articles
            case (12):
                switch (phaseNum)
                {
                    case (24):
                        Phase24();
                        break;

                    case (28):
                        Phase28();
                        break;
                }
                break;

            //Interaction bouton Info Articles
            case (13):
                switch (phaseNum)
                {
                    case (25):
                        Phase25();
                        break;
                }
                break;

            //Fermeture Fiche info
            case (14):
                switch (phaseNum)
                {
                    case (27):
                        Phase27();
                        break;
                }
                break;

            //Interaction bouton Colis1
            case (15):
                switch (phaseNum)
                {
                    case (29):
                        Phase29();
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
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;
        //Doigt slide new position (écran zoom > droite) + set active
    }

    void Phase13()
    {
        phaseNum++;
        //Disable doigt
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
        //Enable Fond noir
        //New position (scan RFID + colis) + Enable Spritemask x2
        //Doigt slide (secouer) new position (colis > scan RFID) + set active
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis).enabled = true;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.scanRFID).enabled = true;
    }

    void Phase14()
    {
        phaseNum++;
        //Disable Fond noir
        //Disable Spritemask
        //Disable doigt
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis).enabled = false;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.scanRFID).enabled = false;
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
        //Enable Fond noir
        //New position (écran) + Enable Spritemask
        //Doigt click new position (écran) + set active
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = true;
    }

    void Phase15()
    {
        phaseNum++;
        //Disable doigt
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = false;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
        //New position (inputfield Recount + PCB HU) Spritemask
        Manager(4);
    }

    void Phase16()
    {
        phaseNum++;
        //Disable Fond noir
        //Disable Spritemask
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = true;
    }

    void Phase17()
    {
        phaseNum++;
        gameObjectsManager.bigScreen.GetComponent<BigMonitor>().enabled = false;
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
        //Doigt maintenir new position (colis) + set active
        Manager(4);
    }

    void Phase18()
    {
        phaseNum++;
        //Enable Sprite Menu circulaire
        //Enable Fond noir
        //New position (bouton ouvrir) + Enable Spritemask
        //Doigt slide new position (colis > bouton Ouvrir)
        Manager(4);
    }

    void Phase19()
    {
        phaseNum++;
        //Disable Sprite Menu Ciruclaire
        //Disable Fond noir
        //Disable Spritemask
        //Doigt maintenir new position (colis)
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis).enabled = true;
    }

    void Phase20()
    {
        phaseNum++;
        canOuvrirFermer = true;
    }

    void Phase21()
    {
        phaseNum++;
        //Disable Doigt
        canOuvrirFermer = false;
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
    }

    void Phase22()
    {
        phaseNum++;
        canVider = true;
    }

    void Phase23()
    {
        phaseNum++;
        canVider = false;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis).enabled = false;
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
        //Doigt maintenir new position (pile article) + set active
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pileArticles).enabled = true;
    }

    void Phase24()
    {
        phaseNum++;
        //Doigt slide new position (pile article > bouton Info)
        canInfo = true;
    }

    void Phase25()
    {
        phaseNum++;
        //Disable Doigt
        canInfo = false;
        //Enable Fond noir
        //New position (fiche info) + Enable Spritemask
        Manager(4);
    }

    void Phase26()
    {
        phaseNum++;
        //Disable fond noir
        //Disable Spritemask
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
        //Doigt click new position (anywhere) + set active
    }

    void Phase27()
    {
        phaseNum++;
        //Disable Doigt
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
        //Enable Fond noir
        //New position (pile articles) + Enable Spritemask
        //Doigt maintenir new position (pile article) + set active
    }

    void Phase28()
    {
        phaseNum++;
        //Disable fond noir
        //Disable Spritemask
        //Disable Doigt
        canColis1 = true;
    }

    void Phase29()
    {
        phaseNum++;
        canColis1 = false;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pileArticles).enabled = false;
        dialogueManager.LoadDialogue(listDialogues[dialogNum]);
        dialogNum++;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = true;
    }

    void Phase30()
    {
        phaseNum++;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.screen).enabled = false;
        gameObjectsManager.GameObjectToButton(gameObjectsManager.recountTab).interactable = true;
    }
}
