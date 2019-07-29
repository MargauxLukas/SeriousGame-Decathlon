using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFaireVenirColis : MonoBehaviour
{
    public Transform positionVisee;
    public float speed = 5;
    Vector3 startPosition;
    Vector3 startScale;

    bool didArrive;
    bool didArriveSecond;
    private bool began = false;
    public int emplacement;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startScale = transform.localScale;
    }

    // Update is called once per frame
    public IEnumerator AnimationColis(GameObject aActiver)
    {
        /*if (!began)
        {
            transform.position = startPosition;
            began = true;
        }*/
        if (Vector3.Distance(positionVisee.position, transform.position) > 0.1f && !didArrive)
        {
            transform.position += (positionVisee.position - transform.position).normalized * Time.deltaTime * speed;
            yield return new WaitForSeconds(Time.deltaTime);
            StartCoroutine(AnimationColis(aActiver));
        }
        else if(Vector3.Distance(positionVisee.position, transform.position) < 1.5f)
        {
            didArrive = true;
            transform.position -= new Vector3(0, 1, 0) * Time.fixedDeltaTime * 2f;
            transform.localScale += Vector3.one * Time.fixedDeltaTime * 0.13f;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            StartCoroutine(AnimationColis(aActiver));
        }
        else
        {
            began = false;
            didArrive = false;
            transform.position = startPosition;
            aActiver.SetActive(true);
            transform.localScale = startScale;
            StopAllCoroutines();
        }
        yield return null;
    }

    public IEnumerator AnimationColisRenvoie(ManagerColisVider aActiver)
    {
        if(!began)
        {
            transform.position = aActiver.emplacementsScripts[emplacement].transform.position;
            began = true;
            didArriveSecond = false;
        }
        if (Vector3.Distance(positionVisee.position, transform.position) > 0.1f && !didArriveSecond)
        {
            transform.position += new Vector3(0, 1, 0) * Time.fixedDeltaTime * 2f;
            transform.localScale -= Vector3.one * Time.fixedDeltaTime * 0.13f;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            StartCoroutine(AnimationColisRenvoie(aActiver));
        }
        else if (Vector3.Distance(startPosition, transform.position) > 0.1f)
        {
            transform.position += (startPosition - transform.position).normalized * Time.deltaTime * speed;
            yield return new WaitForSeconds(Time.deltaTime);
            StartCoroutine(AnimationColisRenvoie(aActiver));
            didArriveSecond = true;
        }
        else
        {
            aActiver.FaireVenirNouveauColis(emplacement);
            //transform.position = startPosition;
            //transform.localScale = startScale;
            StopAllCoroutines();
            began = false;
            didArriveSecond = false;
        }
        yield return null;
    }
}
