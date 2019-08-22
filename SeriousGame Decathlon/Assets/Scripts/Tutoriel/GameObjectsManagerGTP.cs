using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObjectsManagerGTP : MonoBehaviour
{
    [Header("Ecran")]
    public GameObject loupe1Button;
    public GameObject loupe3Button;
    public GameObject listPickTUBackButton;
    public GameObject correctPickedQtyButton;
    public GameObject correctPickedQtyMinusButton;
    public GameObject correctPickedQtyOkButton;
    public GameObject closePickTUButton;
    public GameObject remainingQtyPlusButton;
    public GameObject remainingQtyOkButton;
    public GameObject anomalyButton;
    public GameObject correctSourceQtyButton;
    public GameObject correctSourceQtyWrongPlusButton;
    public GameObject correctSourceQtyMissingPlusButton;
    public GameObject correctSourceQtyConfirmButton;
    public GameObject correctSourceQtyBackButton;
    public GameObject wrongProductButton;

    [Header("Colis")]
    public GameObject colisSource1;
    public GameObject colisSource2;

    [Header("Cartons (Assign. auto) + Manager")]
    public ManagerColisVider colisManager;
    public GameObject colisVide1;
    public GameObject colisVide2;
    public GameObject colisVide3;

    [Header("Machine")]
    public GameObject bac1; 
    public GameObject bac2;
    public GameObject bac3;
    public GameObject pushButton1;
    public GameObject pushButton2;
    public GameObject pushButton3;
    public GameObject scanRFID;
    public GameObject console3MinusButton;
    public GameObject console3ValidateButton;
    public GameObject quitButton;

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

    private void Start()
    {
        colisManager = colisManager.GetComponent<ManagerColisVider>();
    }
    private void Update()
    {
        if(colisManager.colisActuellementsPose != null)
        {
            if(colisManager.colisActuellementsPose[0] != null)
            {
                colisVide1 = colisManager.colisActuellementsPose[0].gameObject;
            }
            if(colisManager.colisActuellementsPose[1] != null)
            {
                colisVide2 = colisManager.colisActuellementsPose[1].gameObject;
            }
            if (colisManager.colisActuellementsPose[2])
            {
                colisVide3 = colisManager.colisActuellementsPose[2].gameObject;
            }
        }
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
