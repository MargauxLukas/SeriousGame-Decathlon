using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    /*public Joystick joystick;

    public float speed = 0.4f;
    private Touch touch;

    void Update()
    {
        for (int i = 0; i <= Input.touchCount; i++)
        {
            touch = Input.GetTouch(i);
        }
        touch = Input.GetTouch(Input.touchCount);
        if (joystick.Horizontal >= .2f)
        {
            transform.position = Vector3.right* Time.deltaTime * speed;
            Debug.Log("MOVE RIGHT");
        }
        else if(joystick.Horizontal <= -.2f)
        {
            transform.position = -Vector3.right * Time.deltaTime * speed;
            Debug.Log("MOVE LEFT");
        }

        if(joystick.Vertical >= .2f)
        {
            transform.position = Vector3.up * Time.deltaTime * speed;
            Debug.Log("MOVE UP");
        }
        else if (joystick.Vertical <= -.2f)
        {
            transform.position = -Vector3.up * Time.deltaTime * speed;
            Debug.Log("MOVE DOWN");
        }

        transform.position = touch.position;
        Debug.Log(touch.position);
    }*/

    /*void OnMouseDrag()
    {
        Vector2 cubePosition = Camera.main.ScreenToWorldPoint(touch.position);
        Debug.Log("tp = " + transform.position + " / cp = " + cubePosition);
        transform.position = cubePosition;
        //Debug.Log("Drag detected");
    }*/
}
