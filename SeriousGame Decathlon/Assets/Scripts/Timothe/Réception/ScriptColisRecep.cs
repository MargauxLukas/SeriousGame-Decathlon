using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptColisRecep : MonoBehaviour
{
    public Colis colisScriptable;

    public bool doesTouch;
    public bool canMove = false;
    public bool canBePicked = true;
    private bool isOnTapis;

    public int currentHauteur = 0;

    private Vector2 startPosition;

    private TapisRoulant tapisScript;

    public bool isOneSecondScreen;

    // Start is called before the first frame update
    void Start()
    {
        if(canBePicked)
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

            if (tapisScript != null && Vector2.Distance(touchPosition, tapisScript.turnMenuPosition) > 7f && isOnTapis && !tapisScript.tapisGeneral.doesStop)
            {
                canMove = true;
            }
            if (!canMove)
            {
                touchObject();
            }
            else
            {
                doesTouch = false;
            }

            if (doesTouch && canBePicked)
            {
                transform.position = new Vector2(touchPosition.x, touchPosition.y);

                if(touch.phase == TouchPhase.Ended)
                {
                    if(!isOnTapis || tapisScript.tapisGeneral.doesStop)
                    {
                        //Malus de lacher le colis
                        transform.position = startPosition;
                    }
                    else
                    {
                        transform.position = new Vector2(transform.position.x, tapisScript.positionTapisZoom.position.y);
                        tapisScript.AddColis(this.gameObject);
                    }
                }
            }
            
        }
        else
        {
            doesTouch = false;
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
        if (!isOneSecondScreen)
        {
            if (face == "Up" && (rotation == 90 || rotation == 270))
            {
                colisScriptable.isBadOriented = false;
            }
            else
            {
                colisScriptable.isBadOriented = true;
            }
        }
        else
        {
            if (face == "Up" && (rotation == 0 || rotation == 180))
            {
                colisScriptable.isBadOriented = false;
            }
            else
            {
                colisScriptable.isBadOriented = true;
            }
        }
    }
}
