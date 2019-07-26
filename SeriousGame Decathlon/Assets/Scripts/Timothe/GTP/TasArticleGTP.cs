using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasArticleGTP : MonoBehaviour
{
    public List<Article> articlesPresents;
    public List<GameObject> affichageTas;

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
                /*if (articlesPresents.Count > 0)
                {
                    GameObject nouvelArticle = Instantiate(articleUnit, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Quaternion.identity);
                    nouvelArticle.GetComponent<ArticleUnitGTP>().currentArticle = articlesPresents[articlesPresents.Count - 1];
                    articlesPresents.RemoveAt(articlesPresents.Count - 1);
                    nouvelArticle.GetComponent<ArticleUnitGTP>().tasParent = this;
                    nouvelArticle.GetComponent<ArticleUnitGTP>().doesTouch = true;
                    nouvelArticle.GetComponent<SpriteRenderer>().sprite = nouvelArticle.GetComponent<ArticleUnitGTP>().currentArticle.spriteGTP;
                }*/
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
        articlesPresents = new List<Article>(lesArticles);
        int nbMax = articlesPresents.Count;
        for (int j = 0; j < nbMax; j++)
        {
            //Debug.Log("Test ");
            GameObject nouvelArticle = Instantiate(articleUnit, transform.position+new Vector3(Random.Range(-0.5f,0.5f), Random.Range(-0.5f, 0.5f),0), Quaternion.identity);
            nouvelArticle.GetComponent<ArticleUnitGTP>().currentArticle = articlesPresents[articlesPresents.Count - 1];
            articlesPresents.RemoveAt(articlesPresents.Count - 1);
            nouvelArticle.GetComponent<ArticleUnitGTP>().tasParent = this;
            //nouvelArticle.GetComponent<ArticleUnitGTP>().doesTouch = true;
            nouvelArticle.GetComponent<SpriteRenderer>().sprite = nouvelArticle.GetComponent<ArticleUnitGTP>().currentArticle.spriteGTP;
            affichageTas.Add(nouvelArticle);
        }
    }

    public int ReturnNumberScanned()
    {
        int nbScanned = 0;
        foreach(GameObject gm in affichageTas)
        {
            if(gm.GetComponent<ArticleUnitGTP>().hasBeenScanned)
            {
                nbScanned++;
            }
        }
        return nbScanned;
    }

    public List<Article> CloseTasArticle()
    {
        List<Article> laNewListe = new List<Article>();

        foreach(GameObject gm in affichageTas)
        {
            laNewListe.Add(gm.GetComponent<ArticleUnitGTP>().currentArticle);
            Destroy(gm);
        }
        return laNewListe;
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
