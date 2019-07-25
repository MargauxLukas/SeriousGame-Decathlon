using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartonVide : MonoBehaviour
{
    public bool doesTouch;

    private Vector3 startPosition;

    public CartonVideLink cvl;
    public ManagerColisVider mcv;

    private Vector3 tapis1Pos;
    private Vector3 tapis2Pos;
    private Vector3 tapis3Pos;

    public int nbEmplacementCarton;

    private bool lectureEnCours = false;

    public void Start()
    {
        tapis1Pos = cvl.tapis1GameObject.transform.position;
        tapis2Pos = cvl.tapis2GameObject.transform.position;
        tapis3Pos = cvl.tapis3GameObject.transform.position;
        startPosition = transform.position;    
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchCarton();

            if(doesTouch)
            {
                transform.position = new Vector3(Camera.main.ScreenToWorldPoint(touch.position).x, Camera.main.ScreenToWorldPoint(touch.position).y, 0);
            }

            if(touch.phase == TouchPhase.Ended)
            {
                transform.position = startPosition;
                doesTouch = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Debug.Log(collision.gameObject.name);

            /*if(touch.phase == TouchPhase.Ended)
            {
                Debug.Log(collision.gameObject.name);
                if (collision.gameObject.name == "Tapis1" && cvl.isFree1)
                {
                    cvl.PutAnotherColis(startPosition);
                    startPosition = new Vector3(62.40f, -3.20f, 0f);
                    cvl.isFree1 = false;
                    cvl.cs1 = gameObject.GetComponent<RemplissageColisGTP>().colisScriptable;
                    GetComponent<RemplissageColisGTP>().enabled = true;
                    //stuck = true;
                    this.enabled = false;
                }
                else if (collision.gameObject.name == "Tapis2" && cvl.isFree2)
                {
                    cvl.PutAnotherColis(startPosition);
                    startPosition = new Vector3(65.5f, -3.20f, 0f);
                    cvl.isFree2 = false;
                    cvl.cs2 = gameObject.GetComponent<RemplissageColisGTP>().colisScriptable;
                    GetComponent<RemplissageColisGTP>().enabled = true;
                    //stuck = true;
                    this.enabled = false;
                }
                else if (collision.gameObject.name == "Tapis3" && cvl.isFree3)
                {
                    cvl.PutAnotherColis(startPosition);
                    startPosition = new Vector3(68.40f, -3.20f, 0f);
                    cvl.isFree3 = false;
                    cvl.cs3 = gameObject.GetComponent<RemplissageColisGTP>().colisScriptable;
                    GetComponent<RemplissageColisGTP>().enabled = true;
                    //stuck = true;
                    this.enabled = false;
                }
            }*/

            //Je pense pas que ce système pour magnétiser le colis puisse marcher

            if (collision.gameObject.name == "Tapis1" && cvl.isFree1 && !lectureEnCours)
            {
                cvl.PutAnotherColis(startPosition);
                lectureEnCours = true;
                //Tableau[0]
                startPosition = new Vector3(62.40f, -3.20f, 0f);
                transform.position = startPosition;
                cvl.isFree1 = false;
                cvl.csTab[0] = gameObject.GetComponent<RemplissageColisGTP>().colisScriptable;
                GetComponent<RemplissageColisGTP>().enabled = true;
                GetComponent<RemplissageColisGTP>().startPosition = startPosition;
                mcv.colisActuellementsPose[0] = GetComponent<RemplissageColisGTP>();
                enabled = false;
                return;
            }
            else if (collision.gameObject.name == "Tapis2" && cvl.isFree2 && !lectureEnCours)
            {
                cvl.PutAnotherColis(startPosition);
                lectureEnCours = true;
                //Tableau[1]
                startPosition = new Vector3(65.5f, -3.20f, 0f);
                transform.position = startPosition;
                cvl.isFree2 = false;
                cvl.csTab[1] = gameObject.GetComponent<RemplissageColisGTP>().colisScriptable;
                GetComponent<RemplissageColisGTP>().enabled = true;
                GetComponent<RemplissageColisGTP>().startPosition = startPosition;
                mcv.colisActuellementsPose[1] = GetComponent<RemplissageColisGTP>();
                enabled = false;
                return;
            }
            else if (collision.gameObject.name == "Tapis3" && cvl.isFree3 && !lectureEnCours)
            {
                cvl.PutAnotherColis(startPosition);
                lectureEnCours = true;
                //Tableau[2]
                startPosition = new Vector3(68.40f, -3.20f, 0f);
                transform.position = startPosition;
                cvl.isFree3 = false;
                cvl.csTab[2] = gameObject.GetComponent<RemplissageColisGTP>().colisScriptable;
                GetComponent<RemplissageColisGTP>().enabled = true;
                GetComponent<RemplissageColisGTP>().startPosition = startPosition;
                mcv.colisActuellementsPose[2] = GetComponent<RemplissageColisGTP>();
                enabled = false;
                return;
            }

            lectureEnCours = false;
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
