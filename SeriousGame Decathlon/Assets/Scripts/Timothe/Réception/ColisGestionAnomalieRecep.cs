using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColisGestionAnomalieRecep : MonoBehaviour
{
    [Header("Camera")]
    public Camera cameraGeneral;

    [Header("ASSIGNER AUTOMATIQUEMENT")]
    public Colis colisScriptable;
    public SpriteRenderer colisTapis;

    [Header("Anomalie")]
    public DetectionAnomalieRecep detect;
    public GameObject gestionAnomalieRecep;

    [Header("Palette")]
    public CreationDePalette paletteManager;

    [Header("Listes sprite Carton")]
    public List<Sprite> spriteCartons;

    [Header("GameObject")]
    public GameObject tournerMenu;

    public int changeDirection = 0;                                 //Appelé que ici, private ?

    [Header("Menu Circulaire")]
    public Image circleImage;
    private Vector2 startPosition;
    private Vector2 circlePosition;

    [Header("Menu")]
    public RotationScript rotationScr;
    public  bool menuIsOpen  = false;
    private bool menuCanOpen =  true;
    private bool tournerMenuIsOpen  ;
    public float timeBeforeMenuOpen = 1;

    [Header("Item")]
    public  int itemNumber = 5;
    private int currentItem;

    public float timeTouched;
    public bool doesTouch;

    void Start()
    {
        circlePosition = Vector2.zero;
        circleImage.fillAmount = 1f / itemNumber;
    }

    private void OnDisable()
    {
        if (!gameObject.transform.parent.gameObject.activeSelf && tournerMenuIsOpen)
        {
            tournerMenu.SetActive(false);
            tournerMenuIsOpen = false;
            doesTouch = false;
        }
    }

    void Update()
    {
        if(!gestionAnomalieRecep.activeSelf)
        {
            if(circleImage.transform.parent.gameObject.activeSelf)
            {
                circleImage.transform.parent.gameObject.SetActive(true);
                currentItem = -1;
            }
            if(doesTouch)
            {
                doesTouch = false;
            }
        }
        if (colisTapis != null)
        {
            if (colisTapis.sprite != GetComponent<SpriteRenderer>().sprite)
            {
                GetComponent<SpriteRenderer>().sprite = colisTapis.sprite;
            }
            if (colisTapis.transform.rotation != transform.rotation)
            {
                transform.rotation = colisTapis.transform.rotation;
                transform.localScale = new Vector3(2, 2, 1);
            }
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            //Debug.Log(Vector2.Distance(new Vector3(cameraGeneral.ScreenToWorldPoint(touch.position).x, cameraGeneral.ScreenToWorldPoint(touch.position).y, 0), transform.position));

            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            if (!tournerMenuIsOpen)
            {
                touchObject();

                if (doesTouch)
                {
                    //Menu circulaire
                    if (touch.phase == TouchPhase.Began)
                    {
                        currentItem = -1;
                        timeTouched =  0;
                        startPosition  = touchPosition;
                        circlePosition = touchPosition;
                    }
                    else if (Vector3.Distance(startPosition, touchPosition) > 1f && timeTouched < timeBeforeMenuOpen)
                    {
                        menuCanOpen = false;
                        menuIsOpen  = false;
                    }

                    timeTouched += Time.deltaTime;

                    if (timeTouched > timeBeforeMenuOpen && menuCanOpen)
                    {
                        menuIsOpen = true;
                        circleImage.transform.parent.gameObject.SetActive(true);
                        circleImage.transform.parent.gameObject.transform.position = transform.position;
                        circleImage.fillAmount = 1f / itemNumber;

                        if (touch.phase == TouchPhase.Moved)
                        {
                            if (Vector2.Distance(startPosition, touchPosition) > 1f)
                            {
                                currentItem = GetItemFromAngle(GetAngle(startPosition, touchPosition));
                            }
                            else
                            {
                                currentItem = -1;
                            }
                        }
                        else if (touch.phase == TouchPhase.Ended)
                        {
                            menuCanOpen =  true;
                            menuIsOpen  = false;

                            circleImage.transform.parent.gameObject.SetActive(false);
                            if (currentItem > -1)
                            {
                                PickInventory(currentItem);
                            }
                            return;
                        }
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        menuCanOpen =  true;
                        menuIsOpen  = false;
                    }
                }
            }
            else if (Vector2.Distance(touchPosition, tournerMenu.transform.position) > 6f)
            {
                tournerMenu.SetActive(false);
                tournerMenuIsOpen = false;
            }
        }
        else
        {
            doesTouch = false;
            if (circleImage.transform.parent.gameObject.activeSelf)
            {
                circleImage.transform.parent.gameObject.SetActive(false);
            }
        }
    }

    void touchObject()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit2D hit = Physics2D.Raycast(cameraGeneral.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject != null && gameObject != null && hit.collider.gameObject == gameObject && hit.collider.gameObject.name == gameObject.name)
            {
                doesTouch = true;
            }
        }
    }

    float GetAngle(Vector2 startPos, Vector2 endPos)
    {
        float angle = 0;
        Vector2 baseAngle = Vector2.zero;
        Vector2 direction = endPos - startPos;

        angle = (Mathf.Atan2(circlePosition.y - circlePosition.y, circlePosition.x - circlePosition.x) - Mathf.Atan2(endPos.y - circlePosition.y, endPos.x - circlePosition.x)) * Mathf.Rad2Deg;
        Debug.Log(angle);
        Debug.Log("Touch : " + Input.GetTouch(0).position);
        Debug.Log("Circle Pos : " + circlePosition);

        if (angle < 0)
        {
            angle += 360;
        }
        return angle;
    }

    int GetItemFromAngle(float angle)
    {
        int itemNb = 0;

        itemNb = (int)((angle + 90) / (360 / itemNumber));

        circleImage.transform.eulerAngles = new Vector3(0, 180, (360 / itemNumber) * itemNb);
        return itemNb;
    }

    /*Solutions possibles
     * - Mettre le colis de côté
     * - Tourner le colis
     * - Y imprimer un nouvel HU
     */

    void PickInventory(int nb)
    {
        switch (nb)
        {
            case 1:
                Jeter();
                TellSomething(1);
                break;
            case 2:
                OpenTurnMenu();
                TellSomething(3);
                break;
            case 0:
                OpenTurnMenu();
                TellSomething(3);
                break;
        }
    }

    void TellSomething(int texte)
    {
        //Debug.Log(texte);
    }

    public void OpenTurnMenu()
    {
        menuCanOpen = false;
        if (colisScriptable.isBadOriented)
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
        tournerMenuIsOpen = true;
        tournerMenu.SetActive(true);
    }

    public void Jeter() //Permet de mettre le colis sur le côté
    {
        paletteManager.colisDeCote.Add(colisScriptable);
        Destroy(detect.colisATraiter);
        GetComponent<SpriteRenderer>().sprite = null;
        detect.ResolveAnomalie();
    }
}
