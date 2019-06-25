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
            //interaction pédale
            case (1):
                switch (phaseNum)
                {
                    case (1):
                        Phase01();
                        break;
                }
                break;

            case (2):
                switch (phaseNum)
                {
                    case (2):
                        Phase02();
                        break;
                }
                break;
        }
    }

    void Phase00()
    {
        phaseNum++;
        //CameraTopDown disabled
        //CameraHori enabled
        //Fond noir set active
        //Spritemask new position + set active
        //Doigt new position + set active
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
        //New position + Enable Spritemask x2 
        //Doigt new position + set active + animation
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.pistolet).enabled = true;
        gameObjectsManager.GameObjectToBoxCollider(gameObjectsManager.colis).enabled = true;
    }

    void Phase02()
    {

    }
}
