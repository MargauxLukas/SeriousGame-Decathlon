using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionColisBombe : MonoBehaviour
{
    private bool haveAlreadySomething;
    public GameObject colisRevoir;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ColisGTP")
        {
            if(collision.GetComponent<RemplissageColisGTP>() != null)
            {
                if(collision.GetComponent<RemplissageColisGTP>().tauxRemplissage >= 10)
                {
                    if (!haveAlreadySomething)
                    {
                        collision.GetComponent<RemplissageColisGTP>().StopAllCoroutines();
                        colisRevoir = collision.gameObject;
                        collision.GetComponent<RemplissageColisGTP>().besoinEtreVide = true;
                        StartCoroutine(MoveToNeuviemePoste(collision.gameObject));
                    }
                }
            }
        }
    }

    IEnumerator MoveToNeuviemePoste(GameObject colisToMove)
    {
        for(int i = 0; i < 100; i++)
        {
            colisToMove.transform.position -= new Vector3(0, -1, 0) * Time.deltaTime * 2;
            transform.localScale += Vector3.one * Time.fixedDeltaTime * 0.13f;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public void RenvoieColis()
    {
        if (!colisRevoir.GetComponent<RemplissageColisGTP>().besoinEtreVide)
        {
            colisRevoir.GetComponent<RemplissageColisGTP>().AnimationColisRenvoie();
            colisRevoir.GetComponent<RemplissageColisGTP>().tauxRemplissage = 0;
        }
    }

}
