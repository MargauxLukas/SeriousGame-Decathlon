using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScreenDisplayColor : MonoBehaviour
{
    public RecountTab recountTab;

    public GameObject screenColor;
    private Animator screenColorAnimator;

    public GameObject lamp;
    private Animator lampAnimator;

    public TextMeshProUGUI textNBAnomalie;

    void Start()
    {
        screenColorAnimator = screenColor.GetComponent<Animator>();
        lampAnimator        = lamp       .GetComponent<Animator>();
        screenColorAnimator.SetBool("hasAnomalie", false);
        lampAnimator       .SetBool("hasAnomalie", false);

        //textNBAnomalie.enabled = false;
        textNBAnomalie.text = "";

        screenColor.SetActive(false);
    }

    void Update()
    {
        if (recountTab.colis != null)
        {
            screenColor.SetActive(true);
            if (recountTab.colis.GetComponent<ColisScript>().hasBeenScannedByPistol && recountTab.colis.GetComponent<ColisScript>().colisScriptable.nbAnomalie > 0)
            {
                textNBAnomalie.enabled = true;
                textNBAnomalie.text    = recountTab.colis.GetComponent<ColisScript>().colisScriptable.nbAnomalie.ToString();

                lampAnimator       .SetBool("hasAnomalie", true);
                screenColorAnimator.SetBool("hasAnomalie", true);
            }
            else if (recountTab.colis.GetComponent<ColisScript>().hasBeenScannedByPistol && recountTab.colis.GetComponent<ColisScript>().colisScriptable.nbAnomalie == 0)
            {
                textNBAnomalie.enabled = false;

                lampAnimator       .SetBool("hasAnomalie", false);
                screenColorAnimator.SetBool("hasAnomalie", false);
            }
            else
            {
                screenColor.SetActive(false);
                //textNBAnomalie.enabled = false;
            }
        }
        else
        {
            screenColor.SetActive(false);
            //textNBAnomalie.enabled = false;
        }
    }
}
