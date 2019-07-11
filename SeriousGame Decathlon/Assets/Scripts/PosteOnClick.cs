using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosteOnClick : MonoBehaviour
{
    public GameObject  cameraPoste;
    public GameObject cameraDezoom;
    public GameObject       player;
    public GameObject     moniteur;
    public GameObject        aStar;

    private bool isMoving;
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            isMoving = false;
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            if(gameObject.GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPosition) && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (Vector2.Distance(player.transform.position, gameObject.transform.position) > 1f)
                {
                    player.GetComponent<Unit>().DeplacementPlayer(gameObject.transform.position);
                    isMoving = true;
                }
                else
                {
                    cameraPoste .SetActive(true);
                    cameraDezoom.SetActive(false);
                    aStar       .SetActive(false);
                    moniteur    .SetActive(false);
                    player      .SetActive(false);
                }
            }
        }

        if(isMoving)
        {
            if (Vector2.Distance(player.transform.position, gameObject.transform.position) < 1f)
            {
                cameraPoste.SetActive(true);
                cameraDezoom.SetActive(false);
                aStar.SetActive(false);
                moniteur.SetActive(false);
                player.SetActive(false);
            }
        }
    }

    public void ReturnToDezoom()
    {
        cameraPoste .SetActive(false);
        cameraDezoom.SetActive(true);
        aStar       .SetActive(true);
        moniteur    .SetActive(true);
        player      .SetActive(true);
    }
}
