using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObjectsManager : MonoBehaviour
{
    public GameObject pedal;
    public GameObject pistolet;
    public GameObject screen;
    public GameObject bigScreen;
    public GameObject scanRFID;
    public GameObject ticketHU;
    public GameObject quitButtonSendView;
    public GameObject quitButtonWorkView;

    //Colis1
    public GameObject colis1;
    public GameObject pileArticlesColis1;

    //Colis2
    public GameObject colis2;
    public GameObject pileArticles1Colis2;
    public GameObject pileArticles2Colis2;

    //Ecran
    public GameObject toggleEndTask1;
    public GameObject toggleEndTask2;
    public GameObject toggleEndTask3;
    public GameObject returnMecaButton;
    
    //RecountTab
    public GameObject recountTab;
    public GameObject recountButton;
    public GameObject inventoryButton;
    public GameObject printHUButton;
    public GameObject printRFIDButton;

    //FillingRateTab
    public GameObject fillTab;
    public GameObject fill50Button;
    public GameObject fill100Button;
    public GameObject fill125Button;
    public GameObject mecaOpenToggle;

    //Menu Tourner
    public GameObject turnRightButton;
    public GameObject turnLeftButton;
    public GameObject turnUpButton;
    public GameObject turnDownButton;
    public GameObject rightRotationButton;
    public GameObject leftRotationButton;


    //WIP
    public GameObject repackTab;

    public BoxCollider2D GameObjectToBoxCollider(GameObject gameObject)
    {
        return gameObject.GetComponent<BoxCollider2D>();
    }

    public Button GameObjectToButton(GameObject gameObject)
    {
        return gameObject.GetComponent<Button>();
    }

    public Toggle GameObjectToToggle(GameObject gameObject)
    {
        return gameObject.GetComponent<Toggle>();
    }
}
