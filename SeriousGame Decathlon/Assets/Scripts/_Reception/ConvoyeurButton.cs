using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoyeurButton : MonoBehaviour
{
    public ConvoyeurManager convoyeur;

    public bool UpPressed   = false;
    public bool downPressed = false;
    public bool replierPressed = false;
    public bool deplierPressed = false;
    public bool isOn = false;
    public bool validatePressed = false;

    private void Update()
    {
        if(UpPressed)
        {
            convoyeur.MoveZ("up");
        }
        if(downPressed)
        {
            convoyeur.MoveZ("down");
        }
        if(deplierPressed)
        {
            convoyeur.MoveY("deplier");
        }
        if(replierPressed && validatePressed)
        {
            convoyeur.MoveY("replier");
        }
        else
        {

        }
    }

    public void OnOrOff()
    {
        if(isOn)
        {
            isOn = false;
        }
        else
        {
            isOn = true;
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
    }

    public void DownPointerDown()
    {
        downPressed = true;
    }

    public void DownPointerUp()
    {
        downPressed = false;
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
}

