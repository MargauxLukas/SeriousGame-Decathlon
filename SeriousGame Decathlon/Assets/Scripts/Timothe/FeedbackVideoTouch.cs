using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackVideoTouch : MonoBehaviour
{
    Color currentColor;
    public float coef;
    public float coefDeux;

    public static FeedbackVideoTouch instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y, 0);
                currentColor = Color.white;
                currentColor.a = 1;
                GetComponent<SpriteRenderer>().color = currentColor;
                transform.localScale = Vector3.one * 0.1f;
            }
        }

        if(currentColor.a >0)
        {
            currentColor.a -= Time.deltaTime * coef;
            GetComponent<SpriteRenderer>().color = currentColor;
        }
        if(transform.localScale.x < 1f)
        {
            transform.localScale += Vector3.one * Time.deltaTime * coefDeux;
        }
    }
}
