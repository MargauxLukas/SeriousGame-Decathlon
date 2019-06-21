using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculNombreArticle : MonoBehaviour
{
    public Text affichageNumber;

    public int currentNumber;

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
    }

    public void ValideAjout()
    {
        Debug.Log("Test");
        pileScript.RemplirColis(pileScript.listColisPresent[nbColisAffecte].colisScriptable, pileScript.listColisPresent[nbColisAffecte], currentNumber);
        currentNumber = 0;
        UpdateAffichage();
        gameObject.SetActive(false);
    }
}
