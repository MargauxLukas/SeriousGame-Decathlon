using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**********************************************
 *   Je ne traite que les boutons par ici     *
 **********************************************/
public class ConvoyeurButton : MonoBehaviour
{
    public ConvoyeurManager convoyeur;

    private bool UpPressed       = false;
    private bool downPressed     = false;
    private bool replierPressed  = false;
    private bool deplierPressed  = false;
    private bool validatePressed = false;

    private bool isReturnContener = false;

    public bool isCollide         = false;

    public GameObject  isOnAmpoule;
    public GameObject isOffAmpoule;

    public DetectionAnomalieRecep detectAnom;
    public ChangementEtiquettes etiquettesManager;

    private void Update()
    {
        if (convoyeur.isOn)
        {
            if (UpPressed) { convoyeur.MoveZ("up"); }                       //Boutton Monter
            if (downPressed) { convoyeur.MoveZ("down"); }                       //Boutton Descendre
            if (!isCollide)
            {
                if (deplierPressed) { convoyeur.MoveY("deplier"); }                       //Boutton Deplier
            }
            if (replierPressed && validatePressed) { convoyeur.MoveY("replier"); }    //Boutton Replier + Validate
            else { return; }
        }
        else
        {
            return;
        }
    }

    public void OnOrOff()
    {
        //Verification si convoyeur est allumé ou pas sinon ça bug lorsque j'appuie sur Replier/Deplier
        if (convoyeur.isOn)
        {
            isOffAmpoule.SetActive(true );
            isOnAmpoule .SetActive(false);
            convoyeur.isOn = false;
        }
        else if (!detectAnom.gotAnomalie && etiquettesManager.nbEtiquettes > 0)
        {
            isOnAmpoule .SetActive(true );
            isOffAmpoule.SetActive(false);
            convoyeur.isOn = true;
        }        
    }

    public void ValidationPointerDown()
    {
        validatePressed = true;
    }

    public void ValidationPointerUp()
    {
        validatePressed = false;
    }

    public void UpPointerDown()
    {
        UpPressed = true;
    }

    public void UpPointerUp()
    {
        UpPressed = false;
        convoyeur.SetFloor();
    }

    public void DownPointerDown()
    {
        downPressed = true;
    }

    public void DownPointerUp()
    {
        downPressed = false;
        convoyeur.SetFloor();
    }

    public void DeplierPointerDown()
    {
        deplierPressed = true;
    }

    public void DeplierPointerUp()
    {
        deplierPressed = false;
        convoyeur.PlayerNotMove();
    }

    public void RepliePointerDown()
    {
        replierPressed = true;
    }

    public void RepliePointerUp()
    {
        replierPressed = false;
        convoyeur.PlayerNotMove();
    }


    /*****************************
     *    Renvois contener       *
     *****************************/
    public void RenvoisContener()
    {
        if(convoyeur.isReplierMax /*&& isReturnContener.isDefectueux*/)
        {
            //Contener renvoyé
            isReturnContener = true;
            //Quitter niveau
        }
    }
}

