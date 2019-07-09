using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoyeurButton : MonoBehaviour
{
    public GameObject convoyeur;
    public bool UpPressed = false;

    private void Update()
    {
        if(UpPressed)
        {
            //Deplacement Convoyeur Haut
            Debug.Log("Deplacement Convoyeur Haut");
        }
    }

    private void On()
    {

    }

    private void Off()
    {

    }

    private void Validation()
    {

    }

    public void UpPointerDown()
    {
        UpPressed = true;
    }

    public void UpPointerUp()
    {
        UpPressed = false;
    }

    private void Down()
    {
        
    }

    private void Deplier()
    {

    }

    private void Replier()
    {

    }
}

