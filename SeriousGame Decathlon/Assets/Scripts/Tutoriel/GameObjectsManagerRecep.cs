﻿using System.Collections;
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

    [Header("Sprite Masks")]
    public GameObject blackScreen;
    public GameObject squareSpriteMask01;
    public GameObject squareSpriteMask02;
    public GameObject circleSpriteMask01;
    public GameObject circleSpriteMask02;

    [Header("Doigt")]
    public GameObject doigtClick;
    public GameObject doigtClickSpriteMask;
    public GameObject doigtStay;
    public GameObject doigtStaySpriteMask;

    [Header("Flèche")]
    public GameObject arrow;
    public GameObject arrowSpriteMask;

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
