using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortieEntrepôtBouton : MonoBehaviour
{
    public void SortEntrepot()
    {
        if(ChargementListeColis.instance != null)
        {
            ChargementListeColis.instance.SortEntrepot();
        }
    }
}
