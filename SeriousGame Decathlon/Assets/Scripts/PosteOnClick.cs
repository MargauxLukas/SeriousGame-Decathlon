using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosteOnClick : MonoBehaviour
{
    public GameObject  cameraPoste;
    public GameObject cameraDezoom;
    public GameObject       player;

    private void Update()
    {
        if (Input.touchCount > 0)
        {

            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            if(Physics2D.OverlapPoint(touchPosition))
            {
                cameraPoste.SetActive(true);
                cameraDezoom.SetActive(false);
                player.GetComponent<Unit>().enabled = false;
            }
        }
    }

    public void ReturnToDezoom()
    {
        cameraPoste.SetActive(false);
        cameraDezoom.SetActive(true);
        player.GetComponent<Unit>().enabled = true;
    }
}
