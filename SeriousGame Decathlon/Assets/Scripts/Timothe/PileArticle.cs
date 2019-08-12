using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PileArticle : MonoBehaviour
{
    public List<Article> listArticles;
    public List<ColisScript> listColisPresent;
    public GameObject[] listeColisDispo;

    private bool doesTouch;

    public Image circleImage;
    private Vector2 startPosition;
    private Vector2 circlePosition;
    public int itemNumber;
    private int currentItem;
    public bool menuIsOpen;
    private bool menuCanOpen = true;
    public float timeBeforeOpen;
    private float timeTouched;

    public GameObject canvasNombre;
    public GameObject canvasInfo;
    public Text textNbArticle;
    public Text textNbRFID;
    public Text textRefRFID;

    bool menuInfoIsOpen;

    private float timeUpdate;

    public CalculNombreArticle calculArt;

    private void Start()
    {
        StartCoroutine(ResetPile());
    }

    private void Update()
    {
        if(timeUpdate<=0.5f)
        {
            timeUpdate++;
        }
        else
        {
            UpdatePileArticle();
            timeUpdate = 0;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchObject();

            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            if(menuInfoIsOpen && ((TutoManagerMulti.instance == null) || (TutoManagerMulti.instance != null && TutoManagerMulti.instance.canCloseFicheInfo)))
            {
                menuInfoIsOpen = false;
                canvasInfo.SetActive(false);
                if (TutoManagerMulti.instance != null) {TutoManagerMulti.instance.Manager(14);}
            }

            if (doesTouch)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    currentItem = -1;
                    timeTouched = 0;
                    startPosition = touchPosition;
                    circlePosition = transform.position;
                }
                else if (Vector3.Distance(startPosition, touchPosition) > 1f && timeTouched < timeBeforeOpen)
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

                if (timeTouched > timeBeforeOpen && menuCanOpen)
                {
                    menuIsOpen = true;
                    if(TutoManagerMulti.instance != null) {TutoManagerMulti.instance.Manager(12);}
                    circleImage.transform.parent.gameObject.SetActive(true);
                    circleImage.transform.parent.gameObject.transform.position = transform.position;
                    circleImage.fillAmount = 1f / itemNumber;

                    if (touch.phase == TouchPhase.Moved)
                    {
                        if (Vector2.Distance(startPosition, touchPosition) > 1f)
                        {
                            currentItem = GetItemFromAngle(GetAngle(startPosition, touchPosition));
                            PickInventorySecond(currentItem);
                        }
                        else
                        {
                            if (listColisPresent.Count-1 >= 1)
                            {
                                listColisPresent[1].spriteSelection.SetActive(false);
                            }
                            listColisPresent[0].spriteSelection.SetActive(false);
                            currentItem = -1;
                        }
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        if (listColisPresent.Count - 1 >= 1)
                        {
                            listColisPresent[1].spriteSelection.SetActive(false);
                        }
                        listColisPresent[0].spriteSelection.SetActive(false);
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
            }
        }
        else
        {
            doesTouch = false;
        }
    }

    public void UpdatePileArticle()
    {
        listeColisDispo = GameObject.FindGameObjectsWithTag("Colis");
        listColisPresent = new List<ColisScript>();
        for (int i = 0; i < listeColisDispo.Length; i++)
        {
            if (listeColisDispo[i] != null && listeColisDispo[i].GetComponent<ColisScript>() != null)
            {
                listColisPresent.Add(listeColisDispo[i].GetComponent<ColisScript>());
            }
        }

        if (TutoManagerMulti.instance != null)
        {
            Debug.Log("------0 :" + listColisPresent[0].name + " ------1 :" + listColisPresent[1].name);

            if (listColisPresent.Count > 2)
            {
                if (listColisPresent[1].name.Equals("TutoColis3") && listColisPresent[2].name.Equals("TutoColis2"))
                {
                    ColisScript temporaire = listColisPresent[2];
                    listColisPresent[2] = listColisPresent[1];
                    listColisPresent[1] = temporaire;
                }
                if (listColisPresent[0].name.Equals("TutoColis3") && listColisPresent[1].name.Equals("TutoColis2"))
                {
                    ColisScript temporaire = listColisPresent[1];
                    listColisPresent[1] = listColisPresent[0];
                    listColisPresent[0] = temporaire;
                }
                if (listColisPresent[0].name.Equals("TutoColis3") && listColisPresent[2].name.Equals("TutoColis2Vide"))
                {
                    ColisScript temporaire = listColisPresent[2];
                    listColisPresent[2] = listColisPresent[0];
                    listColisPresent[0] = temporaire;
                }
            }

            foreach (ColisScript colis in listColisPresent)
            {
                //Debug.Log("------" + colis.colisScriptable.name);
            }
        }
    }

    IEnumerator ResetPile()
    {
        yield return new WaitForSeconds(2f);
        UpdatePileArticle();
        StartCoroutine(ResetPile());
    }

    public void ChangeRFID(RFID refid)
    {
        foreach(Article art in listArticles)
        {
            art.rfid = refid;
        }
    }

    public void AddArticle(List<Article> newList)
    {
        listArticles = newList;
    }

    //A mettre sur le bouton de validation du nombre
    public void RemplirColis(Colis colisRemplir, ColisScript scriptColis, int nb)
    {
        List<Article> newArticleList = new List<Article>();
        for(int i = nb-1; i >= 0; i--)
        {
            if (i < listArticles.Count)
            {
                Article articleToHad = Article.CreateInstance<Article>();
                articleToHad = listArticles[i];
                newArticleList.Add(articleToHad);
                listArticles.RemoveAt(i);
            }
        }
        colisRemplir.Remplir(newArticleList);
        //listArticles = new List<Article>();
        if(listArticles.Count <= 0)
        {
            gameObject.SetActive(false);
        }
        scriptColis.hasBeenScannedByRFID = false;
        scriptColis.spriteArticleDansColis.sprite = GetComponent<SpriteRenderer>().sprite;
    }

    void touchObject()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
            if (hit.collider != null && hit.collider != null && hit.collider.gameObject != null && gameObject != null && hit.collider.gameObject == gameObject)
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

    public void ChoiceNumberColis(Colis colisRemplir, ColisScript scriptColis)
    {
        canvasNombre.SetActive(true);
    }

    public void ShowInfo()
    {
        canvasInfo.SetActive(true);
        if(TutoManagerMulti.instance != null) {TutoManagerMulti.instance.Manager(13);}
        textNbArticle.text = listArticles.Count.ToString();
        int nbRFID = 0;
        foreach (Article art in listArticles)
        {
            if (art.rfid != null)
            {
                if (art.rfid.estFonctionnel)
                {
                    nbRFID++;
                }
            }
        }
        textNbRFID.text = nbRFID.ToString();
        if (listArticles[0].rfid != null)
        {
            textRefRFID.text = listArticles[0].rfid.refArticle.numeroRef.ToString();
        }
        menuInfoIsOpen = true;
    }

    void PickInventory(int nb)
    {
        switch (nb)
        {
            case 1:
                if ((TutoManagerMulti.instance == null || TutoManagerMulti.instance.canColis2) && listColisPresent[1] != null)
                {
                    calculArt.nbColisAffecte = 1;
                    ChoiceNumberColis(listColisPresent[1].colisScriptable, listColisPresent[1]);
                }
                break;
            case 2:
                if ((TutoManagerMulti.instance == null || TutoManagerMulti.instance.canColis1) && listColisPresent[0] != null)
                {
                    calculArt.nbColisAffecte = 0;
                    ChoiceNumberColis(listColisPresent[0].colisScriptable, listColisPresent[0]);
                    if (TutoManagerMulti.instance != null) { TutoManagerMulti.instance.Manager(15); }
                }
                break;
            case 0:
                if (TutoManagerMulti.instance == null || TutoManagerMulti.instance.canInfo)
                {
                    ShowInfo();
                }
                break;
        }
    }

    void PickInventorySecond(int nb)
    {
        switch (nb)
        {
            case 1:
                if (listColisPresent.Count - 1 >= 1)
                {
                    listColisPresent[1].spriteSelection.SetActive(true);
                }
                listColisPresent[0].spriteSelection.SetActive(false);
                break;
            case 2:
                listColisPresent[0].spriteSelection.SetActive(true);
                if (listColisPresent.Count - 1 >= 1)
                {
                    listColisPresent[1].spriteSelection.SetActive(false);
                }
                break;
            default:
                if (listColisPresent.Count-1 >= 1)
                {
                    listColisPresent[1].spriteSelection.SetActive(false);
                }
                listColisPresent[0].spriteSelection.SetActive(false);
                break;
        }
    }

}
