using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptColisRecep : MonoBehaviour
{
    public Colis colisScriptable;

    private bool doesTouch;
    public bool canMove = false;
    public bool canBePicked = true;
    private bool isOnTapis;

    private Vector2 startPosition;

    private TapisRoulant tapisScript;


    // Start is called before the first frame update
    void Start()
    {
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
            touchObject();

            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            if (doesTouch && canBePicked)
            {
                transform.position = new Vector2(touchPosition.x, touchPosition.y);

                if(touch.phase == TouchPhase.Ended)
                {
                    if(!isOnTapis)
                    {
                        //Malus de lacher le colis
                        transform.position = startPosition;
                    }
                    else
                    {
                        //Ouvrir le menu tourner
                        transform.position = new Vector2(transform.position.x, 3.4f);
                        tapisScript.AddColis(this.gameObject);
                    }
                }
            }

            if(tapisScript != null && Vector2.Distance(touchPosition, tapisScript.turnMenuPosition) > 7f && isOnTapis)
            {
                canMove = true;
            }
        }
        else
        {
            doesTouch = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Tapis" && collision.GetComponent<TapisRoulant>().lastColis == null)
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
        if(face == "Up" && (rotation == 90 || rotation == 270))
        {
            colisScriptable.isBadOriented = false;
            Debug.Log("Test");
        }
        else
        {
            colisScriptable.isBadOriented = true;
        }
    }
}
