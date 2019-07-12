using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentouseDeplacement : MonoBehaviour
{
    private bool doesTouch;
    public GameObject colisAttached;

    private bool haveToWait;

    private Vector3 startPosition;

    public GameObject menuTourner;

    private void Start()
    {
        startPosition = transform.localPosition;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchObject();

            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            if (menuTourner.activeSelf)
            {
                transform.localPosition = startPosition;
                doesTouch = false;
                if (colisAttached != null)
                {
                    colisAttached.GetComponent<ScriptColisRecep>().doesTouch = false;
                    colisAttached = null;
                }
            }

            if (doesTouch && !haveToWait)
            {
                transform.position = new Vector2(touchPosition.x, touchPosition.y);

                if (touch.phase == TouchPhase.Ended)
                {
                    transform.localPosition = startPosition;
                    colisAttached.GetComponent<ScriptColisRecep>().doesTouch = false;
                    colisAttached = null;
                }
            }
            else
            {
                if (colisAttached != null)
                {
                    colisAttached.GetComponent<ScriptColisRecep>().doesTouch = false;
                    colisAttached = null;
                }
            }
        }
        else if(menuTourner.activeSelf)
        {
            doesTouch = false;
            if (colisAttached != null)
            {
                colisAttached.GetComponent<ScriptColisRecep>().doesTouch = false;
                colisAttached = null;
            }
        }
        else
        {
            doesTouch = false;
            if(colisAttached != null)
            {
                colisAttached.GetComponent<ScriptColisRecep>().doesTouch = false;
                colisAttached = null;
            }
        }
    }

    IEnumerator NowWait()
    {
        haveToWait = true;
        yield return new WaitForSeconds(0.5f);
        haveToWait = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (doesTouch && collision.tag == "Colis" && colisAttached == null && collision.GetComponent<ScriptColisRecep>().canBePicked && !collision.GetComponent<ScriptColisRecep>().canMove)
        {
            colisAttached = collision.gameObject;
            colisAttached.GetComponent<ScriptColisRecep>().doesTouch = true;
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

}
