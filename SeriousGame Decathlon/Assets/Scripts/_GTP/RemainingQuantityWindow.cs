using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemainingQuantityWindow : MonoBehaviour
{
    public TextMeshProUGUI remainingQuantitynb;
    public TextMeshProUGUI tunb;
    public TextMeshProUGUI expectednb;

    public int articleNb;
    public int nb = 0;

    public void Affichage()
    {
        //Mettre a jour affichage
        remainingQuantitynb.text = nb.ToString();
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
        if(nb == articleNb)
        {
            //Bien
        }
        else
        {
            //Pas bien
        }

        gameObject.SetActive(false);
    }
}
