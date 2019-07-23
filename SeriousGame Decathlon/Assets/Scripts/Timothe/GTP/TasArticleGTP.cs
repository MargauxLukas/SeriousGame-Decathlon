using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasArticleGTP : MonoBehaviour
{
    public List<Article> articlesPresents;
    private List<GameObject> affichageTas;

    public GameObject prefabAffichageArticlePiece;
    public GameObject articleUnit;

    private bool doesTouch;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchObject();
            }
            if (doesTouch)
            {
                GameObject nouvelArticle = Instantiate(articleUnit, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Quaternion.identity);
                nouvelArticle.GetComponent<ArticleUnitGTP>().currentArticle = articlesPresents[articlesPresents.Count - 1];
                articlesPresents.RemoveAt(articlesPresents.Count - 1);
                nouvelArticle.GetComponent<ArticleUnitGTP>().tasParent = this;
                nouvelArticle.GetComponent<ArticleUnitGTP>().doesTouch = true;
                nouvelArticle.GetComponent<SpriteRenderer>().sprite = nouvelArticle.GetComponent<ArticleUnitGTP>().currentArticle.spriteGTP;
                doesTouch = false;
            }
        }
    }

    public void LetArticleFall(Article articleToHad)
    {
        articlesPresents.Add(articleToHad);
        affichageTas.Add(Instantiate(prefabAffichageArticlePiece, transform, false));
        affichageTas[articlesPresents.Count-1].GetComponent<SpriteRenderer>().sprite = articlesPresents[0].spriteGTP;
        affichageTas[articlesPresents.Count-1].transform.localPosition += new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
    }

    public void OpenTasArticle(List<Article> lesArticles)
    {
        if (affichageTas != null)
        {
            for (int i = 0; i < affichageTas.Count; i++)
            {
                Destroy(affichageTas[i]);
            }
        }

        affichageTas = new List<GameObject>();
        articlesPresents = lesArticles;
        for(int j = 0; j < articlesPresents.Count; j++)
        {
            Debug.Log("Test ");
            affichageTas.Add(Instantiate(prefabAffichageArticlePiece, transform, false));
            affichageTas[j].GetComponent<SpriteRenderer>().sprite = articlesPresents[0].spriteGTP;
            affichageTas[j].transform.localPosition += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f,1f), 0);
        }
    }

    public List<Article> CloseTasArticle()
    {
        return articlesPresents;
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
