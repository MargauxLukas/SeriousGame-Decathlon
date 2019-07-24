using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculNombreArticle : MonoBehaviour
{
    public Text affichageNumber;

    public GameObject tutoMan;

    public int currentNumber = 0;

    public PileArticle pileScript;
    public int nbColisAffecte;

    public void AddArt()
    {
        if (currentNumber < pileScript.listArticles.Count)
        {
            currentNumber++;
            UpdateAffichage();
        }
    }

    public void RemoveArt()
    {
        if(currentNumber>0)
        {
            currentNumber--;
            UpdateAffichage();
        }
    }

    public void UpdateAffichage()
    {
        affichageNumber.text = currentNumber.ToString();
        if(TutoManagerMulti.instance != null && affichageNumber.text == tutoMan.GetComponent<TutoManagerMulti>().articlesNum)
        {
            TutoManagerMulti.instance.Manager(46);
        }
    }

    public void ValideAjout()
    {
        if (TutoManagerMulti.instance != null) {TutoManagerMulti.instance.Manager(47);}
        pileScript.RemplirColis(pileScript.listColisPresent[nbColisAffecte].colisScriptable, pileScript.listColisPresent[nbColisAffecte], currentNumber);
        currentNumber = 0;
        UpdateAffichage();
        gameObject.SetActive(false);
    }
}
