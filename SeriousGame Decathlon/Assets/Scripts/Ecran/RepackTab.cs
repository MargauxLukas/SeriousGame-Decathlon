using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RepackTab : MonoBehaviour
{
    [Header("Texte en enfant")]
    public TMPro.TextMeshProUGUI textCurrentQuantity1;
    public TMPro.TextMeshProUGUI textCurrentQuantity2;
    public TMPro.TextMeshProUGUI textPCB1;
    public TMPro.TextMeshProUGUI textPCB2;

    public FicheCarton ficheCarton;

    [Header("Colis assigné tout seul")]
    public GameObject colis;
    public GameObject colisVide;

    private Article art;
    private bool comptage= false;
    public void Update()
    {
        if (colis != null)
        {
            if (colis.GetComponent<ColisScript>().hasBeenScannedByPistol && !comptage)
            {
                textPCB1.text = colis.GetComponent<ColisScript>().colisScriptable.PCB.ToString();
                textCurrentQuantity1.text = colis.GetComponent<ColisScript>().colisScriptable.PCB.ToString();
                comptage = true;
            }
            if (colisVide != null)
            {
                textPCB2.text = colisVide.GetComponent<ColisScript>().colisScriptable.PCB.ToString();
                textCurrentQuantity2.text = colisVide.GetComponent<ColisScript>().colisScriptable.PCB.ToString();
                comptage = true;
            }
        }
    }

    public void Plus1()
    {
        if (colisVide != null && colis != null)
        {
            if (colisVide.GetComponent<ColisScript>().colisScriptable.PCB > 0)
            {
                art = colisVide.GetComponent<ColisScript>().colisScriptable.listArticles[colis.GetComponent<ColisScript>().colisScriptable.listArticles.Count - 1];

                colisVide.GetComponent<ColisScript>().colisScriptable.listArticles.RemoveAt(colis.GetComponent<ColisScript>().colisScriptable.listArticles.Count - 1);
                colis.GetComponent<ColisScript>().colisScriptable.listArticles.Add(art);

                colis.GetComponent<ColisScript>().colisScriptable.PCB++;
                colisVide.GetComponent<ColisScript>().colisScriptable.PCB--;

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
            if (colis.GetComponent<ColisScript>().colisScriptable.PCB > 0)
            {
                art = colis.GetComponent<ColisScript>().colisScriptable.listArticles[colis.GetComponent<ColisScript>().colisScriptable.listArticles.Count - 1];

                colis.GetComponent<ColisScript>().colisScriptable.listArticles.RemoveAt(colis.GetComponent<ColisScript>().colisScriptable.listArticles.Count - 1);
                Debug.Log(art);
                colisVide.GetComponent<ColisScript>().colisScriptable.listArticles.Add(art);

                colis.GetComponent<ColisScript>().colisScriptable.PCB--;
                colisVide.GetComponent<ColisScript>().colisScriptable.PCB++;

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
        ficheCarton.InstantiateCarton(codeCarton);
    }
}
