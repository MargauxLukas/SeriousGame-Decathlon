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

    [Header("Colis assigné tout seul")]
    public GameObject colis;
    public GameObject colisVide;

    [Header("A assigner")]
    public FicheCarton ficheCarton;
    public RecountTab recountTab;

    private Article art;
    private bool comptage= false;
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

                colisVide.GetComponent<ColisScript>().colisScriptable.listArticles.RemoveAt(colisVide.GetComponent<ColisScript>().colisScriptable.listArticles.Count - 1);
                colis    .GetComponent<ColisScript>().colisScriptable.listArticles.Add(art);

                colis    .GetComponent<ColisScript>().colisScriptable.PCB++;
                colisVide.GetComponent<ColisScript>().colisScriptable.PCB--;

                colis    .GetComponent<ColisScript>().colisScriptable.UpdateWeight();
                colisVide.GetComponent<ColisScript>().colisScriptable.UpdateWeight();

                comptage = false;
            }
        }
    }

    public void Plus2()
    {
        Moins1();
    }

    public void Moins1()
    {
        if (colis != null && colisVide != null)
        {
            if (colis.GetComponent<ColisScript>().colisScriptable.PCB >= 0)
            {
                art = colis.GetComponent<ColisScript>().colisScriptable.listArticles[colis.GetComponent<ColisScript>().colisScriptable.listArticles.Count-1];

                colis    .GetComponent<ColisScript>().colisScriptable.listArticles.RemoveAt(colis.GetComponent<ColisScript>().colisScriptable.listArticles.Count - 1);
                colisVide.GetComponent<ColisScript>().colisScriptable.listArticles.Add(art);
                colis    .GetComponent<ColisScript>().colisScriptable.PCB--;
                colisVide.GetComponent<ColisScript>().colisScriptable.PCB++;

                colis    .GetComponent<ColisScript>().colisScriptable.UpdateWeight();
                colisVide.GetComponent<ColisScript>().colisScriptable.UpdateWeight();

                comptage = false;
            }
        }
    }

    public void Moins2()
    {
        Plus1();
    }

    public void NouveauColis()
    {
        string codeCarton = colis.GetComponent<ColisScript>().colisScriptable.carton.codeRef;
        Debug.Log(codeCarton);
        ficheCarton.InstantiateCarton(codeCarton);
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
        int pcb = colisVide.GetComponent<ColisScript>().colisScriptable.PCB;
        int refArticle = colisVide.GetComponent<ColisScript>().colisScriptable.listArticles[0].rfid.refArticle.numeroRef;
        float poids = colisVide.GetComponent<ColisScript>().colisScriptable.poids;

        recountTab.PrintHU(pcb, refArticle, poids);
    }
}
