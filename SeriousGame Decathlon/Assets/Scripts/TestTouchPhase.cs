using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestTouchPhase : MonoBehaviour
{
    Vector3 cubePosition;
    void Update()
    {

        for (int i = 0; i < Input.touchCount; i++)
        {
            cubePosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
            Debug.DrawLine(Vector3.zero, cubePosition, Color.red);
            //transform.localPosition = new Vector3(cubePosition.x,cubePosition.y,0);

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        Debug.Log("TOUCH detected at : " + touch.position);
                        break;

                    case TouchPhase.Moved:
                        Debug.Log("MOVED at : " + touch.position);
                        break;

                    case TouchPhase.Ended:
                        Debug.Log("ENDED at : " + touch.position);
                        break;

                    case TouchPhase.Stationary:
                        Debug.Log("STATIONARY at : " + touch.position);
                        break;

                    case TouchPhase.Canceled:
                        Debug.Log("CANCELED detected at : " + touch.position);
                        break;
                }
            }
        }
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(cubePosition.x, cubePosition.y, 0), 0.1f);
    }
}