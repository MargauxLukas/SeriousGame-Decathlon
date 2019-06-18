using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PileArticle : MonoBehaviour
{
    public List<Article> listArticles;
    public List<ColisScript> listColisPresent;
    private GameObject[] listeColisDispo;

    public Text textNbArticle;
    public Text textNbRFID;

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

    private float timeUpdate;

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

            if (doesTouch)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0;

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
            }

            if (touch.phase == TouchPhase.Ended)
            {
                doesTouch = false;
            }
        }
    }

    public void UpdatePileArticle()
    {
        listeColisDispo = GameObject.FindGameObjectsWithTag("Colis");
        listColisPresent = new List<ColisScript>();
        for(int i = 0; i < listeColisDispo.Length; i++)
        {
            if(listeColisDispo[i] != null && listeColisDispo[i].GetComponent<ColisScript>() != null)
            {
                listColisPresent.Add(listeColisDispo[i].GetComponent<ColisScript>());
            }
        }
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

    public void RemplirColis(Colis colisRemplir, ColisScript scriptColis)
    {
        colisRemplir.Remplir(listArticles.Count, listArticles);
        listArticles = new List<Article>();
        if(listArticles.Count <= 0)
        {
            gameObject.SetActive(false);
        }
        scriptColis.hasBeenScannedByRFID = false;
    }

    void touchObject()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
            if (hit.collider.gameObject != null && hit.collider.gameObject == gameObject)
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

    void PickInventory(int nb)
    {
        switch (nb)
        {
            case 1:
                if(listColisPresent[0] != null)
                {
                    RemplirColis(listColisPresent[0].colisScriptable, listColisPresent[0]);
                }
                break;
            case 2:
                if (listColisPresent[1] != null)
                {
                    RemplirColis(listColisPresent[1].colisScriptable, listColisPresent[1]);
                }
                break;
            case 3:
                if (listColisPresent[2] != null)
                {
                    RemplirColis(listColisPresent[2].colisScriptable, listColisPresent[2]);
                }
                break;
        }
    }

}
