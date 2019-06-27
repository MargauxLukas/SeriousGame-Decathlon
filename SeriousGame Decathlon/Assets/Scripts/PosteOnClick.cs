using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosteOnClick : MonoBehaviour
{
    public GameObject  cameraPoste;
    public GameObject cameraDezoom;
    public GameObject       player;
    public GameObject        aStar;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            if(Physics2D.OverlapPoint(touchPosition) && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                cameraPoste .SetActive(true);
                cameraDezoom.SetActive(false);
                aStar       .SetActive(false);
                player      .SetActive(false);
            }
        }
    }

    public void ReturnToDezoom()
    {
        cameraPoste .SetActive(false);
        cameraDezoom.SetActive(true);
        aStar       .SetActive(true);
        player      .SetActive(true);
    }
}
