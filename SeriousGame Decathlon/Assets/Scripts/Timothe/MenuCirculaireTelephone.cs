using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCirculaireTelephone : MonoBehaviour
{
    public Image circleImage;
    private Vector2 startPosition;
    private Vector2 circlePosition;
    public int itemNumber;
    private int currentItem;
    public bool isOpen;
    private bool canOpen = true;
    public float timeBeforeOpen;
    private float timeTouched;

    // Start is called before the first frame update
    void Start()
    {
        circlePosition = Vector2.zero;
        circleImage.fillAmount = 1f / itemNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            if (touch.phase == TouchPhase.Began)
            {
                timeTouched = 0;
                startPosition = transform.position;
            }
            else if (Vector3.Distance(startPosition, touchPosition) > 1.5f && timeTouched < timeBeforeOpen)
            {
                canOpen = false;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                canOpen = true;
            }

            timeTouched += Time.deltaTime;

            if (timeTouched > timeBeforeOpen && canOpen)
            {
                circleImage.gameObject.SetActive(true);
                circleImage.fillAmount = 1f / itemNumber;

                switch (touch.phase)
                {
                    case TouchPhase.Moved:
                        if (Vector2.Distance(startPosition, touchPosition) > 1f)
                        {
                            currentItem = GetItemFromAngle(GetAngle(startPosition, touchPosition));
                        }
                        else
                        {
                            currentItem = -1;
                        }
                        break;
                    case TouchPhase.Ended:
                        circleImage.gameObject.SetActive(false);
                        PickInventory(currentItem);
                        break;
                }
            }
        }
    }

    void touchObject()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
            if (hit.collider.gameObject == gameObject)
            {
                Debug.Log("Touched it");
            }
        }
    }

    float GetAngle(Vector2 startPos, Vector2 endPos)
    {
        float angle = 0;
        Vector2 baseAngle = Vector2.zero;
        Vector2 direction = endPos - startPos;

        angle = (Mathf.Atan2(-1 - circlePosition.y, 0 - circlePosition.x) - Mathf.Atan2(endPos.y - circlePosition.y, endPos.x - circlePosition.x)) * Mathf.Rad2Deg;

        if (angle < 0)
        {
            angle += 360;
        }
        return angle;
    }

    int GetItemFromAngle(float angle)
    {
        int itemNb = 0;

        itemNb = (int)(angle / (360 / itemNumber));

        circleImage.transform.eulerAngles = new Vector3(0, 180, (360 / itemNumber) * itemNb);
        return itemNb;
    }

    void PickInventory(int nb)
    {
        switch (nb)
        {
            case 0:
                TellSomething("Allo");
                break;
            case 1:
                TellSomething("Bonjour");
                break;
            case 2:
                TellSomething("Salut");
                break;
            case 3:
                TellSomething("Hello");
                break;
            case 4:
                TellSomething("Ohaio");
                break;
        }
    }

    void TellSomething(string texte)
    {
        Debug.Log(texte);
    }
}
