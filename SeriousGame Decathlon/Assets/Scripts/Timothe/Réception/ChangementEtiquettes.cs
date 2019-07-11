using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangementEtiquettes : MonoBehaviour
{
    public int nbEtiquetteMax;
    public int nbEtiquettes;

    public TapisRoulantGeneral tapisGeneral;

    private bool doesTouch;

    private void Update()
    {
        if(nbEtiquettes<= 0.2*nbEtiquetteMax)
        {
            if(nbEtiquettes<=0)
            {
                tapisGeneral.doesStop = true;
            }
        }
       
        if(Input.touchCount > 0)
        {
            touchObject();
            if(doesTouch)
            {
                nbEtiquettes = nbEtiquetteMax;
                doesTouch = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Colis")
        {
            nbEtiquettes--;
        }
    }

    void touchObject() //Fonction permettant de détecter si le joueur touche l'objet
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject != null && gameObject != null && hit.collider.gameObject == gameObject && hit.collider.gameObject.name == gameObject.name)
            {
                Debug.Log("Test");
                doesTouch = true;
            }
        }
    }
}
