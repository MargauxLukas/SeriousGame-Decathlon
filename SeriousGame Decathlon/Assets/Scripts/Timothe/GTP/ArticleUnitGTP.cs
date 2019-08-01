using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArticleUnitGTP : MonoBehaviour
{
    public Article currentArticle;
    public bool doesTouch;

    public TasArticleGTP tasParent;
    public bool doesTouchNewColis;

    private RemplissageColisGTP remplisColis;
    private AffichagePileArticleGTP remplisColisPrincipal;

    public bool hasBeenScanned;
    public int isPack;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            touchObject();

            if (doesTouch)
            {
                transform.position = touchPosition;
                if (touch.phase == TouchPhase.Ended)
                {
                    doesTouch = false;
                    if(remplisColis == null && remplisColisPrincipal == null)
                    {
                        if(transform.position.x < 61.5f || transform.position.x > 78.5f || transform.position.y > 0.3f || transform.position.y < -2.5f)
                        {
                            transform.position = new Vector3(73, -1.2f, 0);
                        }
                    }
                    else
                    {
                        if (remplisColis != null)
                        {
                            if (isPack != 0)
                            {
                                for (int l = 0; l < isPack; l++)
                                {
                                    remplisColis.AddArticle(currentArticle);
                                }
                            }
                            else
                            {
                                remplisColis.AddArticle(currentArticle);
                            }
                        }
                        else if (remplisColisPrincipal != null)
                        {
                            if (isPack != 0)
                            {
                                for (int l = 0; l < isPack; l++)
                                {
                                    remplisColisPrincipal.AddArticle(currentArticle);
                                }
                            }
                            else
                            {
                                remplisColisPrincipal.AddArticle(currentArticle);
                            }
                        }
                        tasParent.affichageTas.Remove(gameObject);
                        Destroy(gameObject);
                    }
                    //Destroy(gameObject);
                }
            }
        }
        else
        {
            doesTouch = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "ColisGTP")
        {
            remplisColis = collision.GetComponent<RemplissageColisGTP>();
        }
        else if (collision.tag == "ColisPrincipauxGTP" && !collision.GetComponent<AffichagePileArticleGTP>().isOpen)
        {
            remplisColisPrincipal = collision.GetComponent<AffichagePileArticleGTP>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "ColisGTP")
        {
            remplisColis = null;
        }
        else if (collision.tag == "ColisPrincipauxGTP")
        {
            remplisColisPrincipal = null;
        }
    }

    void touchObject()
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
