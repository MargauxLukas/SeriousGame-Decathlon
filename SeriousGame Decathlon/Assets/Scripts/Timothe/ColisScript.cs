using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColisScript : MonoBehaviour
{
    private bool isMoving;

    public Colis colisScriptable;
    //public MenuCirculaireV2 menuCirculaire;


    public bool estSecoue;
    public int changeDirection;
    private bool goRight;
    private bool canMove = true;

    private float deltaTimeShake;

    public List<Article> articleOnTable;
    public GameObject tournerMenu;
    public GameObject spriteArticleTable;

    public List<Sprite> spriteCartons;

    //Pour le menu circulaire
    public Image circleImage;
    private Vector2 startPosition;
    private Vector2 circlePosition;
    public int itemNumber;
    private int currentItem;
    public bool menuIsOpen;
    private bool menuCanOpen = true;
    public float timeBeforeMenuOpen;
    private float timeTouched;

    public bool asBeenScanned;

    // Start is called before the first frame update
    void Start()
    {
        Colis newColis = Instantiate(colisScriptable);
        colisScriptable = newColis;
        circlePosition = Vector2.zero;
        circleImage.fillAmount = 1f / itemNumber;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(menuCirculaire.isOpen && isMoving)
        {
            canMove = false;
        }
        else if(!canMove)
        {
            canMove = true;
        }*/

        deltaTimeShake += Time.deltaTime;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (!tournerMenu.gameObject.activeSelf)
            {
                touchObject();
                if (isMoving)
                {
                    //Menu circulaire

                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    touchPosition.z = 0;

                    if (touch.phase == TouchPhase.Began)
                    {
                        currentItem = -1;
                        timeTouched = 0;
                        startPosition = touchPosition;
                        circlePosition = transform.position;
                    }
                    else if (Vector3.Distance(startPosition, touchPosition) > 2f && timeTouched < timeBeforeMenuOpen)
                    {
                        menuCanOpen = false;
                        menuIsOpen = false;
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        menuCanOpen = true;
                        menuIsOpen = false;
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
                            menuCanOpen = true;
                            menuIsOpen = false;
                            circleImage.transform.parent.gameObject.SetActive(false);
                            if (currentItem > -1)
                            {
                                PickInventory(currentItem);
                            }
                            return;
                        }
                    }

                    //Déplacement du Colis
                    if (canMove && !menuIsOpen)
                    {
                        Vector3 ancientPosition = transform.position;

                        transform.position = new Vector3(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)).x, transform.position.y, 0);

                        //Vérification colis secoué
                        if (transform.position.x - ancientPosition.x > 0 && !goRight)
                        {
                            goRight = true;
                            if (deltaTimeShake <= 0.85f || changeDirection == 0)
                            {
                                changeDirection++;
                                deltaTimeShake = 0;
                            }
                        }
                        else if (transform.position.x - ancientPosition.x < 0 && goRight)
                        {
                            goRight = false;
                            if (deltaTimeShake <= 0.85f || changeDirection == 0)
                            {
                                changeDirection++;
                                deltaTimeShake = 0;
                            }
                        }

                        if (changeDirection >= 3)
                        {
                            Debug.Log("Est secoué");
                            changeDirection = 0;
                            estSecoue = true;
                        }
                        else if (deltaTimeShake >= 1.5f && estSecoue)
                        {
                            Debug.Log("Est plus secoué");
                            estSecoue = false;
                        }
                    }
                }
            }
            else if(Vector3.Distance(Camera.main.ScreenToWorldPoint(touch.position),transform.position) >= 10f)
            {
                Debug.Log(Vector3.Distance(Camera.main.ScreenToWorldPoint(touch.position), transform.position));
                tournerMenu.SetActive(false);
            }
        }
        else
        {
            if(circleImage.transform.parent.gameObject.activeSelf)
            {
                circleImage.transform.parent.gameObject.SetActive(false);
            }
            isMoving = false;
        }
    }

    void touchObject()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
            if (hit.collider.gameObject != null && hit.collider.gameObject == gameObject)
            {
                isMoving = true;
            }
        }
    }

    float GetAngle(Vector2 startPos, Vector2 endPos)
    {
        float angle = 0;
        Vector2 baseAngle = Vector2.zero;
        Vector2 direction = endPos - startPos;

        angle = (Mathf.Atan2(0 - circlePosition.y, 1 - circlePosition.x) - Mathf.Atan2(endPos.y - circlePosition.y, endPos.x - circlePosition.x)) * Mathf.Rad2Deg;
        if (angle < 0)
        {
            angle += 360;
        }
        return angle;
    }

    int GetItemFromAngle(float angle)
    {
        int itemNb = 0;

        itemNb = (int)((angle + 180) / (360 / itemNumber));

        circleImage.transform.eulerAngles = new Vector3(0, 180, (360 / itemNumber) * itemNb);
        return itemNb;
    }

    void PickInventory(int nb)
    {
        switch (nb)
        {
            case 6:
                Jeter();
                TellSomething(1);
                break;
            case 7:
                Vider();
                TellSomething(2);
                break;
            case 2:
                Vider();
                TellSomething(2);
                break;
            case 3:
                OuvrirFermer();
                TellSomething(3);
                break;
            case 4:
                Remplir();
                TellSomething(4);
                break;
            case 5:
                OpenTurnMenu();
                TellSomething(5);
                break;
        }
    }

    void TellSomething(int texte)
    {
        Debug.Log(texte);
    }

    void OuvrirFermer()
    {
        colisScriptable.OuvrirFermer();
        if (colisScriptable.estOuvert)
        {
            Color newColo = GetComponent<SpriteRenderer>().color;
            newColo.a = 0.3f;
            GetComponent<SpriteRenderer>().color = newColo;
        }
        else if(!colisScriptable.estOuvert)
        {
            Color newColo = GetComponent<SpriteRenderer>().color;
            newColo.a = 1f;
            GetComponent<SpriteRenderer>().color = newColo;
        }
    }

    void OpenTurnMenu()
    {
        circleImage.transform.parent.gameObject.SetActive(false);
        tournerMenu.transform.position = transform.position;
        tournerMenu.SetActive(true);
    }

    public void Tourner(Vector2 vect)
    {
        colisScriptable.Tourner(vect, spriteCartons);
    }

    void Vider()
    {
        List<Article> listTemporaire = colisScriptable.Vider();
        if (listTemporaire.Count > 0)
        {
            articleOnTable = listTemporaire;
        }
        Debug.Log(articleOnTable.Count);
        if (articleOnTable.Count>0)
        {
            spriteArticleTable.SetActive(true);
        }
    }

    void Remplir()
    {
        if (articleOnTable.Count > 0)
        {
            colisScriptable.Remplir(articleOnTable.Count, articleOnTable);
            articleOnTable = new List<Article>();
            if (articleOnTable.Count <= 0)
            {
                spriteArticleTable.SetActive(false);
            }
        }
    }

    void Jeter()
    {
        Debug.Log("A été jeté lachement");
    }
}
