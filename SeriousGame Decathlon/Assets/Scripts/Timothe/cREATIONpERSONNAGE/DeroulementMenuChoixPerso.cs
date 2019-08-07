using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeroulementMenuChoixPerso : MonoBehaviour
{
    public List<GameObject> allPerso;
    public List<int> listPersos;

    public float speed;
    private Vector2 lastPosition;

    public int currentPErso;
    private bool isStop = true;

    // Start is called before the first frame update
    void Start()
    {
        allPerso[1].GetComponent<MenuPersoChoice>().StartAnim();
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
            if(touch.phase == TouchPhase.Began)
            {
                lastPosition = touchPosition;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (touchPosition.x > lastPosition.x)
                {
                    speed = 1;
                    foreach (GameObject go in allPerso)
                    {
                        go.transform.position += new Vector3(1, 0, 0) * Vector2.Distance(touchPosition, lastPosition);
                        if (Vector3.Distance(transform.position, go.transform.position) < 3)
                        {
                            go.transform.localScale = Vector3.one * (1.5f - Vector3.Distance(transform.position, go.transform.position) / 6) * 3;
                        }
                    }
                }
                else if (touchPosition.x < lastPosition.x)
                {
                    speed = -1;
                    foreach (GameObject go in allPerso)
                    {
                        go.transform.position -= new Vector3(1, 0, 0) * Vector2.Distance(touchPosition, lastPosition);
                        if (Vector3.Distance(transform.position, go.transform.position) < 3)
                        {
                            go.transform.localScale = Vector3.one * (1.5f - Vector3.Distance(transform.position, go.transform.position) / 6) * 3;
                        }
                    }
                }
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                float distance = 50f;
                GameObject goToTake = allPerso[0];
                foreach (GameObject go in allPerso)
                {
                    if (Vector3.Distance(transform.position, go.transform.position) < distance)
                    {
                        distance = Vector3.Distance(transform.position, go.transform.position);
                        goToTake = go;
                    }
                }

                Vector3 newPos = new Vector3(transform.position.x - goToTake.transform.position.x, 0, 0);

                foreach (GameObject go in allPerso)
                {
                    go.transform.position += newPos;
                    go.transform.localScale = Vector3.one * 3;
                    go.GetComponent<MenuPersoChoice>().StopAnim();
                }
                goToTake.GetComponent<MenuPersoChoice>().StartAnim();
                goToTake.transform.localScale = Vector3.one * (1.5f - Vector3.Distance(transform.position, goToTake.transform.position) / 6) * 3;
                ManagerPersoChoisit.instance.Personnage = goToTake.GetComponent<MenuPersoChoice>().numDuPerso;
            }
            lastPosition = touchPosition;
        }
    }
}
