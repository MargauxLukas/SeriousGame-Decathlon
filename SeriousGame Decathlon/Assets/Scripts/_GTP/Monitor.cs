using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Monitor : MonoBehaviour
{
    public TextMeshProUGUI nbText;
    public int nb;

    public GameObject colis1;
    public GameObject colis2;
    public GameObject colis3;

    public GameObject colisAPrelever1;
    public GameObject colisAPrelever2;

    public void UpdateAffichage() //Soit on l'appelle de l'endroit ou on connait le nombre, soit on l'assigne ici et on le récupère
    {
        //Colis desactivé
        //change nb    
    }

    public void Colis1Actif() //Il faut savoir comment on définit quel colis sera actif
    {
        //Animator Colis1
    }

    public void Colis2Actif() //Il faut savoir comment on définit quel colis sera actif
    {
        //Animator Colis2
    }

    public void Colis3Actif() //Il faut savoir comment on définit quel colis sera actif
    {
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
