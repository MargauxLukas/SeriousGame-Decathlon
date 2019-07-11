using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionAnomalieRecep : MonoBehaviour
{
    public Camera cameraGeneral;

    public GameObject colisATraiter;
    public ScriptColisRecep colisAffiche;

    public TapisRoulantGeneral tapisGeneral;

    public GameObject gestionAnomalie;

    public GameObject avertissementZoom;

    public GameObject signalBoiteOrange;
    public GameObject signalBoiteVert;
    public GameObject signalBoiteOrangeClignotant;

    public bool doesDetectDimension;
    public bool doesDetectOrientation;
    public bool doesDetectPoids;

    public ColisGestionAnomalieRecep colisAnomalie;

    private bool doesTouch;

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            touchObject();
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            if(doesTouch)
            {
                if (colisATraiter != null)
                {
                    gestionAnomalie.SetActive(true);
                    colisAnomalie.colisScriptable = colisATraiter.GetComponent<ScriptColisRecep>().colisScriptable;
                    colisAnomalie.rotationScr.cartonObj = colisATraiter;
                    colisATraiter.gameObject.GetComponent<SpriteRenderer>().sprite = colisATraiter.GetComponent<SpriteRenderer>().sprite;
                    colisAnomalie.colisTapis = colisATraiter.GetComponent<SpriteRenderer>();
                }
                if(touch.phase == TouchPhase.Ended)
                {
                    doesTouch = false;
                }
            }
            else if(touch.phase == TouchPhase.Began && gestionAnomalie.activeSelf && Vector2.Distance(touchPosition, (gestionAnomalie.transform.position - cameraGeneral.gameObject.transform.position)) >= 7f)
            {
                gestionAnomalie.SetActive(false);
                colisATraiter.GetComponent<ScriptColisRecep>().colisScriptable = colisAnomalie.colisScriptable;

                signalBoiteOrange.SetActive(false);
                signalBoiteOrangeClignotant.SetActive(false);
                if (colisATraiter.GetComponent<ScriptColisRecep>().colisScriptable.isBadOriented)
                {
                    //Afficher les feedbacks de l'anomalie
                    tapisGeneral.doesStop = true;
                    avertissementZoom.SetActive(true);
                    avertissementZoom.GetComponent<affichageAmouleRecep>().alertLevel = 0;
                    signalBoiteOrangeClignotant.SetActive(true);
                }
                else if (colisATraiter.GetComponent<ScriptColisRecep>().colisScriptable.carton.codeRef == "CBGrand")
                {
                    //Afficher les feedbacks de l'anomalie
                    tapisGeneral.doesStop = true;
                    avertissementZoom.SetActive(true);
                    avertissementZoom.GetComponent<affichageAmouleRecep>().alertLevel = 0;
                    signalBoiteOrangeClignotant.SetActive(true);
                }
                else if (colisATraiter.GetComponent<ScriptColisRecep>().colisScriptable.poids >= 35)
                {
                    //Afficher les feedbacks de l'anomalie
                    tapisGeneral.doesStop = true;
                    avertissementZoom.SetActive(true);
                    avertissementZoom.GetComponent<affichageAmouleRecep>().alertLevel = 1;
                    signalBoiteOrange.SetActive(true);
                }
                else
                {
                    tapisGeneral.doesStop = false;
                    colisATraiter = null;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Colis")
        {
            ScriptColisRecep currentColis = collision.GetComponent<ScriptColisRecep>();
            signalBoiteOrange.SetActive(false);
            signalBoiteOrangeClignotant.SetActive(false);
            colisATraiter = collision.gameObject;
            if (doesDetectOrientation)
            {
                if (currentColis.colisScriptable.isBadOriented)
                {
                    //Afficher les feedbacks de l'anomalie
                    tapisGeneral.doesStop = true;
                    avertissementZoom.SetActive(true);
                    avertissementZoom.GetComponent<affichageAmouleRecep>().alertLevel = 0;
                    signalBoiteOrangeClignotant.SetActive(true);
                }
               
            }

            if(doesDetectDimension)
            {
                if(currentColis.colisScriptable.carton.codeRef == "CBGrand")
                {
                    //Afficher les feedbacks de l'anomalie
                    tapisGeneral.doesStop = true;
                    avertissementZoom.SetActive(true);
                    avertissementZoom.GetComponent<affichageAmouleRecep>().alertLevel = 0;
                    signalBoiteOrangeClignotant.SetActive(true);
                }
            }

            if(doesDetectPoids)
            {
                if(currentColis.colisScriptable.poids >= 35)
                {
                    //Afficher les feedbacks de l'anomalie
                    tapisGeneral.doesStop = true;
                    avertissementZoom.SetActive(true);
                    avertissementZoom.GetComponent<affichageAmouleRecep>().alertLevel = 1;
                    signalBoiteOrange.SetActive(true);
                }
            }
        }
    }

    public void ResolveAnomalie()
    {
        signalBoiteOrange.SetActive(false);
        signalBoiteVert.SetActive(true);
        signalBoiteOrangeClignotant.SetActive(false);
}

    void touchObject() //Fonction permettant de détecter si le joueur touche l'objet
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit2D hit = Physics2D.Raycast(cameraGeneral.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject != null && gameObject != null && hit.collider.gameObject == gameObject && hit.collider.gameObject.name == gameObject.name)
            {
                doesTouch = true;
            }
        }
    }
}
