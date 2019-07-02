using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColisScript : MonoBehaviour
{
    public Colis colisScriptable;

    [Header("Listes piles articles")]
    public List<Article> articleOnTableUn  ;  
    public List<Article> articleOnTableDeux;

    [Header("Listes sprite Carton")]
    public List<Sprite > spriteCartons     ;

    [Header("GameObject")]
    public GameObject IWayEtiquette         ;
    public GameObject tournerMenu           ;
    public GameObject spriteArticleTableUn  ;
    public GameObject spriteArticleTableDeux;
    //public MenuCirculaireV2 menuCirculaire;

    public int changeDirection = 0;                                 //Appelé que ici, private ?

    private bool goRight;
    private bool isMoving;
    private bool canMove   = true ;

    public bool estSecoue = false;
    public bool hasBeenScannedByRFID;
    public bool hasBeenScannedByPistol;

    private float deltaTimeShake;
    //public Text textArticleTableNombre;
    //public Text textArtcileTableRFID;

    [Header("Menu Circulaire")]
    public Image circleImage;
    private Vector2 startPosition;
    private Vector2 circlePosition;
    public  int itemNumber = 5;
    private int currentItem;
    public  bool menuIsOpen = false;
    private bool menuCanOpen = true;
    private bool tournerMenuIsOpen;
    public  float timeBeforeMenuOpen = 1;
    private float timeTouched;

    [Header("Renvoie Colis")]
    public bool doesEntrance;
    public bool doesEntranceSecond;
    public bool doesRenvoie;
    public bool canMoveVertical;
    public Vector3 entrancePosition;

    [Header("Carton Etat")]
    public SpriteRenderer spriteArticleDansColis;
    public SpriteMask spriteMaskArticleColis;

    // Start is called before the first frame update
    void Start()
    {
        entrancePosition = transform.position;
        circlePosition = Vector2.zero;
        circleImage.fillAmount = 1f / itemNumber;

        if (colisScriptable.isBadOriented && IWayEtiquette != null && colisScriptable.wayTicket != null)
        {
            IWayEtiquette.SetActive(false);
        }
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

        if (!doesEntrance && !doesRenvoie && !doesEntranceSecond)
        {
            deltaTimeShake += Time.deltaTime;

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (!tournerMenuIsOpen)
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
                            timeTouched =  0;
                            startPosition  = touchPosition     ;
                            circlePosition = transform.position;
                        }
                        else if (Vector3.Distance(startPosition, touchPosition) > 1f && timeTouched < timeBeforeMenuOpen)
                        {
                            menuCanOpen = false;
                            menuIsOpen  = false;
                        }
                        else if (touch.phase == TouchPhase.Ended)
                        {
                            menuCanOpen = true ;
                            menuIsOpen  = false;
                        }

                        timeTouched += Time.deltaTime;

                        if (timeTouched > timeBeforeMenuOpen && menuCanOpen)
                        {
                            menuIsOpen = true;
                            if(TutoManager.instance != null) {TutoManager.instance.Manager(9);}

                            circlePosition = transform.position;
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
                                menuCanOpen = true ;
                                menuIsOpen  = false;

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

                            if (!canMoveVertical)
                            {
                                transform.position = new Vector3(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)).x, transform.position.y, 0);
                            }
                            else
                            {
                                transform.position = new Vector3(transform.position.x, Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)).y, 0);
                            }

                            //Vérification colis secoué
                            if (transform.position.x - ancientPosition.x > 0 && !goRight)
                            {
                                goRight = true;
                                if (deltaTimeShake <= 1.5f || changeDirection == 0)
                                {
                                    deltaTimeShake = 0;
                                    changeDirection++;
                                }
                            }
                            else if (transform.position.x - ancientPosition.x < 0 && goRight)
                            {
                                deltaTimeShake = 0;
                                goRight = false;
                                if (deltaTimeShake <= 1.5f || changeDirection == 0)
                                {
                                    changeDirection++;
                                }
                            }

                            if (changeDirection >= 3)
                            {
                                //Debug.Log("Est secoué");
                                changeDirection = 0;
                                estSecoue = true;
                            }
                            else if (deltaTimeShake >= 2f && estSecoue)
                            {
                                //Debug.Log("Est plus secoué");
                                estSecoue = false;
                            }
                        }
                    }
                }
                else if (Vector3.Distance(new Vector3(Camera.main.ScreenToWorldPoint(touch.position).x, Camera.main.ScreenToWorldPoint(touch.position).y, 0), transform.position) >= 5f)
                {
                    Debug.Log(Vector3.Distance(Camera.main.ScreenToWorldPoint(touch.position), transform.position));
                    estSecoue = false;
                    tournerMenu.SetActive(false);
                    tournerMenuIsOpen = false;
                    if (TutoManager.instance != null) {TutoManager.instance.Manager(28);}
                }
            }
            else
            {
                estSecoue = false;
                if (circleImage.transform.parent.gameObject.activeSelf)
                {
                    circleImage.transform.parent.gameObject.SetActive(false);
                }
                isMoving = false;
            }
        }
        else if (doesEntranceSecond)
        {
            transform.position += new Vector3(1, 0, 0) * 3 * Time.deltaTime;
            if (Vector3.Distance(transform.position, entrancePosition) > 9f)
            {
                doesEntranceSecond = false;
            }
        }
        else if (doesRenvoie)
        {
            transform.position += new Vector3(0, 1, 0) * 2 * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(-1, 0, 0) * 3 * Time.deltaTime;
            if (Vector3.Distance(transform.position, entrancePosition) > 7f)
            {
                doesEntrance = false;
            }
        }
    }

    void touchObject()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject != null && gameObject != null && hit.collider.gameObject == gameObject && hit.collider.gameObject.name == gameObject.name)
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

        angle = (Mathf.Atan2(circlePosition.y - circlePosition.y, circlePosition.x - circlePosition.x) - Mathf.Atan2(endPos.y - circlePosition.y, endPos.x - circlePosition.x)) * Mathf.Rad2Deg;
        if (angle < 0)
        {
            angle += 360;
        }
        return angle;
    }

    int GetItemFromAngle(float angle)
    {
        int itemNb = 0;

        itemNb = (int)((angle) / (360 / itemNumber));

        circleImage.transform.eulerAngles = new Vector3(0, 180, (360 / itemNumber) * itemNb);
        return itemNb;
    }

    void PickInventory(int nb)
    {
        switch (nb)
        {
            case 1:
                if (TutoManager.instance == null || TutoManager.instance.canJeter)
                {
                    Jeter();
                    TellSomething(1);
                }
                break;
            case 2:
                if (TutoManager.instance == null || TutoManager.instance.canVider)
                {
                    Vider();
                    TellSomething(2);
                }
                break;
            case 3:
                if (TutoManager.instance == null || TutoManager.instance.canOuvrirFermer)
                {
                    OuvrirFermer();
                    TellSomething(3);
                }
                break;
            case 0:
                if (TutoManager.instance == null || TutoManager.instance.canOpenTurnMenu)
                {
                    OpenTurnMenu();
                    TellSomething(5);
                }
                break;
        }
    }

    void TellSomething(int texte)
    {
        //Debug.Log(texte);
    }

    Sprite savedSprite;

    void OuvrirFermer()
    {
        if (TutoManager.instance != null) {TutoManager.instance.Manager(10);}
        colisScriptable.OuvrirFermer();
        if (colisScriptable.estOuvert)
        {
            Color newColo = GetComponent<SpriteRenderer>().color;
            newColo.a = 0.3f;
            spriteMaskArticleColis.transform.localScale = new Vector3(1,1.5f,1);
            GetComponent<SpriteRenderer>().color = newColo;
        }
        else if (!colisScriptable.estOuvert)
        {
            Color newColo = GetComponent<SpriteRenderer>().color;
            newColo.a = 1f;
            spriteMaskArticleColis.transform.localScale = new Vector3(1, 1f, 1);
            GetComponent<SpriteRenderer>().color = newColo;
        }

        if (colisScriptable.estOuvert)
        {
            savedSprite = GetComponent<SpriteRenderer>().sprite;
            Debug.Log("Test Ouvrir");
            GetComponent<SpriteRenderer>().sprite = colisScriptable.carton.cartonOuvert;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = savedSprite;
        }
    }


    void OpenTurnMenu()
    {
        if (TutoManager.instance != null) {TutoManager.instance.Manager(25);}
        if (!colisScriptable.estOuvert)
        {
            circleImage.transform.parent.gameObject.SetActive(false);
            tournerMenu.transform.position = transform.position;
            tournerMenu.SetActive(true);
            tournerMenuIsOpen = true;
        }
    }

    public void Tourner()
    {
        if(colisScriptable.isBadOriented && IWayEtiquette.activeSelf)
        {
            IWayEtiquette.SetActive(false);
        }
        else if(!colisScriptable.isBadOriented && !IWayEtiquette.activeSelf && colisScriptable.wayTicket != null)
        {
            IWayEtiquette.SetActive(true);
        }
    }

    void Vider()
    {
        if (colisScriptable.estOuvert)
        {
            colisScriptable.aEteVide = true;
            if(TutoManager.instance != null) {TutoManager.instance.Manager(11);}
            articleOnTableUn = spriteArticleTableUn.GetComponent<PileArticle>().listArticles;
            articleOnTableDeux = spriteArticleTableDeux.GetComponent<PileArticle>().listArticles;

            if (colisScriptable.listArticles.Count > 0)
            {
                if (spriteArticleTableUn.GetComponent<PileArticle>().listArticles.Count <= 0 || spriteArticleTableDeux.GetComponent<PileArticle>().listArticles.Count <= 0)
                {
                    List<Article> listTemporaire = colisScriptable.Vider();
                    int refBase = 0;
                    if (listTemporaire[0].rfid != null)
                    {
                        refBase = listTemporaire[0].rfid.refArticle.numeroRef;
                    }
                    List<Article> listTemporairePremiere = new List<Article>();
                    List<Article> listTemporaireSeconde = new List<Article>();
                    bool needSecond = false;
                    bool needOne = false;
                    foreach (Article art in listTemporaire)
                    {
                        if (spriteArticleTableUn.GetComponent<PileArticle>().listArticles.Count > 0 && spriteArticleTableDeux.GetComponent<PileArticle>().listArticles.Count <= 0)
                        {
                            needSecond = true;
                            listTemporairePremiere.Add(art);
                        }
                        else if (spriteArticleTableDeux.GetComponent<PileArticle>().listArticles.Count <= 0 && listTemporaire[0].rfid != null && art.rfid.refArticle.numeroRef != refBase)
                        {
                            needSecond = true;
                            listTemporaireSeconde.Add(art);
                        }
                        else if (spriteArticleTableUn.GetComponent<PileArticle>().listArticles.Count <= 0 || (listTemporaire[0].rfid != null && art.rfid.refArticle.numeroRef == refBase))
                        {
                            needOne = true;
                            listTemporairePremiere.Add(art);
                        }
                    }

                    //Table 1
                    if (needOne)
                    {
                        int nbRFIDFonctionnelTableUn = 0;
                        if (listTemporairePremiere.Count > 0 && articleOnTableUn.Count <= 0)
                        {
                            articleOnTableUn = listTemporairePremiere;
                            foreach (Article art in articleOnTableUn)
                            {
                                if (art.rfid != null && art.rfid.estFonctionnel)
                                {
                                    nbRFIDFonctionnelTableUn++;
                                }
                            }
                        }
                        if (articleOnTableUn.Count > 0)
                        {
                            spriteArticleTableUn.SetActive(true);
                            spriteArticleTableUn.GetComponent<PileArticle>().listArticles = articleOnTableUn;

                            if (colisScriptable.fillPercent <= 50)
                            {
                                spriteArticleTableUn.GetComponent<SpriteRenderer>().sprite = articleOnTableUn[0].spriteList[0];
                            }
                            else if (colisScriptable.fillPercent >= 125)
                            {
                                spriteArticleTableUn.GetComponent<SpriteRenderer>().sprite = articleOnTableUn[0].spriteList[1];
                            }
                            else
                            {
                                spriteArticleTableUn.GetComponent<SpriteRenderer>().sprite = articleOnTableUn[0].spriteList[2];
                            }
                            spriteArticleDansColis.sprite = null;
                        }
                    }
                    if (articleOnTableUn.Count > 0)
                    {
                        spriteArticleTableUn.SetActive(true);
                        spriteArticleTableUn.GetComponent<PileArticle>().listArticles = articleOnTableUn;
                        if (colisScriptable.fillPercent <= 50)
                        {
                            spriteArticleTableUn.GetComponent<SpriteRenderer>().sprite = articleOnTableUn[0].spriteList[0];
                        }
                        else if (colisScriptable.fillPercent >= 125)
                        {
                            spriteArticleTableUn.GetComponent<SpriteRenderer>().sprite = articleOnTableUn[0].spriteList[1];
                        }
                        else
                        {
                            spriteArticleTableUn.GetComponent<SpriteRenderer>().sprite = articleOnTableUn[0].spriteList[2];
                        }
                        spriteArticleDansColis.sprite = null;
                    }

                    //Table 2
                    if (needSecond)
                    {
                        int nbRFIDFonctionnelTableDeux = 0;
                        if (listTemporaireSeconde.Count > 0 && articleOnTableDeux.Count <= 0)
                        {
                            articleOnTableDeux = listTemporaireSeconde;
                            foreach (Article art in articleOnTableDeux)
                            {
                                if (art.rfid != null && art.rfid.estFonctionnel)
                                {
                                    nbRFIDFonctionnelTableDeux++;
                                }
                            }
                        }
                        if (articleOnTableDeux.Count > 0)
                        {
                            spriteArticleTableDeux.SetActive(true);
                            spriteArticleTableDeux.GetComponent<PileArticle>().listArticles = articleOnTableDeux;

                            if (colisScriptable.fillPercent <= 50)
                            {
                                spriteArticleTableUn.GetComponent<SpriteRenderer>().sprite = articleOnTableUn[0].spriteList[0];
                            }
                            else if (colisScriptable.fillPercent >= 125)
                            {
                                spriteArticleTableUn.GetComponent<SpriteRenderer>().sprite = articleOnTableUn[0].spriteList[1];
                            }
                            else
                            {
                                spriteArticleTableUn.GetComponent<SpriteRenderer>().sprite = articleOnTableUn[0].spriteList[2];
                            }
                            spriteArticleDansColis.sprite = null;
                        }
                    }
                    if (articleOnTableDeux.Count > 0)
                    {
                        spriteArticleTableDeux.SetActive(true);
                        spriteArticleTableDeux.GetComponent<PileArticle>().listArticles = articleOnTableDeux;

                        if (colisScriptable.fillPercent <= 50)
                        {
                            spriteArticleTableUn.GetComponent<SpriteRenderer>().sprite = articleOnTableUn[0].spriteList[0];
                        }
                        else if (colisScriptable.fillPercent >= 125)
                        {
                            spriteArticleTableUn.GetComponent<SpriteRenderer>().sprite = articleOnTableUn[0].spriteList[1];
                        }
                        else
                        {
                            spriteArticleTableUn.GetComponent<SpriteRenderer>().sprite = articleOnTableUn[0].spriteList[2];
                        }
                        spriteArticleDansColis.sprite = null;
                    }
                }
            }
        }
    }
    /*void Remplir()
    {
        articleOnTable = spriteArticleTable.GetComponent<PileArticle>().listArticles;
        if (articleOnTable.Count > 0)
        {
            colisScriptable.Remplir(articleOnTable.Count, articleOnTable);
            articleOnTable = new List<Article>();
            if (articleOnTable.Count <= 0)
            {
                spriteArticleTable.GetComponent<PileArticle>().listArticles = new List<Article>();
                spriteArticleTable.SetActive(false);
            }
            textArtcileTableRFID.text = "0";
            textArticleTableNombre.text = "0";
        }
    }*/

    void Jeter()
    {
        if(colisScriptable.PCB == 0)
        {
            Destroy(gameObject);
        }
    }
}
