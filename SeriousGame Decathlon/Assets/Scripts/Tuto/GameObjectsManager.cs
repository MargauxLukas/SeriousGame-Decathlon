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

    //RecountTab
    public GameObject recountTab;
    public GameObject recountButton;
    public GameObject inventoryButton;
    public GameObject printHUButton;
    public GameObject printRFIDButton;

    public GameObject repackTab;
    public GameObject fillTab;
    public GameObject colis;
    public GameObject menuCirculaireColis;
    public GameObject pileArticles;
    public GameObject menuCirculairePileArticles;
    //WIP

    public BoxCollider2D GameObjectToBoxCollider(GameObject gameObject)
    {
        return gameObject.GetComponent<BoxCollider2D>();
    }

    public Button GameObjectToButton(GameObject gameObject)
    {
        return gameObject.GetComponent<Button>();
    }
}
