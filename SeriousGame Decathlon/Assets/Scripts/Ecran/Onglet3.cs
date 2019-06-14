using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onglet3 : MonoBehaviour
{
    public OngletManager om;

    /************************
    * Ouvre l'onglet Repack *
    *************************/
    private void OnMouseDown()
    {
        om.RepackOpen();
    }
}
