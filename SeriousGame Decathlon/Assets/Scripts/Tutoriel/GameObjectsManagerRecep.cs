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

    [Header("Colis")]
    public GameObject colis1;
    public GameObject colis2;
    public GameObject colis3;
    public GameObject colis4;
    public GameObject colis5;
    public GameObject colis6;

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
    public GameObject squareSpriteMask01;
    public GameObject squareSpriteMask02;
    public GameObject circleSpriteMask01;
    public GameObject circleSpriteMask02;
    public GameObject upArrowTurnSpriteMask;
    public GameObject rightRotaArrowTurnSpriteMask;

    [Header("Doigt")]
    public GameObject doigtClick;
    public GameObject doigtClickSpriteMask;
    public GameObject doigtStay;
    public GameObject doigtStaySpriteMask;

    [Header("Flèche")]
    public GameObject arrow;
    public GameObject arrowSpriteMask;

    [Header("Canvas")]
    public GameObject quitButton;

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
