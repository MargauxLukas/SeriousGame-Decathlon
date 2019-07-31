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

    public List<GameObject> lumieresVertes;
    public List<GameObject> commandesInternets;

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

        lumieresVertes[0].SetActive(false);
        lumieresVertes[1].SetActive(false);
        lumieresVertes[2].SetActive(false);

        commandesInternets[0].SetActive(false);
        commandesInternets[1].SetActive(false);
        commandesInternets[2].SetActive(false);

        Tapis1.GetComponent<ColisLink>().console.IsActivate(false);
        Tapis2.GetComponent<ColisLink>().console.IsActivate(false);
        Tapis3.GetComponent<ColisLink>().console.IsActivate(false);
    }

    public void Colis1Actif(int phaseVoulue, int phaseActuelle, bool comeFromInternet)
    {
        Tapis1.GetComponent<ColisLink>().console.UpdateAffichage(         );
        Tapis1.GetComponent<ColisLink>().console.emplacementConsole = 0;
        Tapis1.GetComponent<ColisLink>().cm     .UpdateAffichage(nbMonitor);

        commandesInternets[0].SetActive(comeFromInternet);

        if (phaseActuelle + 1 == phaseVoulue)
        {
            colis1.GetComponent<Animator>().SetInteger("Color", 2);
        }
        else
        {
            colis1.GetComponent<Animator>().SetInteger("Color", 1);
        }

        lumieresVertes[0].SetActive(true);

        Tapis1.GetComponent<ColisLink>().console.IsActivate(true );
    }

    public void Colis2Actif(int phaseVoulue, int phaseActuelle, bool comeFromInternet)
    {
        Tapis2.GetComponent<ColisLink>().console.UpdateAffichage();
        Tapis2.GetComponent<ColisLink>().console.emplacementConsole = 1;
        Tapis2.GetComponent<ColisLink>().cm     .UpdateAffichage(nbMonitor);

        commandesInternets[1].SetActive(comeFromInternet);

        if (phaseActuelle + 1 == phaseVoulue)
        {
            colis2.GetComponent<Animator>().SetInteger("Color", 2);
        }
        else
        {
            colis2.GetComponent<Animator>().SetInteger("Color", 1);
        }

        lumieresVertes[1].SetActive(true);

        Tapis2.GetComponent<ColisLink>().console.IsActivate(true );
    }

    public void Colis3Actif(int phaseVoulue, int phaseActuelle, bool comeFromInternet)
    {
        Tapis3.GetComponent<ColisLink>().console.UpdateAffichage();
        Tapis3.GetComponent<ColisLink>().console.emplacementConsole = 2;
        Tapis3.GetComponent<ColisLink>().cm     .UpdateAffichage(nbMonitor);

        commandesInternets[2].SetActive(comeFromInternet);

        if (phaseActuelle + 1 == phaseVoulue)
        {
            colis3.GetComponent<Animator>().SetInteger("Color", 2);
        }
        else
        {
            colis3.GetComponent<Animator>().SetInteger("Color", 1);
        }

        lumieresVertes[2].SetActive(true);

        Tapis3.GetComponent<ColisLink>().console.IsActivate(true );
    }
}
