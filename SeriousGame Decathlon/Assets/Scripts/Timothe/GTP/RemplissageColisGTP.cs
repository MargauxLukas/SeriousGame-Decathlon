using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemplissageColisGTP : MonoBehaviour
{
    public Colis colisScriptable;

    public Transform positionVisee;
    public float speed = 1;
    Vector3 startPosition;

    bool didArrive;

    private void Start()
    {
        colisScriptable = Instantiate(colisScriptable);
    }

    public void AddArticle(Article articleToHad)
    {
        if(colisScriptable.listArticles == null)
        {
            colisScriptable.listArticles = new List<Article>();
        }
        colisScriptable.listArticles.Add(articleToHad);
    }

    public IEnumerator AnimationColisRenvoie()
    {
        if (Vector3.Distance(startPosition, transform.position) < 1.5f && !didArrive)
        {
            transform.position += new Vector3(0, 1, 0) * Time.fixedDeltaTime * 2f;
            transform.localScale -= Vector3.one * Time.fixedDeltaTime * 0.13f;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            StartCoroutine(AnimationColisRenvoie());
        }
        else if (Vector3.Distance(positionVisee.position, transform.position) > 0.1f)
        {
            transform.position -= (positionVisee.position - transform.position).normalized * Time.deltaTime * speed;
            yield return new WaitForSeconds(Time.deltaTime);
            StartCoroutine(AnimationColisRenvoie());
            didArrive = true;
        }
        else
        {
            didArrive = false;
            StopAllCoroutines();
            Destroy(gameObject);
        }
        yield return null;
    }
}
