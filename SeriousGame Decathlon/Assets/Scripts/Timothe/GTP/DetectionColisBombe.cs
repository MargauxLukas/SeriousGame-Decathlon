using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionColisBombe : MonoBehaviour
{
    public bool haveAlreadySomething;
    public GameObject colisRevoir;

    private void Update()
    {
        if(colisRevoir != null && !colisRevoir.GetComponent<RemplissageColisGTP>().besoinEtreVide)
        {
            RenvoieColis();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ColisGTP")
        {
            if(collision.GetComponent<RemplissageColisGTP>() != null)
            {
                if(collision.GetComponent<RemplissageColisGTP>().tauxRemplissage > 1)
                {
                    if (!haveAlreadySomething)
                    {
                        foreach(Transform child in transform)
                        {
                            child.position -= new Vector3(4, 0, 0);
                        }
                        haveAlreadySomething = true;
                        Scoring.instance.LosePointGTP(25, "Renvoie d'un colis trop chargé");
                        collision.GetComponent<RemplissageColisGTP>().StopAllCoroutines();
                        collision.GetComponent<SpriteRenderer>().sortingOrder += 2;
                        colisRevoir = collision.gameObject;
                        collision.GetComponent<RemplissageColisGTP>().besoinEtreVide = true;
                        collision.GetComponent<RemplissageColisGTP>().estParti = false;
                        collision.GetComponent<RemplissageColisGTP>().canBeTouch = true;
                        StartCoroutine(MoveToNeuviemePoste(collision.gameObject));
                    }
                }
                
            }
        }
    }

    IEnumerator MoveToNeuviemePoste(GameObject colisToMove)
    {
        for(int i = 0; i < 20; i++)
        {
            colisToMove.transform.position -= new Vector3(0, 1, 0) * Time.deltaTime * 2;
            //transform.localScale += Vector3.one * Time.fixedDeltaTime * 0.13f;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public void RenvoieColis()
    {
        if (!colisRevoir.GetComponent<RemplissageColisGTP>().besoinEtreVide)
        {
            haveAlreadySomething = false;
            colisRevoir.GetComponent<RemplissageColisGTP>().canBeTouch = false;
            colisRevoir.GetComponent<SpriteRenderer>().sortingOrder--;
            colisRevoir.GetComponent<RemplissageColisGTP>().startPosition = colisRevoir.transform.position;
            colisRevoir.GetComponent<RemplissageColisGTP>().didArrive = false;
            StartCoroutine(colisRevoir.GetComponent<RemplissageColisGTP>().AnimationColisRenvoie());
            colisRevoir.GetComponent<RemplissageColisGTP>().tauxRemplissage = 0;
            colisRevoir = null;
        }
    }

}
