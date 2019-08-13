using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePAthAI : MonoBehaviour
{
    public List<Transform> positionsVoulues;
    public List<bool> needWait;
    private int currentPosVoulue = 0;
    public float speed = 1;
    private bool canMove = true;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            if (Vector2.Distance(positionsVoulues[currentPosVoulue].position, transform.position) > 0.5f)
            {
                //GetComponent<Animator>().SetBool("DoesWalk", true);
                transform.position += (positionsVoulues[currentPosVoulue].position - transform.position).normalized * Time.fixedDeltaTime * speed;
            }
            else
            {
                if (needWait[currentPosVoulue])
                {
                    //GetComponent<Animator>().SetBool("DoesWalk", false);
                    StartCoroutine(WaitHere(Random.Range(3, 7)));
                }
                currentPosVoulue++;
                currentPosVoulue = currentPosVoulue % positionsVoulues.Count;
            }
        }
    }

    IEnumerator WaitHere(float timeToWait)
    {
        canMove = false;
        yield return new WaitForSeconds(timeToWait);
        canMove = true;
    }
}
