using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObjectsManager : MonoBehaviour
{
    [Header("General")]
    public GameObject pedal;
    public GameObject pistolet;
    public GameObject screen;
    public GameObject bigScreen;
    public GameObject scanRFID;
    public GameObject quitButtonWorkView;

    [Header("Colis 1")]
    public GameObject colis1;
    public GameObject pileArticlesColis1;
    public GameObject newTicketHUColis1;

    [Header("Colis 2")]
    public GameObject colis2;
    public GameObject pileArticles1Colis2;
    public GameObject pileArticles2Colis2;
    public GameObject newTicketHUColis2;

    [Header("Ecran principal")]
    public GameObject toggleEndTask1;
    public GameObject toggleEndTask2;
    public GameObject toggleEndTask3;
    public GameObject returnMecaButton;

    [Header("Recount Tab")]
    public GameObject recountTab;
    public GameObject recountButton;
    public GameObject inventoryButton;
    public GameObject printHUButton;
    public GameObject printRFIDButton;

    [Header("Filling Rate Tab")]
    public GameObject fillTab;
    public GameObject fill50Button;
    public GameObject fill100Button;
    public GameObject fill125Button;
    public GameObject mecaOpenToggle;

    [Header("Menu Tourner")]
    public GameObject turnRightButton;
    public GameObject turnLeftButton;
    public GameObject turnUpButton;
    public GameObject turnDownButton;
    public GameObject rightRotationButton;
    public GameObject leftRotationButton;

    [Header("Sprite Masks")]
    public GameObject blackScreen;
    public GameObject squareSpriteMask01;
    public GameObject squareSpriteMask02;
    public GameObject circleSpriteMask;
    public GameObject menuColis14SpriteMask;
    public GameObject menuColis24SpriteMask;
    public GameObject menuColis34SpriteMask;
    public GameObject menuColis44SpriteMask;
    public GameObject menuArticles13SpriteMask;
    public GameObject menuArticles23SpriteMask;
    public GameObject menuArticles33SpriteMask;

    [Header("Doigt")]
    public GameObject doigtClick;
    public GameObject doigtStay;


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

    public Transform GameObjectToTransform(GameObject gameObject)
    {
        return gameObject.GetComponent<Transform>();
    }

    public SpriteRenderer GameObjectToSpriteRenderer(GameObject gameObject)
    {
        return gameObject.GetComponent<SpriteRenderer>();
    }

    public SpriteMask GameObjectToSpriteMask(GameObject gameObject)
    {
        return gameObject.GetComponent<SpriteMask>();
    }

    public Animator GameObjectToAnimator(GameObject gameObject)
    {
        return gameObject.GetComponent<Animator>();
    }
}
