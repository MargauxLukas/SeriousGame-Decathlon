using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pANNEAUeNTREPOT : MonoBehaviour
{
    public GameObject plan;
    private bool doesTouch;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touchObject();

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                plan.SetActive(false);
            }

            if (doesTouch)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    plan.SetActive(true);
                    doesTouch = false;
                }
            }
        }
    }

    void touchObject()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject != null && gameObject != null && hit.collider.gameObject == gameObject && hit.collider.gameObject.name == gameObject.name)
            {
                doesTouch = true;
            }
        }
    }
}
