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
    public GameObject telephone;
    public GameObject quitButtonWorkView;
    public GameObject quitButtonToMenu;
    public GameObject menuCirculaireColis;
    public GameObject CB02Button;

    [Header("Colis 1")]
    public GameObject colis1;
    public GameObject pileArticlesColis1;

    [Header("Colis 2")]
    public GameObject colis2;
    public GameObject colis2bis;
    public GameObject pileArticles1Colis2;
    public GameObject pileArticles2Colis2;

    [Header("Colis 3")]
    public GameObject colis3;
    public GameObject colis3bis;

    [Header("Ecran principal")]
    public GameObject toggleEndTask1;
    public GameObject toggleEndTask2;
    public GameObject toggleEndTask3;
    public GameObject returnMecaButton;
    public GameObject createHUButton;

    [Header("Recount Tab")]
    public GameObject recountTab;
    public GameObject recountButton;
    public GameObject inventoryButton;
    public GameObject recountPrintHUButton;
    public GameObject printRFIDButton;

    [Header("Filling Rate Tab")]
    public GameObject fillTab;
    public GameObject fill50Button;
    public GameObject fill100Button;
    public GameObject fill125Button;
    public GameObject mecaOpenToggle;

    [Header("Repack Tab")]
    public GameObject repackTab;
    public GameObject newColisButton;
    public GameObject repackTabMinusButton;
    public GameObject textQuantity1;
    public GameObject textQuantity2;
    public GameObject repackPrintHUButton;
    public string quantity1;
    public string quantity2;

    [Header("Menu Create HU")]
    public GameObject packMatDropdown;
    public GameObject refDropdown;
    public GameObject createHUPlusButton;
    public GameObject createHUMinusButton;
    public GameObject buttonOK;

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
    public GameObject menuColis44SpriteMaskTuto;
    public GameObject menuArticles13SpriteMask;
    public GameObject menuArticles23SpriteMask;
    public GameObject menuArticles33SpriteMask;

    [Header("Doigt")]
    public GameObject doigtClick;
    public GameObject doigtClickSpriteMask;
    public GameObject doigtStay;
    public GameObject doigtStaySpriteMask;

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

    public Dropdown GameObjectToDropdown(GameObject gameObject)
    {
        return gameObject.GetComponent<Dropdown>();
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

    public ColisScript GameObjectToColisScript(GameObject gameObject)
    {
        return gameObject.GetComponent<ColisScript>();
    }
}
