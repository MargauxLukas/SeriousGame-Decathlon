using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Monitor : MonoBehaviour
{
    public TextMeshProUGUI nbText;
    public int nbMonitor = 0;
    public int emplacement;

    public GameObject colis1;
    public GameObject colis2;
    public GameObject colis3;

    public GameObject Tapis1;
    public GameObject Tapis2;
    public GameObject Tapis3;

    public GameObject colisAPrelever1;
    public GameObject colisAPrelever2;

    public GameObject anomalyWindow;

    public void Start()
    {
        nbText.text = "";
    }

    public void UpdateAffichage(int nb)
    {
        nbMonitor = nb;
        nbText.text = nb.ToString();
    }

    public void ResetMonitor()
    {
        colis1.GetComponent<Animator>().SetInteger("Color", 0);
        colis2.GetComponent<Animator>().SetInteger("Color", 0);
        colis3.GetComponent<Animator>().SetInteger("Color", 0);
    }

    public void Colis1Actif(int phaseVoulue, int phaseActuelle) //Il faut savoir comment on définit quel colis sera actif
    {
        Tapis1.GetComponent<ColisLink>().console.UpdateAffichage(         );
        Tapis1.GetComponent<ColisLink>().console.emplacementConsole = 0;
        Tapis1.GetComponent<ColisLink>().cm     .UpdateAffichage(nbMonitor);

        Debug.Log((phaseActuelle+1) + " ==  " +phaseVoulue);
        if (phaseActuelle + 1 == phaseVoulue)
        {
            colis1.GetComponent<Animator>().SetInteger("Color", 2);
        }
        else
        {
            colis1.GetComponent<Animator>().SetInteger("Color", 1);
        }

        //Animator Colis1 activé SUR ECRAN
        //Animator Colis1 activé sur poste
    }

    public void Colis2Actif(int phaseVoulue, int phaseActuelle) //Il faut savoir comment on définit quel colis sera actif
    {
        Tapis2.GetComponent<ColisLink>().console.UpdateAffichage();
        Tapis1.GetComponent<ColisLink>().console.emplacementConsole = 1;
        Tapis2.GetComponent<ColisLink>().cm     .UpdateAffichage(nbMonitor);

        if (phaseActuelle + 1 == phaseVoulue)
        {
            colis2.GetComponent<Animator>().SetInteger("Color", 2);
        }
        else
        {
            colis2.GetComponent<Animator>().SetInteger("Color", 1);
        }
        //Animator Colis2
    }

    public void Colis3Actif(int phaseVoulue, int phaseActuelle) //Il faut savoir comment on définit quel colis sera actif
    {
        Tapis3.GetComponent<ColisLink>().console.UpdateAffichage();
        Tapis1.GetComponent<ColisLink>().console.emplacementConsole = 2;
        Tapis3.GetComponent<ColisLink>().cm     .UpdateAffichage(nbMonitor);

        Debug.Log((phaseActuelle + 1) + " ==  " + phaseVoulue);
        if (phaseActuelle + 1 == phaseVoulue)
        {
            colis3.GetComponent<Animator>().SetInteger("Color", 2);
        }
        else
        {
            colis3.GetComponent<Animator>().SetInteger("Color", 1);
        }
        //Animator Colis3
    }

    public void UpdateColisAPrelever()
    {
        /*if(colisAPrelever1.isActiver)
        {
            Animator.getBool()
        }
        if (colisAPrelever2.isActiver)
        {
            Animator.GetBool()
        }*/
    }
}
