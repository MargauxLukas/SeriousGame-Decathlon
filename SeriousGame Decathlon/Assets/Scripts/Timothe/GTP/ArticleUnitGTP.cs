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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            if (doesTouch)
            {
                transform.position = touchPosition;
                if (touch.phase == TouchPhase.Ended)
                {
                    doesTouch = false;
                    if(remplisColis == null)
                    {
                        tasParent.LetArticleFall(currentArticle);
                    }
                    else
                    {
                        remplisColis.AddArticle(currentArticle);
                    }
                    Destroy(gameObject);
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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "ColisGTP")
        {
            remplisColis = null;
        }
    }
}
