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
    public bool menuIsOpen;
    private bool menuCanOpen = true;
    public float timeBeforeOpen;
    private float timeTouched;

    private bool doesTouch;

    public DialogueManager dialManage;
    public List<Dialogue> dialogueList;

    // Start is called before the first frame update
    void Start()
    {
        circlePosition = Vector2.zero;
        circleImage.fillAmount = 1f / itemNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && !dialManage.dialogueGlobal.activeSelf)
        {
            Touch touch = Input.GetTouch(0);
            TouchObject();

            if (doesTouch)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0;

                if (touch.phase == TouchPhase.Began)
                {
                    currentItem = -1;
                    timeTouched = 0;
                    startPosition = touchPosition;
                    circlePosition = transform.position;
                }
                else if (Vector3.Distance(startPosition, touchPosition) > 1f && timeTouched < timeBeforeOpen)
                {
                    menuCanOpen = false;
                    menuIsOpen = false;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    menuCanOpen = true;
                    menuIsOpen = false;
                }

                timeTouched += Time.deltaTime;

                if (timeTouched > timeBeforeOpen && menuCanOpen)
                {
                    menuIsOpen = true;
                    circleImage.transform.parent.gameObject.SetActive(true);
                    circleImage.transform.parent.gameObject.transform.position = transform.position;
                    circleImage.fillAmount = 1f / itemNumber;

                    if (touch.phase == TouchPhase.Moved)
                    {
                        if (Vector2.Distance(startPosition, touchPosition) > 1f)
                        {
                            currentItem = GetItemFromAngle(GetAngle(startPosition, touchPosition));
                        }
                        else
                        {
                            currentItem = -1;
                        }
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        menuCanOpen = true;
                        menuIsOpen = false;
                        circleImage.transform.parent.gameObject.SetActive(false);
                        if (currentItem > -1)
                        {
                            PickInventory(currentItem);
                        }
                        return;
                    }
                }
            }

            if(touch.phase == TouchPhase.Ended)
            {
                doesTouch = false;
            }
        }
    }

    void TouchObject()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
            if (hit.collider.gameObject != null && hit.collider.gameObject == gameObject)
            {
                doesTouch = true;
            }
        }
    }

    float GetAngle(Vector2 startPos, Vector2 endPos)
    {
        float angle = 0;
        Vector2 baseAngle = Vector2.zero;
        Vector2 direction = endPos - startPos;

        angle = (Mathf.Atan2(0 - circlePosition.y, 1 - circlePosition.x) - Mathf.Atan2(endPos.y - circlePosition.y, endPos.x - circlePosition.x)) * Mathf.Rad2Deg;
        if (angle < 0)
        {
            angle += 360;
        }
        return angle;
    }

    int GetItemFromAngle(float angle)
    {
        int itemNb = 0;

        itemNb = (int)((angle + (360 / 5)) / (360 / itemNumber));

        circleImage.transform.eulerAngles = new Vector3(0, 180, (360 / itemNumber) * itemNb);
        return itemNb;
    }

    void PickInventory(int nb)
    {
        switch (nb)
        {
            case 1:
                LancementDialogue(0);
                break;
            case 2:
                LancementDialogue(1);
                break;
            case 0:
                LancementDialogue(2);
                break;
        }
    }

    public void LancementDialogue(int numDial)
    {
        Debug.Log("Dialogue : " + numDial);
        dialManage.dialogueGlobal.SetActive(true);
        dialManage.isDialogueOpen = true;
        dialManage.actualDialogue = dialogueList[numDial];
        doesTouch = false;
    }

    void TellSomething(int texte)
    {
        Debug.Log(texte);
    }

    void CallSomeone()
    {
        //En variable d'entrée : Le mec avec qui on veut dialoguer
        //Début du dialogue
    }

}
