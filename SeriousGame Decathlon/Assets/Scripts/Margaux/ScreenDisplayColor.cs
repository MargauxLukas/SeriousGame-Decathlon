using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenDisplayColor : MonoBehaviour
{
    public RecountTab recountTab;

    private SpriteRenderer screenColor;

    public GameObject lamp;
    private Animator lampAnimator;

    void Start()
    {
        screenColor = gameObject.GetComponent<SpriteRenderer>();
        screenColor.enabled = false;
        lampAnimator = lamp.GetComponent<Animator>();
        lampAnimator.SetBool("hasAnomalie", false);
    }

    void Update()
    {
        if (recountTab.colis != null)
        {
            screenColor.enabled = true;
            if (recountTab.colis.GetComponent<ColisScript>().hasBeenScannedByPistol && recountTab.colis.GetComponent<ColisScript>().colisScriptable.nbAnomalie > 0)
            {
                screenColor.color = new Color32(192, 30, 41, 255);
                lampAnimator.SetBool("hasAnomalie", true);
            }
            else if (recountTab.colis.GetComponent<ColisScript>().hasBeenScannedByPistol && recountTab.colis.GetComponent<ColisScript>().colisScriptable.nbAnomalie == 0)
            {
                screenColor.color = new Color32(30, 192, 112, 255);
                lampAnimator.SetBool("hasAnomalie", false);
            }
            else
            {
                screenColor.enabled = false;
            }
        }
        else
        {
            screenColor.enabled = false;
        }
    }
}
