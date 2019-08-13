using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptColisRecep : MonoBehaviour
{
    public Colis colisScriptable;

    public bool doesTouch;
    public bool canMove = false;
    public bool canBePicked = true;
    public bool canBePickedTuto = false;
    private bool isOnTapis;

    public int currentHauteur = 0;

    private Vector2 startPosition;

    private TapisRoulant tapisScript;

    public bool isOneSecondScreen;

    // Start is called before the first frame update
    void Start()
    {
        if(colisScriptable!=null)
        {
            colisScriptable = Instantiate(colisScriptable);
        }

        if (canBePicked)
        {
            Color theColor = new Color();
            theColor.b = 260;
            theColor.g = 260;
            theColor.r = 260;
            theColor.a = 1;
            GetComponent<SpriteRenderer>().color = theColor;
        }
        startPosition = transform.position;
        if(colisScriptable.isBadOriented)
        {
            GetComponent<SpriteRenderer>().sprite = colisScriptable.carton.spriteCartonsListe[4];
            Tourner("Forward", 0);
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = colisScriptable.carton.spriteCartonsListe[0];
            transform.Rotate(0, 0, 90, Space.Self);
            Tourner("Up", 90);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
                if (TutoManagerRecep.instance == null)
                {
                    if (tapisScript != null && Vector2.Distance(touchPosition, tapisScript.turnMenuPosition) > 7f && isOnTapis && tapisScript.tapisGeneral.convoyeur.isOn)
                    {
                        canMove = true;
                    }
                }
                else if (TutoManagerRecep.instance != null)
                {
                    if (tapisScript != null && Vector2.Distance(touchPosition, tapisScript.turnMenuPosition) > 7f && isOnTapis && tapisScript.tapisGeneral.convoyeur.isOn && !tapisScript.turnMenu.activeSelf && TutoManagerRecep.instance.canMoveTuto)
                    {
                        canMove = true;
                    }
                }
            if (!canMove && (tapisScript == null || tapisScript.colisSurLeTapis.Count>0 || !tapisScript.colisSurLeTapis.Contains(gameObject)))
            {
                touchObject();
                if (tapisScript != null && tapisScript.turnMenu.activeSelf)
                {
                    doesTouch = false;
                }
            }
            else
            {
                doesTouch = false;
            }

            if (TutoManagerRecep.instance == null)
            {
                if (isOnTapis && !doesTouch && !tapisScript.colisSurLeTapis.Contains(gameObject) && !tapisScript.colisEnvoye.Contains(gameObject))
                {
                    transform.position = new Vector2(transform.position.x, tapisScript.positionTapisZoom.position.y);
                    tapisScript.AddColis(this.gameObject);
                }
                else if (doesTouch && canBePicked)
                {
                    transform.position = new Vector2(touchPosition.x, touchPosition.y);
                    MalusScript.instance.HaveAMalus();                                  //Je le met ici car je pense qu'il peut être sympa de donner un malus tant qu'il le garde dans les mains (Le malus doit être faible du coup)
                    if (touch.phase == TouchPhase.Ended)
                    {
                        if (!isOnTapis || !tapisScript.tapisGeneral.convoyeur.isOn)
                        {
                            Scoring.instance.RecepMalus(15);
                            transform.position = startPosition;
                            Scoring.instance.EndLosePointOnTime();
                        }
                        else
                        {
                            transform.position = new Vector2(transform.position.x, tapisScript.positionTapisZoom.position.y);
                            tapisScript.AddColis(this.gameObject);
                            if (colisScriptable.estAbime || colisScriptable.carton.codeRef == "CBGrand")
                            {
                                Scoring.instance.EndLosePointOnTime();
                                Scoring.instance.RecepMalus(50);
                                Scoring.instance.ResetComboRpcep();
                            }
                            else
                            {
                                Scoring.instance.UpCombo();
                                Scoring.instance.EndLosePointOnTime();
                            }
                        }
                    }
                }
            }
            
            if(TutoManagerRecep.instance != null)
            {
                if (isOnTapis && !doesTouch && !tapisScript.colisSurLeTapis.Contains(gameObject) && !tapisScript.colisEnvoye.Contains(gameObject))
                {
                    Debug.Log("Jsp ce que c'est mais il y passe");
                    transform.position = new Vector2(transform.position.x, tapisScript.positionTapisZoom.position.y);
                    tapisScript.AddColis(this.gameObject);
                }
                else if (doesTouch && canBePicked && canBePickedTuto)
                {
                    Debug.Log("canBePickedTuto");
                    if (TutoManagerRecep.instance != null) { TutoManagerRecep.instance.Manager(9); }
                    transform.position = new Vector2(touchPosition.x, touchPosition.y);
                    if (touch.phase == TouchPhase.Ended)
                    {
                        if (!isOnTapis || !tapisScript.tapisGeneral.convoyeur.isOn)
                        {
                            Scoring.instance.RecepMalus(15);
                            transform.position = startPosition;
                            Scoring.instance.EndLosePointOnTime();
                        }
                        else
                        {
                            transform.position = new Vector2(transform.position.x, tapisScript.positionTapisZoom.position.y);
                            tapisScript.AddColis(this.gameObject);
                            Scoring.instance.UpCombo();
                            Scoring.instance.EndLosePointOnTime();
                        }
                    }
                }
            }
        }
        else
        {
            doesTouch = false;
            Scoring.instance.EndLosePointOnTime();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Tapis" && collision.GetComponent<TapisRoulant>().lastColis == null)
        {
            isOnTapis = true;
            tapisScript = collision.GetComponent<TapisRoulant>();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Tapis" && collision.GetComponent<TapisRoulant>().lastColis == null)
        {
            isOnTapis = true;
            tapisScript = collision.GetComponent<TapisRoulant>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Tapis")
        {
            isOnTapis = false;
        }
    }

    void touchObject() //Fonction permettant de détecter si le joueur touche l'objet
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

    public void Tourner(string face, float rotation)
    {
        if ((face == "Up" || face =="Down") && ((rotation >= 85 && rotation <= 95) || (rotation >= 265 && rotation <= 275)))
        {
            Debug.Log("Test here orientation up");
            colisScriptable.isBadOriented = false;
        }
        else
        {
            Debug.Log("Test here orientation up 2");
            colisScriptable.isBadOriented = true;
        }
    }
}
