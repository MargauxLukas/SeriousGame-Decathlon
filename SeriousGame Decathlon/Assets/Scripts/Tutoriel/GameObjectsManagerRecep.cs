using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObjectsManagerRecep : MonoBehaviour
{
    [Header("Convoyeur")]
    public GameObject onOffButton;
    public GameObject unfoldButton;
    public GameObject foldUpButton;
    public GameObject ascendButton;
    public GameObject descendButton;
    public GameObject validateButton;
    public GameObject posteInteractible;
    public GameObject ventouse;

    [Header("Colis")]
    public GameObject colis1;
    public GameObject colis2;
    public GameObject colis3;
    public GameObject colis4;
    public GameObject colis5;
    public GameObject colis6;
    public GameObject colis7;
    public GameObject colis8;
    public GameObject colis9;
    public GameObject colis10;
    public GameObject colis11;
    public GameObject colis12;
    public GameObject colis13;
    public GameObject colis14;
    public GameObject colis15;
    public GameObject colis16;

    [Header("Poste Anomalies")]
    public GameObject detectAnomaliesDesk;
    public GameObject cartonGestAnomalies;
    public GameObject ampoule;
    public GameObject ampouleSpriteMask;
    public GameObject etiqueteuse;
    public GameObject detectAnomaliesDeskLED;

    [Header("Menu Circulaire")]
    public GameObject menuCirculaireTuto;
    public GameObject menuCirculaireTutoSpriteMask;
    public GameObject whiteCircle12;
    public GameObject whiteCircle22;
    public GameObject menuCirculaireSpriteMask;

    [Header("Menu Tourner")]
    public GameObject upArrow;
    public GameObject downArrow;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject leftRotaArrow;
    public GameObject rightRotaArrow;
    public GameObject palette;

    [Header("Sprite Masks")]
    public GameObject blackScreen;
    public GameObject blackScreenDezoom;
    public GameObject squareSpriteMask01;
    public GameObject squareSpriteMask02;
    public GameObject circleSpriteMask01;
    public GameObject circleSpriteMask02;
    public GameObject upArrowTurnSpriteMask;

    [Header("Doigt")]
    public GameObject doigtClick;
    public GameObject doigtClickSpriteMask;
    public GameObject doigtStay;
    public GameObject doigtStaySpriteMask;

    [Header("Flèche")]
    public GameObject arrow;
    public GameObject arrowSpriteMask;
    public GameObject arrowCameraDezoom;

    [Header("Canvas")]
    public GameObject quitButton;
    public GameObject returnContainerButton;

    [Header("Trigger")]
    public GameObject cameraTriggerUp;
    public GameObject cameraTriggerDown;

    [Header("Player")]
    public GameObject player;

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

    public Transform GameObjectToTransform(GameObject gameObject)
    {
        return gameObject.GetComponent<Transform>();
    }

    public BoxCollider2D GameObjectToBoxCollider(GameObject gameObject)
    {
        return gameObject.GetComponent<BoxCollider2D>();
    }

    public Button GameObjectToButton(GameObject gameObject)
    {
        return gameObject.GetComponent<Button>();
    }
}
