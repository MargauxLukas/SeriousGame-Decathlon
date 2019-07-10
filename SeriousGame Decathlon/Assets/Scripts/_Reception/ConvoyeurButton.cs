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

    private void Update()
    {
        if(UpPressed     ){convoyeur.MoveZ("up"     );}
        if(downPressed   ){convoyeur.MoveZ("down"   );}
        if(deplierPressed){convoyeur.MoveY("deplier");}
        if(replierPressed && validatePressed){convoyeur.MoveY("replier");}
        else{return;}
    }

    public void OnOrOff()
    {
        if(convoyeur.isOn) {convoyeur.isOn = false;}
        else               {convoyeur.isOn = true ;}
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

    public void RenvoisContener()
    {
        if(convoyeur.isReplierMax)
        {
            //Contener renvoyé
            isReturnContener = true;
        }
    }
}

