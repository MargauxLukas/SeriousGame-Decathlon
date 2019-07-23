﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartonVide : MonoBehaviour
{
    private bool doesTouch;

    private Vector3 startPosition;

    public CartonVideLink cvl;

    public void Start()
    {
        startPosition = transform.position;    
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchCarton();

            if(doesTouch)
            {
                transform.position = new Vector3(Camera.main.ScreenToWorldPoint(touch.position).x, Camera.main.ScreenToWorldPoint(touch.position).y, 0);
            }

            if(touch.phase == TouchPhase.Ended)
            {
                transform.position = startPosition;
                doesTouch = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log(collision.gameObject.name);
                if (collision.gameObject.name == "Tapis1" && cvl.isFree1)
                {
                    startPosition = new Vector3(62.40f, -3.20f, 0f);
                    cvl.isFree1 = false;
                }
                else if (collision.gameObject.name == "Tapis2" && cvl.isFree2)
                {
                    startPosition = new Vector3(65.5f, -3.20f, 0f);
                    cvl.isFree2 = false;
                }
                else if (collision.gameObject.name == "Tapis3" && cvl.isFree3)
                {
                    startPosition = new Vector3(68.40f, -3.20f, 0f);
                    cvl.isFree3 = false;
                }

                transform.position = startPosition;
            }
        }
    }

    void touchCarton()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
            if (hit.collider.gameObject != null && gameObject != null && hit.collider.gameObject == gameObject)
            {
                doesTouch = true;
            }
        }
    }
}
