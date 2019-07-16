using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosteOnClick : MonoBehaviour
{
    [Header("Camera du Poste")]
    public GameObject  cameraPoste;   //Camera vers laquelle on souhaite aller
    [Header("Camera de la vue dézoom")]
    public GameObject cameraDezoom;   //Camera vue Dezoom
    public GameObject       player;
    public GameObject     moniteur;
    public GameObject        aStar;

    [Header("Apparition du joueur")]
    public ConvoyeurManager managerConvoie;
    public Transform positionVoulue;

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
                cameraPoste .SetActive(true);
                cameraDezoom.SetActive(false);
                aStar       .SetActive(false);
                moniteur    .SetActive(false);
                player      .SetActive(false);
            }
        }
    }

    public void ReturnToDezoom()
    {
        if (TutoManager.instance != null) {TutoManager.instance.Manager(45);}
        cameraPoste .SetActive(false);
        cameraDezoom.SetActive(true);
        aStar       .SetActive(true);
        moniteur    .SetActive(true);
        player      .SetActive(true);
        if (!managerConvoie.isReplierMax)
        {
            player.transform.position = positionVoulue.position;
        }
    }
}
