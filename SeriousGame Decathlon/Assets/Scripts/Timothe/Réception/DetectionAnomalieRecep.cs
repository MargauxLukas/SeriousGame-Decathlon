using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionAnomalieRecep : MonoBehaviour
{
    public Unit player;

    [Header("Camera")]
    public Camera cameraGeneral;

    [Header("Assigné automatiquement")]
    public GameObject colisATraiter;
    public ScriptColisRecep colisAffiche;

    [Header("Ampoule/Led")]
    public GameObject signalBoiteOrange          ;
    public GameObject signalBoiteVert            ;
    public GameObject signalBoiteOrangeClignotant;
    public GameObject ampouleOrange              ;
    public GameObject ampouleClignotante         ;
    public GameObject bulle                      ;

    [Header("Erreur detecté")]
    public bool doesDetectDimension  ;
    public bool doesDetectOrientation;
    public bool doesDetectPoids      ;
    public bool gotAnomalie;

    [Header("Gestion Anomalie")]
    public ColisGestionAnomalieRecep colisAnomalie;
    public AffichageAnomalieRecep affichageAnomalieRecep;
    public GameObject gestionAnomalie;
    public ColisGestionAnomalieRecep colisGestionScript;
    public ChangementEtiquettes etiquettesManager;

    [Header("Tapis")]
    public TapisRoulantGeneral tapisGeneral;

    private bool doesTouch;
    private void Start()
    {
        signalBoiteVert.SetActive(true);
    }

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
                    GetComponent<BoxCollider2D>().enabled = false;
                    player.stuck = true;
                    colisAnomalie.colisScriptable = colisATraiter.GetComponent<ScriptColisRecep>().colisScriptable;
                    colisAnomalie.rotationScr.cartonObj = colisATraiter;
                    colisAnomalie.GetComponent<SpriteRenderer>().sprite = colisATraiter.GetComponent<SpriteRenderer>().sprite;
                    colisAnomalie.colisTapis                            = colisATraiter.GetComponent<SpriteRenderer>();
                }
                if(touch.phase == TouchPhase.Ended)
                {
                    doesTouch = false;
                }
            }
            else if(touch.phase == TouchPhase.Began && gestionAnomalie.activeSelf && Vector2.Distance(touchPosition, gestionAnomalie.transform.position) >= 6f)
            {
                Debug.Log("A verifer : " + Vector2.Distance(touchPosition, (gestionAnomalie.transform.position - cameraGeneral.gameObject.transform.position)));
                colisGestionScript.circleImage.gameObject.SetActive(false);
                colisGestionScript.tournerMenu.SetActive(false);
                colisGestionScript.doesTouch = false;
                colisGestionScript.timeTouched = 0;

                gestionAnomalie.SetActive(false);
                GetComponent<BoxCollider2D>().enabled = true;
                player.stuck = false;
                colisATraiter.GetComponent<ScriptColisRecep>().colisScriptable = colisAnomalie.colisScriptable;

                if (colisATraiter.GetComponent<ScriptColisRecep>().colisScriptable.isBadOriented)
                {
                    //Afficher les feedbacks de l'anomalie
                    tapisGeneral.doesStop = true;
                    signalBoiteVert            .SetActive(false);
                    signalBoiteOrange          .SetActive(false);
                    signalBoiteOrangeClignotant.SetActive(true );
                    ampouleClignotante         .SetActive(true );
                    ampouleOrange              .SetActive(false);
                    bulle                      .SetActive(true );
                    Scoring.instance.RecepMalus(15);
                }
                else if (colisATraiter.GetComponent<ScriptColisRecep>().colisScriptable.carton.codeRef == "CBGrand")
                {
                    //Afficher les feedbacks de l'anomalie
                    tapisGeneral.doesStop = true;
                    signalBoiteVert            .SetActive(false);
                    signalBoiteOrange          .SetActive(false);
                    signalBoiteOrangeClignotant.SetActive(true );
                    ampouleClignotante         .SetActive(true );
                    ampouleOrange              .SetActive(false);
                    bulle                      .SetActive(true );
                    Scoring.instance.RecepMalus(15);
                }
                else if (colisATraiter.GetComponent<ScriptColisRecep>().colisScriptable.poids >= 35)
                {
                    //Afficher les feedbacks de l'anomalie
                    tapisGeneral.doesStop = true;
                    signalBoiteVert            .SetActive(false);
                    signalBoiteOrange          .SetActive(true );
                    signalBoiteOrangeClignotant.SetActive(false);
                    ampouleClignotante         .SetActive(false);
                    ampouleOrange              .SetActive(true );
                    bulle                      .SetActive(true );
                    Scoring.instance.RecepMalus(15);
                }
                else
                {
                    gotAnomalie = false;
                    if (etiquettesManager.nbEtiquettes > 0)
                    {
                        tapisGeneral.doesStop = false;
                        signalBoiteVert.SetActive(true);
                        signalBoiteOrange.SetActive(false);
                        signalBoiteOrangeClignotant.SetActive(false);
                        ampouleClignotante.SetActive(false);
                        ampouleOrange.SetActive(false);
                        bulle.SetActive(false);
                    }
                    colisATraiter = null;
                    Scoring.instance.RecepBonus(350);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Colis")
        {
            ScriptColisRecep currentColis = collision.GetComponent<ScriptColisRecep>();
            signalBoiteOrange          .SetActive(false);
            signalBoiteOrangeClignotant.SetActive(false);
            ampouleOrange              .SetActive(false);
            ampouleClignotante         .SetActive(false);
            bulle                      .SetActive(false);

            if (etiquettesManager.nbEtiquettes > 0)
            {
                signalBoiteVert.SetActive(false);
            }

            colisATraiter = collision.gameObject;

            if (doesDetectOrientation)
            {
                if (currentColis.colisScriptable.isBadOriented)
                {
                    affichageAnomalieRecep.ChangeText("badOriented");
                    tapisGeneral.doesStop = true;
                    signalBoiteVert            .SetActive(false);
                    signalBoiteOrangeClignotant.SetActive(true);
                    ampouleClignotante         .SetActive(true);
                    bulle                      .SetActive(true);
                    gotAnomalie = true;
                    Scoring.instance.RecepMalus(15);
                }
            }

            if(doesDetectDimension)
            {
                if(currentColis.colisScriptable.carton.codeRef == "CBGrand")
                {
                    gotAnomalie = true;
                    affichageAnomalieRecep.ChangeText("dimension");
                    tapisGeneral.doesStop = true;
                    signalBoiteVert            .SetActive(false);
                    signalBoiteOrangeClignotant.SetActive(true);
                    ampouleClignotante         .SetActive(true);
                    bulle                      .SetActive(true);
                }
            }

            if(doesDetectPoids)
            {
                if(currentColis.colisScriptable.poids >= 35)
                {
                    gotAnomalie = true;
                    affichageAnomalieRecep.ChangeText("heavy");
                    tapisGeneral.doesStop = true;
                    signalBoiteVert  .SetActive(false);
                    signalBoiteOrange.SetActive(true);
                    ampouleOrange    .SetActive(true);
                    bulle            .SetActive(true);
                }
            }
            Scoring.instance.RecepRenvoieColis();
        }
    }

    public void ResolveAnomalie()
    {
        if (etiquettesManager.nbEtiquettes > 0)
        {
            signalBoiteVert.SetActive(true);
            signalBoiteOrange.SetActive(false);
            signalBoiteOrangeClignotant.SetActive(false);
            ampouleOrange.SetActive(false);
            ampouleClignotante.SetActive(false);
            bulle.SetActive(false);
        }
    }

    void touchObject() //Fonction permettant de détecter si le joueur touche l'objet
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit2D hit = Physics2D.Raycast(cameraGeneral.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject != null 
                                     &&              gameObject != null 
                                     && hit.collider.gameObject      == gameObject
                                     && hit.collider.gameObject.name == gameObject.name)
            {
                doesTouch = true;
            }
        }
    }
}
