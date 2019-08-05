using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeroulementMenuChoixPerso : MonoBehaviour
{
    public List<GameObject> allPerso;
    public List<int> listPersos;

    public float speed;
    public float diffWithPlanete;
    public bool isStar;
    public bool isLorris;
    public int nombreEtoiles;

    public Animator animator;

    public List<GameObject> etoilesDisponibles;
    public Transform parentEtoiles;

    private Vector2 lastPosition;

    public int currentPErso;
    private bool isStop = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       /* if (speed != 0)
        {
            isStop = false;
            foreach (GameObject go in allPerso)
            {
                //go.transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
                if(Vector3.Distance(transform.position, go.transform.position) < 1)
                {
                    go.transform.localScale =Vector3.one*(2 - Vector3.Distance(transform.position, go.transform.position));
                }
            }
        }
        else if(!isStop)
        {
            float distance = 50f;
            GameObject goToTake = allPerso[0];
            foreach (GameObject go in allPerso)
            {
                if(Vector3.Distance(transform.position, go.transform.position) < distance)
                {
                    distance = Vector3.Distance(transform.position, go.transform.position);
                    goToTake = go;
                }
            }

            foreach (GameObject go in allPerso)
            {
                go.transform.position += new Vector3(goToTake.transform.position.x - transform.position.x, 0, 0);
            }
            isStop = true;
            //Prendre le script dans le GO permettant d'avoir le perso
        }*/

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log(Vector2.Distance(touchPosition, lastPosition));
                if (touchPosition.x > lastPosition.x)
                {
                    speed = 1;
                    foreach (GameObject go in allPerso)
                    {
                        go.transform.position += new Vector3(1, 0, 0) * Vector2.Distance(touchPosition, lastPosition);
                        if (Vector3.Distance(transform.position, go.transform.position) < 1)
                        {
                            go.transform.localScale = Vector3.one * (1.5f - Vector3.Distance(transform.position, go.transform.position) / 2);
                        }
                    }
                }
                else if (touchPosition.x < lastPosition.x)
                {
                    speed = -1;
                    foreach (GameObject go in allPerso)
                    {
                        go.transform.position -= new Vector3(1, 0, 0) * Vector2.Distance(touchPosition, lastPosition);
                        if (Vector3.Distance(transform.position, go.transform.position) < 1)
                        {
                            go.transform.localScale = Vector3.one * (1.5f - Vector3.Distance(transform.position, go.transform.position) / 2);
                        }
                    }
                }
            }
            lastPosition = touchPosition;
        }
    }
}
