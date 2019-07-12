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

    public GameObject signalBoiteOrange;
    public GameObject signalBoiteVert;
    public GameObject signalBoiteOrangeClignotant;

    public GameObject ampouleOrange;
    public GameObject ampouleClignotante;
    public GameObject bulle;

    public bool doesDetectDimension;
    public bool doesDetectOrientation;
    public bool doesDetectPoids;

    public ColisGestionAnomalieRecep colisAnomalie;
    public AffichageAnomalieRecep affichageAnomalieRecep;

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

                ampouleOrange.SetActive(false);
                ampouleClignotante.SetActive(false);
                bulle.SetActive(false);

                if (colisATraiter.GetComponent<ScriptColisRecep>().colisScriptable.isBadOriented)
                {
                    //Afficher les feedbacks de l'anomalie
                    tapisGeneral.doesStop = true;
                    signalBoiteOrangeClignotant.SetActive(true);
                    ampouleClignotante.SetActive(true);
                    bulle.SetActive(true);
                }
                else if (colisATraiter.GetComponent<ScriptColisRecep>().colisScriptable.carton.codeRef == "CBGrand")
                {
                    //Afficher les feedbacks de l'anomalie
                    tapisGeneral.doesStop = true;
                    signalBoiteOrangeClignotant.SetActive(true);
                    ampouleClignotante.SetActive(true);
                    bulle.SetActive(true);
                }
                else if (colisATraiter.GetComponent<ScriptColisRecep>().colisScriptable.poids >= 35)
                {
                    //Afficher les feedbacks de l'anomalie
                    tapisGeneral.doesStop = true;
                    signalBoiteOrange.SetActive(true);
                    ampouleOrange.SetActive(true);
                    bulle.SetActive(true);
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
            ampouleOrange.SetActive(false);
            ampouleClignotante.SetActive(false);
            bulle.SetActive(false);

            colisATraiter = collision.gameObject;
            if (doesDetectOrientation)
            {
                if (currentColis.colisScriptable.isBadOriented)
                {
                    affichageAnomalieRecep.ChangeText("badOriented");
                    tapisGeneral.doesStop = true;
                    signalBoiteOrangeClignotant.SetActive(true);
                    ampouleClignotante.SetActive(true);
                    bulle.SetActive(true);
                }
            }

            if(doesDetectDimension)
            {
                if(currentColis.colisScriptable.carton.codeRef == "CBGrand")
                {
                    affichageAnomalieRecep.ChangeText("dimension");
                    tapisGeneral.doesStop = true;
                    signalBoiteOrangeClignotant.SetActive(true);
                    ampouleClignotante.SetActive(true);
                    bulle.SetActive(true);
                }
            }

            if(doesDetectPoids)
            {
                if(currentColis.colisScriptable.poids >= 35)
                {
                    affichageAnomalieRecep.ChangeText("heavy");
                    tapisGeneral.doesStop = true;
                    signalBoiteOrange.SetActive(true);
                    ampouleOrange.SetActive(true);
                    bulle.SetActive(true);
                }
            }
        }
    }

    public void ResolveAnomalie()
    {
        signalBoiteOrange.SetActive(false);
        signalBoiteVert.SetActive(true);
        signalBoiteOrangeClignotant.SetActive(false);
        ampouleOrange.SetActive(false);
        ampouleClignotante.SetActive(false);
        bulle.SetActive(false);

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
