using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenDisplayColor : MonoBehaviour
{
    public RecountTab recountTab;
    private SpriteRenderer screenColor;

    void Start()
    {
        screenColor = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (recountTab.colis != null)
        {
            if (recountTab.colis.GetComponent<ColisScript>().hasBeenScannedByPistol && recountTab.colis.GetComponent<ColisScript>().colisScriptable.nbAnomalie > 0)
            {
                screenColor.color = new Color32(192, 30, 41, 255);
            }
            else if (recountTab.colis.GetComponent<ColisScript>().hasBeenScannedByPistol && recountTab.colis.GetComponent<ColisScript>().colisScriptable.nbAnomalie == 0)
            {
                screenColor.color = new Color32(30, 192, 112, 255);
            }
            else
            {
                screenColor.color = new Color32(0, 198, 255, 255);
            }
        }
    }
}
