using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffichageAnomalieRecep : MonoBehaviour
{
    public Vector2 initialPos;
    public Vector2 targetPos;

    bool isOpen = false;
    bool isOpening = false;
    bool isClosing = false;

    public float startPos;
    public float endPos;

    private float swipeDifference;

    private GameObject fiche;

    public void Start()
    {
        initialPos = transform.position;
        targetPos = new Vector2(initialPos.x ,1.38f);
    }

    private void Update()
    {
        /*if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) { startPos = touch.position.y; }
            if (touch.phase == TouchPhase.Ended) { endPos   = touch.position.y; }
            else { return; }

            swipeDifference = Mathf.Abs(startPos - endPos);
        }

        if(startPos > endPos && swipeDifference > 50f)
        {
            if(isOpen)
            {
                Close();
            }
        }*/

        if(isOpening)
        {
            Open();
        }
        else if(isClosing)
        {
            Close();
        }
        else
        {
            return;
        }
    }

    private void OnMouseDown()
    {
        if(!isOpen)
        {
            isOpening = true;
        }
        else
        {
            isClosing = true;
        }
    }

    private void Open()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, 1f);

        if (Vector2.Distance(transform.position, targetPos) <= 0.1f)
        {
            isOpening = false;
            isOpen = true;
        }
    }

    private void Close()
    {
        transform.position = Vector2.MoveTowards(transform.position, initialPos, 1f);

        if (Vector2.Distance(transform.position, initialPos) <= 0.1f)
        {
            isClosing = false;
            isOpen = false;
        }
    }
}
