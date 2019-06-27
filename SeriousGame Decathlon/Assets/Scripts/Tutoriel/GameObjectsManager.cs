using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObjectsManager : MonoBehaviour
{
    public GameObject pedal;
    public GameObject pistolet;
    public GameObject colis;
    public GameObject screen;
    public GameObject bigScreen;
    public GameObject scanRFID;
    public GameObject pileArticles;
    public GameObject ticketHU;

    //Ecran
    public GameObject toggleEndTask1;
    public GameObject toggleEndTask2;
    
    //RecountTab
    public GameObject recountTab;
    public GameObject recountButton;
    public GameObject inventoryButton;
    public GameObject printHUButton;
    public GameObject printRFIDButton;

    //FillingRateTab
    public GameObject fillTab;
    public GameObject fill50;
    public GameObject fill100;
    public GameObject fill125;


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
