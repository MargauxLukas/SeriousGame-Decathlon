using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class affichageAmouleRecep : MonoBehaviour
{
    public Animator anime;

    public int alertLevel;

    public void Activate()
    {
        switch(alertLevel)
        {
            case 0:
                //Animation Orange clignotant
                break;
            case 1:
                //Animation Orange
                break;
            case 2:
                //Animation Rouge
                break;
            case 3:
                //Animation null
                break;
        }
    }

    public void Desactivate()
    {
        //Animation Verte;
    }

}
