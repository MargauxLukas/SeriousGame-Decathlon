using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolScan : MonoBehaviour
{
    private bool isMoving;
    private Vector3 pistolInitPos;

    private BoxCollider2D triggerPistol;
    public ColisScript scriptColis;
    public IWayInfoManager iWayInfoManager;

    void Start()
    {
        pistolInitPos = transform.position;
        triggerPistol = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            touchObject();

            if (isMoving)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    transform.position = new Vector3(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)).x, Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)).y, 0);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    transform.position = pistolInitPos;
                }
            }
            
        }
        else
        {
            isMoving = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("CollidePistol");
        if (collision.gameObject.tag == "Colis" && !collision.gameObject.GetComponent<ColisScript>().hasBeenScannedByPistol)
        {
            iWayInfoManager.refIntIWay = scriptColis.colisScriptable.wayTicket.refArticle.numeroRef;
            iWayInfoManager.pcbIntIWay = scriptColis.colisScriptable.wayTicket.PCB;
            scriptColis.hasBeenScannedByPistol = true;
        }
    }

    void touchObject()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
            if (hit.collider.gameObject != null && hit.collider.gameObject == gameObject)
            {
                isMoving = true;
            }
        }
    }
}
