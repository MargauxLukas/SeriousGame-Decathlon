using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScreenDisplayColor : MonoBehaviour
{
    public RecountTab recountTab;
    public RepackTab repackTab;
    public AffichageAnomalie affichageAnomalie;

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
        if (recountTab.colis != null ||repackTab.colisVide != null)
        {
            if (recountTab.colis != null && recountTab.colis.GetComponent<ColisScript>().hasBeenScannedByPistol)
            {
                if (recountTab.colis.GetComponent<ColisScript>().colisScriptable.nbAnomalie > 0 && affichageAnomalie.toggleOnNb != recountTab.colis.GetComponent<ColisScript>().colisScriptable.nbAnomalie)
                {
                    Debug.Log("1");
                    textNBAnomalie.enabled = true;
                    textNBAnomalie.text = (recountTab.colis.GetComponent<ColisScript>().colisScriptable.nbAnomalie - affichageAnomalie.toggleOnNb).ToString();

                    lampAnimator.SetBool("hasAnomalie", true);
                    screenColorAnimator.SetBool("hasAnomalie", true);
                    screenColor.SetActive(true);
                }
                else if (recountTab.colis.GetComponent<ColisScript>().colisScriptable.nbAnomalie == 0 || affichageAnomalie.toggleOnNb == recountTab.colis.GetComponent<ColisScript>().colisScriptable.nbAnomalie)
                {
                    Debug.Log(recountTab.colis.GetComponent<ColisScript>().colisScriptable.nbAnomalie + " " + affichageAnomalie.toggleOnNb);
                    textNBAnomalie.enabled = false;

                    lampAnimator.SetBool("hasAnomalie", false);
                    screenColorAnimator.SetBool("hasAnomalie", false);
                    screenColor.SetActive(true);
                }
                else
                {
                    screenColor.SetActive(false);
                    //textNBAnomalie.enabled = false;
                }
            }
            else if (repackTab.colisVide != null)
            {
                if (repackTab.colisVide.GetComponent<ColisScript>().colisScriptable.nbAnomalie > 0 && affichageAnomalie.toggleOnNb != repackTab.colisVide.GetComponent<ColisScript>().colisScriptable.nbAnomalie)
                {
                    Debug.Log("3");
                    textNBAnomalie.enabled = true;
                    textNBAnomalie.text = (repackTab.colisVide.GetComponent<ColisScript>().colisScriptable.nbAnomalie - affichageAnomalie.toggleOnNb).ToString();

                    lampAnimator.SetBool("hasAnomalie", true);
                    screenColorAnimator.SetBool("hasAnomalie", true);
                    screenColor.SetActive(true);
                }
                else if (repackTab.colisVide.GetComponent<ColisScript>().colisScriptable.nbAnomalie == 0 || affichageAnomalie.toggleOnNb == repackTab.colisVide.GetComponent<ColisScript>().colisScriptable.nbAnomalie)
                {
                    Debug.Log("4");
                    textNBAnomalie.enabled = false;

                    lampAnimator.SetBool("hasAnomalie", false);
                    screenColorAnimator.SetBool("hasAnomalie", false);
                    screenColor.SetActive(true);
                }
                else
                {
                    screenColor.SetActive(false);
                    //textNBAnomalie.enabled = false;
                }
            }
            else
            {

            }
        }
        else
        {
            screenColor.SetActive(false);
            //textNBAnomalie.enabled = false;
        }
    }
}
