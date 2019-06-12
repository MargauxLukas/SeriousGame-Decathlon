using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMonitor : MonoBehaviour
{
    public MiniMonitor miniMonitor;

    public Vector2 targetPosition = new Vector2(2.66f,1.22f);
    private Vector2 initialPosition = new Vector2(15.22f,1.22f);
    public bool isOpen = false;
    public bool monitorOpening = false;

    private float startPos;
    private float endPos;
    private float swipeDifference;

    private void Update()
    {      
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                startPos = touch.position.x;
            }
            if(touch.phase == TouchPhase.Ended)
            {
                endPos = touch.position.x;
            }
            else
            {
                return;
            }

            swipeDifference = Mathf.Abs(startPos - endPos);
            //Debug.Log("StartPos : " + startPos + "/ endPos : " + endPos + " /diff : " + swipeDifference);
        }

        if (monitorOpening)
        {
            openBigMonitor();
        }

        if((endPos > startPos) && isOpen && swipeDifference>200f)
        {
            transform.position = Vector2.MoveTowards(transform.position, initialPosition, 1f);

            if(Vector2.Distance(transform.position, initialPosition) <= 0.2f)
            {
                isOpen = false;
                miniMonitor.isOpen = false;
            }
        }
        else if ((endPos < startPos) && isOpen && swipeDifference > 200f)
        {
            //Move to the left
            return;
        }
        else{return;}
    }

    public void openBigMonitor()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, 1f);

        if(Vector2.Distance(transform.position, targetPosition) <= 0.2f)
        {
            isOpen = true;
            monitorOpening = false;
        }
    }
}
