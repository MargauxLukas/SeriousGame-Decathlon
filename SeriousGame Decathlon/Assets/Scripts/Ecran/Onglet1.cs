using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onglet1 : MonoBehaviour
{
    public OngletManager om;

    /******************************
    * Ouvre l'onglet FillingRate  *
    *******************************/
    private void OnMouseDown()
    {
        om.FillingRateOpen();
    }
}
