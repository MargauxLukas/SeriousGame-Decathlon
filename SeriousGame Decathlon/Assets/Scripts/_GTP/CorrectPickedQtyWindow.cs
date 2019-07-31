﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CorrectPickedQtyWindow : MonoBehaviour
{
    [Header("Texte en enfant")]
    public TextMeshProUGUI articleName;
    public TextMeshProUGUI quantitynb ;
    public TextMeshProUGUI expectednb ;

    [Header("EcranPickTUContent")]
    public GameObject ecranPickTUContent;

    public int nb = 0;
    private int nbBase;

    /********************************
    *   Affichage de la quantité    *
    *********************************/
    public void Affichage()
    {
        //Mettre a jour affichage
        quantitynb.text = (nbBase + nb).ToString();
    }

    /***************************************
    *   Affichage lorsque j'ouvre l'ecran  *
    ****************************************/
    public void AffichageStart(string name, int nbArticle)
    {
        nb = 0;
        nbBase = nbArticle;
        articleName.text = name;
        quantitynb.text = nbArticle.ToString();
        expectednb.text = nbArticle.ToString();
    }

    public void Moins()
    {
        nb--;
        Affichage();
    }

    public void Plus()
    {
        nb++;
        Affichage();
    }
    
    public void OK()
    {
        gameObject        .SetActive(false);
        ecranPickTUContent.GetComponent<PickTUContentWindow>().UpdatingArticle(nb);
    }
}

