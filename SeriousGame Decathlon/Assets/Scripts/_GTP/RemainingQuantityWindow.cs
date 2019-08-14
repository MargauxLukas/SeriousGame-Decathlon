using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemainingQuantityWindow : MonoBehaviour
{
    public TextMeshProUGUI remainingQuantitynb;
    public TextMeshProUGUI tunb;
    public TextMeshProUGUI expectednb;

    public ManagerColisVider mcv;

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
        if(mcv.emplacementsScripts[mcv.emplacement].GetComponent<AffichagePileArticleGTP>().isFulledWithPack != 0)
        {
            articleNb = articleNb * 3;
        }
        if(nb == articleNb)
        {
            //Bien
        }
        else
        {
            Scoring.instance.LosePointGTP(50, "Tu n'as pas donner le bon nombre d'article restant dans le SHU");
        }

        mcv.emplacementsScripts[mcv.emplacement].GetComponent<AffichagePileArticleGTP>().isSupposedToBeEmpty = false;

        nb = 0;
        Affichage();
        gameObject.SetActive(false);
    }
}
