using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemplissageColisGTP : MonoBehaviour
{
    public Colis colisScriptable;

    private void Start()
    {
        colisScriptable = Instantiate(colisScriptable);
    }

    public void AddArticle(Article articleToHad)
    {
        if(colisScriptable.listArticles == null)
        {
            colisScriptable.listArticles = new List<Article>();
        }
        colisScriptable.listArticles.Add(articleToHad);
    }
}
