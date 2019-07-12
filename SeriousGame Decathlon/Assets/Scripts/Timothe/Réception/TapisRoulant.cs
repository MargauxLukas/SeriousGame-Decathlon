using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapisRoulant : MonoBehaviour
{
    public List<GameObject> colisSurLeTapis;
    public List<GameObject> colisEnvoye;
    public float speed = 0.3f;
    public RotationScript rotationScr;
    public GameObject lastColis;

    public TapisRoulantGeneral tapisGeneral;

    public Transform positionTapisZoom;

    private bool menuIsOpen;

    public GameObject turnMenu;

    public Vector2 turnMenuPosition;

    // Start is called before the first frame update
    void Start()
    {
        turnMenuPosition = turnMenu.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (colisSurLeTapis.Count > 0 && !tapisGeneral.doesStop)
        {
            foreach (GameObject colisTap in colisSurLeTapis)
            {
                if (colisTap.GetComponent<ScriptColisRecep>() != null && colisTap.GetComponent<ScriptColisRecep>().canMove)
                {
                    colisTap.transform.position += new Vector3(0, 1, 0) * speed * Time.deltaTime;
                }
            }
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            if (menuIsOpen)
            {
                turnMenuPosition = turnMenu.transform.position;
                if (Vector2.Distance(touchPosition, turnMenuPosition) > 5f)
                {
                    turnMenu.SetActive(false);
                    menuIsOpen = false;
                    lastColis.GetComponent<ScriptColisRecep>().canMove = true;
                    lastColis = null;
                }
            }
        }
    }

    public void AddColis(GameObject colisToAdd)
    {
        rotationScr.cartonObj = colisToAdd;
        rotationScr.carton = colisToAdd.GetComponent<SpriteRenderer>();
        rotationScr.cartonsSprites = colisToAdd.GetComponent<ScriptColisRecep>().colisScriptable.carton.spriteCartonsListe;
        lastColis = colisToAdd;
        colisSurLeTapis.Add(colisToAdd);
        OpenTurnMenu();
    }

    public void OpenTurnMenu()
    {
        if (lastColis.GetComponent<ScriptColisRecep>().colisScriptable.isBadOriented)
        {
            rotationScr.resetAll();
            rotationScr.actualFace = rotationScr.squareList[4];
            rotationScr.squareList[4].isCurrentlyPick = true;
        }
        else
        {
            rotationScr.resetAll();
            rotationScr.actualFace = rotationScr.squareList[0];
            rotationScr.squareList[0].isCurrentlyPick = true;
            rotationScr.squareList[0].fullRotation = 90;
        }
        menuIsOpen = true;
        turnMenu.SetActive(true);
    }
}
