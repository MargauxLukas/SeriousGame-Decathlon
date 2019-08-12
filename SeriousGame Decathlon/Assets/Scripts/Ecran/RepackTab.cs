using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class RepackTab : MonoBehaviour
{
    [Header("Texte en enfant")]
    public TextMeshProUGUI textCurrentQuantity1;
    public TextMeshProUGUI textCurrentQuantity2;
    public TextMeshProUGUI textPCB1;
    public TextMeshProUGUI textPCB2;
    public TextMeshProUGUI textRef2;

    [Header("Colis assigné tout seul")]
    public GameObject colis;
    public GameObject colisVide;

    [Header("A assigner")]
    public FicheCarton ficheCarton;
    public RecountTab recountTab;

    private Article art;
    private bool comptage= false;

    public GameObjectsManagerMulti gameObjectsManager;

    public void Update()
    {
        if (colis != null)
        {
            if (!comptage)
            {
                if (colis.GetComponent<ColisScript>().hasBeenScannedByPistol)
                {
                    textPCB1.text = colis.GetComponent<ColisScript>().colisScriptable.PCB.ToString();
                    textCurrentQuantity1.text = colis.GetComponent<ColisScript>().colisScriptable.PCB.ToString();
                }
                if (colisVide != null)
                {
                    textPCB2.text = colisVide.GetComponent<ColisScript>().colisScriptable.PCB.ToString();
                    textCurrentQuantity2.text = colisVide.GetComponent<ColisScript>().colisScriptable.PCB.ToString();
                }
                comptage = true;

                if (TutoManagerMulti.instance != null && textCurrentQuantity1.text == gameObjectsManager.quantity1 && textCurrentQuantity2.text == gameObjectsManager.quantity2)
                {
                    Debug.Log("Repack quantity is good");
                    TutoManagerMulti.instance.Manager(41);
                }
            }
        }
    }

    public void Plus1()
    {
        if (colisVide != null && colis != null)
        {
            if (colisVide.GetComponent<ColisScript>().colisScriptable.PCB >= 0)
            {
                art = colisVide.GetComponent<ColisScript>().colisScriptable.listArticles[colisVide.GetComponent<ColisScript>().colisScriptable.listArticles.Count - 1];

                colis.GetComponent<ColisScript>().spriteArticleDansColis.sprite = art.spriteList[1];
                colisVide.GetComponent<ColisScript>().colisScriptable.listArticles.RemoveAt(colisVide.GetComponent<ColisScript>().colisScriptable.listArticles.Count - 1);
                colis    .GetComponent<ColisScript>().colisScriptable.listArticles.Add(art);
                
                colis    .GetComponent<ColisScript>().colisScriptable.PCB++;
                colisVide.GetComponent<ColisScript>().colisScriptable.PCB--;

                colis    .GetComponent<ColisScript>().colisScriptable.UpdateWeight();
                colisVide.GetComponent<ColisScript>().colisScriptable.UpdateWeight();

                comptage = false;
            }

            if (colisVide.GetComponent<ColisScript>().colisScriptable.PCB <= 0)
            {
                colisVide.GetComponent<ColisScript>().spriteArticleDansColis.sprite = null;
            }
        }
    }

    public void Plus2()
    {
        Moins1();
    }

    public void Moins1()
    {
        if (TutoManagerMulti.instance != null) {TutoManagerMulti.instance.Manager(42);}

        if (colis != null && colisVide != null)
        {
            if (colis.GetComponent<ColisScript>().colisScriptable.PCB >= 0)
            {
                art = colis.GetComponent<ColisScript>().colisScriptable.listArticles[colis.GetComponent<ColisScript>().colisScriptable.listArticles.Count-1];

                colisVide.GetComponent<ColisScript>().spriteArticleDansColis.sprite = colis.GetComponent<ColisScript>().colisScriptable.listArticles[colis.GetComponent<ColisScript>().colisScriptable.listArticles.Count - 1].spriteList[1];

                colis.GetComponent<ColisScript>().colisScriptable.listArticles.RemoveAt(colis.GetComponent<ColisScript>().colisScriptable.listArticles.Count - 1);
                colisVide.GetComponent<ColisScript>().colisScriptable.listArticles.Add(art);
                colis    .GetComponent<ColisScript>().colisScriptable.PCB--;
                colisVide.GetComponent<ColisScript>().colisScriptable.PCB++;

                colis    .GetComponent<ColisScript>().colisScriptable.UpdateWeight();
                colisVide.GetComponent<ColisScript>().colisScriptable.UpdateWeight();

                comptage = false;
            }

            if(colis.GetComponent<ColisScript>().colisScriptable.PCB <= 0)
            {
                colis.GetComponent<ColisScript>().spriteArticleDansColis.sprite = null;
            }
        }
    }

    public void Moins2()
    {
        Plus1();
    }

    public void NouveauColis()
    {
        if (TutoManagerMulti.instance != null) {TutoManagerMulti.instance.Manager(40);}

        textRef2.text = "7357";
        string codeCarton = colis.GetComponent<ColisScript>().colisScriptable.carton.codeRef;
        Debug.Log(codeCarton);
        if (codeCarton.Equals("CB01") || codeCarton.Equals("CB02"))
        {
            ficheCarton.InstantiateCarton(codeCarton);
        }
        else
        {
            codeCarton = "CB01";
            ficheCarton.InstantiateCarton(codeCarton);
        }
    }

    public void print1()
    {
        int pcb = colis.GetComponent<ColisScript>().colisScriptable.PCB;
        int refArticle = colis.GetComponent<ColisScript>().colisScriptable.listArticles[0].rfid.refArticle.numeroRef;
        float poids = colis.GetComponent<ColisScript>().colisScriptable.poids;

        recountTab.PrintHU(pcb, refArticle, poids);
    }

    public void print2()
    {
        if (TutoManagerMulti.instance != null) {TutoManagerMulti.instance.Manager(43);}
        int pcb = colisVide.GetComponent<ColisScript>().colisScriptable.PCB;
        int refArticle = colisVide.GetComponent<ColisScript>().colisScriptable.listArticles[0].rfid.refArticle.numeroRef;
        float poids = colisVide.GetComponent<ColisScript>().colisScriptable.poids;

        recountTab.PrintHU(pcb, refArticle, poids);
    }

    public void Reset()
    {
        textRef2.text = "";
        textCurrentQuantity2.text = "0";
        textCurrentQuantity1.text = "0";
        textPCB1.text = "0";
        textPCB2.text = "0";
    }
}
