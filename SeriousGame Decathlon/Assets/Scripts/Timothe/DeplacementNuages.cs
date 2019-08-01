using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementNuages : MonoBehaviour
{
    public float speed;
    public float diffWithPlanete;
    public bool isStar;
    public bool isLorris;
    public int nombreEtoiles;

    public Animator animator;

    public List<GameObject> etoilesDisponibles;
    public Transform parentEtoiles;

    private Vector2 lastPosition;

    public bool movedUp = false;
    public bool movedDown = false;

    private void Start()
    {
        //Placement des étoiles
        for(int i = 0; i < nombreEtoiles; i++)
        {
            GameObject gm = Instantiate(etoilesDisponibles[Random.Range(0, etoilesDisponibles.Count - 1)], parentEtoiles);
            gm.transform.localPosition = new Vector3(Random.Range(10.37f - (12.3f * (1 + ((diffWithPlanete + speed) / speed))), 10.37f *3), Random.Range(-15f, 6f), 0);
            gm.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isStar)
        {
            if (!isLorris)
            {
                if (transform.localPosition.x <= 10.37f - (12.3f * (1 + ((Mathf.Abs(diffWithPlanete) + Mathf.Abs(speed)) / Mathf.Abs(speed)))))
                {
                    transform.localPosition = new Vector3(10.37f, transform.localPosition.y, 0);
                }
            }
            else
            {
                if (transform.localPosition.x <= 10.37f - (12.3f * 3 * (1 + ((Mathf.Abs(diffWithPlanete) + Mathf.Abs(speed)) / Mathf.Abs(speed)))))
                {
                    transform.localPosition = new Vector3(10.37f, transform.localPosition.y, 0);
                }
            }
            transform.localPosition += new Vector3(-1, 0, 0) * (speed + (diffWithPlanete * (speed / 0.15f)));
        }
        else
        {
            if (transform.localPosition.x >= 10.37f)
            {
                transform.localPosition = new Vector3(-1.97f, transform.localPosition.y, 0);
            }
            transform.localPosition += new Vector3(1, 0, 0) * speed;
        }

        if(Input.touchCount>0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = touch.position;
            if (touch.phase == TouchPhase.Began)
            {
                movedUp = false;
                movedDown = false;
                lastPosition = touchPosition;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                movedUp = false;
                movedDown = false;
            }
            else if(touch.phase == TouchPhase.Moved)
            {
                Debug.Log(Vector2.Distance(touchPosition, lastPosition));
                if (touchPosition.x > lastPosition.x && touchPosition.y > lastPosition.y && Vector2.Distance(touchPosition, lastPosition) > 50f && !movedDown)
                {
                    movedUp = true;
                    speed += Time.deltaTime * 0.75f;
                    if(speed > 1.2f)
                    {
                        speed = 1.2f;
                    }
                }
                if (touchPosition.x < lastPosition.x && touchPosition.y < lastPosition.y && !movedUp && Vector2.Distance(touchPosition, lastPosition) > 50f)
                {
                    movedDown = true;
                    speed -= Time.deltaTime * 0.75f;
                    if(speed < 0)
                    {
                        speed = 0;
                    }
                }
            }
            lastPosition = touchPosition;
        }

        if (animator != null)
        {
            animator.speed = 1 * (speed / 0.15f);
        }

    }
}
