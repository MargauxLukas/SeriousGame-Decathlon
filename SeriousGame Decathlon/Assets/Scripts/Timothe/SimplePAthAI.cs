using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePAthAI : MonoBehaviour
{
    public ChoixCheminIA currentPoint;
    public ChoixCheminIA lastPoint;

    public float speed = 1;
    private bool canMove = true;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            if (Vector2.Distance(currentPoint.position, transform.position) > Random.Range(0.3f,1f))
            {
                GetComponent<Animator>().SetBool("DoesWalk", true);
                GetComponent<Animator>().SetFloat("DirX", currentPoint.position.y - transform.position.y);
                GetComponent<Animator>().SetFloat("DirY", currentPoint.position.x - transform.position.x);
                transform.position += (currentPoint.position - transform.position).normalized * Time.fixedDeltaTime * speed;
            }
            else
            {
                int rng = Random.Range(0, currentPoint.voisins.Count);
                int rngWait = Random.Range(0, 100);
                if (currentPoint.voisins[rng] != lastPoint && rngWait < 30)
                {
                    lastPoint = currentPoint;
                    currentPoint = currentPoint.voisins[rng];
                }
                else
                {
                    lastPoint = currentPoint;
                    currentPoint = currentPoint.voisins[rng];
                    StartCoroutine(WaitHere(Random.Range(3f, 6f)));
                }
            }
        }
    }

    IEnumerator WaitHere(float timeToWait)
    {
        GetComponent<Animator>().SetBool("DoesWalk", false);
        canMove = false;
        yield return new WaitForSeconds(timeToWait);
        canMove = true;
    }
}
