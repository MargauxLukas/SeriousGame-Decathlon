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

    public AudioSource source;

    private void Update()
    {
        if (convoyeur.isOn)
        {
            if (UpPressed)
            {
                if (TutoManagerRecep.instance != null) { TutoManagerRecep.instance.Manager(5); }
                convoyeur.MoveZ("up");                                          //Boutton Monter
            }

            if (downPressed)
            {
                if (TutoManagerRecep.instance != null) { TutoManagerRecep.instance.Manager(11); }
                convoyeur.MoveZ("down");                                        //Boutton Descendre
            }

            if ((TutoManagerRecep.instance == null && !isCollide) || (TutoManagerRecep.instance != null && convoyeur.camera.transform.position.y > convoyeur.maxDeplier))
            {
                if (deplierPressed)
                {
                    if(TutoManagerRecep.instance != null) { TutoManagerRecep.instance.Manager(2); }
                    convoyeur.MoveY("deplier");                                 //Boutton Deplier
                }

                if(TutoManagerRecep.instance != null && convoyeur.camera.transform.position.y <= convoyeur.maxDeplier)
                {
                    TutoManagerRecep.instance.Manager(3);
                }
            }

            if (replierPressed && validatePressed)
            {
                if(TutoManagerRecep.instance != null) { TutoManagerRecep.instance.Manager(24); }
                convoyeur.MoveY("replier");                                     //Boutton Replier + Validate
            }
            
            if(!UpPressed && ! downPressed && !replierPressed && !deplierPressed)
            {
                source.Stop();
            }
        }
        else
        {
            return;
        }
    }

    public void OnOrOff()
    {
        if (TutoManagerRecep.instance != null) { TutoManagerRecep.instance.Manager(1); }

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
        //Vérifier que le tutoriel en a besoin
        //Sinon, ne rien faire et mettre un message d'avertissement
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

