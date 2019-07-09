using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoyeurButton : MonoBehaviour
{
    public GameObject convoyeur;
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
            //Deplacement Convoyeur Haut
            Debug.Log("Deplacement Convoyeur Haut");
        }
        if(downPressed)
        {
            Debug.Log("Deplacement Convoyeur bas");
        }
        if(deplierPressed)
        {
            Debug.Log("On déplie");
        }
        if(replierPressed)
        {
            Debug.Log("On replie");
        }

        if(validatePressed)
        {
            Debug.Log("Validate");
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
    }

    public void RepliePointerDown()
    {
        replierPressed = true;
    }

    public void RepliePointerUp()
    {
        replierPressed = false;
    }
}

