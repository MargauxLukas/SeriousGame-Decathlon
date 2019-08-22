using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartonVide : MonoBehaviour
{
    public  bool doesTouch;
    private bool lectureEnCours = false;

    [Header("CartonVideLink")]
    public CartonVideLink cvl;

    [Header("Manager Colis Vider")]
    public ManagerColisVider mcv;

    private Vector3 tapis1Pos;
    private Vector3 tapis2Pos;
    private Vector3 tapis3Pos;
    private Vector3 startPosition;

    private Vector2 freezedPosition;

    public void Start()
    {
        tapis1Pos = cvl.tapis1GameObject.transform.position;
        tapis2Pos = cvl.tapis2GameObject.transform.position;
        tapis3Pos = cvl.tapis3GameObject.transform.position;

        startPosition   = transform.position;
        freezedPosition = Vector2.zero;
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchCarton();

            if(doesTouch)
            {
                if (Vector2.Distance    (new Vector2(Camera.main.ScreenToWorldPoint(touch.position).x, Camera.main.ScreenToWorldPoint(touch.position).y), freezedPosition) > 1f)
                {
                    transform.position = new Vector3(Camera.main.ScreenToWorldPoint(touch.position).x, Camera.main.ScreenToWorldPoint(touch.position).y, 0);
                }
                else
                {
                    transform.position = freezedPosition;
                }
            }

            if(touch.phase == TouchPhase.Ended)
            {
                transform.position = startPosition;
                doesTouch = false;
            }
        }
    }

    /*****************************************************************************************
     *  Gros Bordel mais en fait pour chaque colis, on active/desactive tout ce qu'il faut   *
     *****************************************************************************************/
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (doesTouch && this.enabled)
            {
                if (touch.phase == TouchPhase.Ended)
                {
                    Debug.Log("Test instance carton");
                    if (collision.gameObject.name == "Tapis1" && cvl.isFree1 && !lectureEnCours)
                    {
                        lectureEnCours = true;                                                                                      //Empêche certains bugs 
                        collision.GetComponent<BoxCollider2D>().enabled = false;                                                    //Desactive BoxCollider2D pour éviter qu'on puisse le déplacer une fois qu'il est positionné
                        cvl.PutAnotherColis(startPosition);                                                                         //Nouveau colis vide à la place de l'ancien (Colis en haut)
                        mcv.managerColis.cm[0].colisActuelPoste = gameObject.GetComponent<RemplissageColisGTP>();                   //Donne l'info de sur quel console afficher                 
                        startPosition = new Vector3(62.40f, -3.20f, 0f);
                        transform.position = startPosition;                                                                         //Sa position = La position precedemment donné
                        cvl.isFree1 = false;                                                                                        //La place n'est plus libre
                        if(TutoManagerGTP.instance != null) { TutoManagerGTP.instance.Manager(1); }
                        cvl.csTab[0] = gameObject.GetComponent<RemplissageColisGTP>();                                              //On range le colis dans une liste pour avoir des informations quand on en a besoin

                        GetComponent<RemplissageColisGTP>().enabled = true;                          //On active son script (Au colis)
                        GetComponent<RemplissageColisGTP>().startPosition = startPosition;                          //On lui fait prendre une valeur position
                        GetComponent<RemplissageColisGTP>().boxDesactivee = GetComponent<BoxCollider2D>();                          //On lui fait prendre une valeur BoxCollider

                        mcv.colisActuellementsPose[0] = GetComponent<RemplissageColisGTP>();                                        //On fait prendre à colisActuellementPose[0] le colis qu'on vient de poser
                        enabled = false;
                        gameObject.tag = "ColisGTP";
                        return;
                    }
                    else if (collision.gameObject.name == "Tapis2" && cvl.isFree2 && !lectureEnCours)
                    {
                        lectureEnCours = true;
                        collision.GetComponent<BoxCollider2D>().enabled = false;
                        cvl.PutAnotherColis(startPosition);
                        mcv.managerColis.cm[1].colisActuelPoste = gameObject.GetComponent<RemplissageColisGTP>();
                        startPosition = new Vector3(65.26f, -3.20f, 0f);
                        transform.position = startPosition;
                        cvl.isFree2 = false;
                        if (TutoManagerGTP.instance != null) { TutoManagerGTP.instance.Manager(1); }
                        cvl.csTab[1] = gameObject.GetComponent<RemplissageColisGTP>();

                        GetComponent<RemplissageColisGTP>().enabled = true;
                        GetComponent<RemplissageColisGTP>().startPosition = startPosition;
                        GetComponent<RemplissageColisGTP>().boxDesactivee = GetComponent<BoxCollider2D>();

                        mcv.colisActuellementsPose[1] = GetComponent<RemplissageColisGTP>();
                        enabled = false;
                        gameObject.tag = "ColisGTP";
                        return;
                    }
                    else if (collision.gameObject.name == "Tapis3" && cvl.isFree3 && !lectureEnCours)
                    {
                        lectureEnCours = true;
                        collision.GetComponent<BoxCollider2D>().enabled = false;
                        cvl.PutAnotherColis(startPosition);
                        mcv.managerColis.cm[2].colisActuelPoste = gameObject.GetComponent<RemplissageColisGTP>();
                        startPosition = new Vector3(68.17f, -3.20f, 0f);
                        transform.position = startPosition;
                        cvl.isFree3 = false;
                        if (TutoManagerGTP.instance != null) { TutoManagerGTP.instance.Manager(1); }
                        cvl.csTab[2] = gameObject.GetComponent<RemplissageColisGTP>();

                        GetComponent<RemplissageColisGTP>().enabled = true;
                        GetComponent<RemplissageColisGTP>().startPosition = startPosition;
                        GetComponent<RemplissageColisGTP>().boxDesactivee = GetComponent<BoxCollider2D>();

                        mcv.colisActuellementsPose[2] = GetComponent<RemplissageColisGTP>();
                        enabled = false;
                        gameObject.tag = "ColisGTP";
                        return;
                    }
                }

                //Le magnétisme
                if (collision.gameObject.name == "Tapis1" && cvl.isFree1 && !lectureEnCours)
                {
                    lectureEnCours = true;
                    freezedPosition = new Vector3(62.40f, -3.20f, 0f);
                }
                else if (collision.gameObject.name == "Tapis2" && cvl.isFree2 && !lectureEnCours)
                {
                    lectureEnCours = true;
                    freezedPosition = new Vector3(65.26f, -3.20f, 0f);
                }
                else if (collision.gameObject.name == "Tapis3" && cvl.isFree3 && !lectureEnCours)
                {
                    lectureEnCours = true;
                    freezedPosition = new Vector3(68.17f, -3.20f, 0f);
                }

                lectureEnCours = false;
            }
        }
    }

    void touchCarton() //Fonction permettant de détecter si le joueur touche l'objet
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject != null && gameObject != null && hit.collider.gameObject == gameObject && hit.collider.gameObject.name == gameObject.name)
            {
                doesTouch = true;
            }
        }
    }
}
