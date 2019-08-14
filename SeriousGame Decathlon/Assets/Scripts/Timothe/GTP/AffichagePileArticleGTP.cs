using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffichagePileArticleGTP : MonoBehaviour
{
    public Colis currentColis;
    private bool doesTouch;

    public GameObject tasArticle;

    public bool isOpen;
    public bool isSupposedToBeEmpty;
    public int isFulledWithPack;
    public ManagerColisAttendu mca;

    public Article artReference;

    private int nbAddedArticle = 0;

    void Start()
    {
        //currentColis = Instantiate(currentColis);
    }

    void Update()
    {
        if(Input.touchCount>0 && !mca.isLevelEnded)
        {
            touchObject();

            if(Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                doesTouch = false;
            }

            if(doesTouch)
            {
                if (!tasArticle.activeSelf && currentColis.listArticles != null && currentColis.listArticles.Count > 0)
                {
                    isOpen = true;
                    GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.4f);
                    tasArticle.SetActive(true);
                    StartCoroutine(tasArticle.GetComponent<TasArticleGTP>().ApparitionArticle(currentColis.listArticles, isFulledWithPack, null));
                    if(TutoManagerGTP.instance != null) { TutoManagerGTP.instance.Manager(5); }
                }
                else
                {
                    isOpen = false;
                    GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
                    tasArticle.SetActive(false);
                    currentColis.listArticles = tasArticle.GetComponent<TasArticleGTP>().CloseTasArticle();
                    if (TutoManagerGTP.instance != null) { TutoManagerGTP.instance.Manager(7); }
                }
                doesTouch = false;
            }
        }
    }

    public void AddArticle(Article articleToHad)
    {
        if (currentColis.listArticles == null)
        {
            currentColis.listArticles = new List<Article>();
        }
        currentColis.listArticles.Add(articleToHad);
    }

    void touchObject()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject != null && gameObject != null && hit.collider.gameObject == gameObject && hit.collider.gameObject.name == gameObject.name)
            {
                doesTouch = true;
            }
        }
    }
}
