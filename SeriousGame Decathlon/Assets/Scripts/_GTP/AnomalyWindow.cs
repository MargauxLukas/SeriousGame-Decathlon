using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyWindow : MonoBehaviour
{
    public GameObject ecranCorrectQuantity;
    
    public void CorrectQuantityInSourceTU()
    {
        ecranCorrectQuantity.SetActive(true );
        gameObject          .SetActive(false);
    }

    public void RFIDScanningError()
    {
        gameObject.SetActive(false);
    }

    public void WrongProduct()
    {
        //Signalement Mauvais article 
        gameObject.SetActive(false);
    }

    public void CuttingDepth()
    {
        gameObject.SetActive(false);
    }

    public void Back()
    {
        gameObject .SetActive(false);
    }
}
