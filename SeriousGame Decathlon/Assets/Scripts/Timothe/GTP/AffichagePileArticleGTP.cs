using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffichagePileArticleGTP : MonoBehaviour
{
    public Colis currentColis;
    private bool doesTouch;

    public GameObject tasArticle;

    public bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        //currentColis = Instantiate(currentColis);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount>0)
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
                    tasArticle.SetActive(true);
                    tasArticle.GetComponent<TasArticleGTP>().OpenTasArticle(currentColis.listArticles);
                }
                else
                {
                    isOpen = false;
                    tasArticle.SetActive(false);
                    currentColis.listArticles = tasArticle.GetComponent<TasArticleGTP>().CloseTasArticle();
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
