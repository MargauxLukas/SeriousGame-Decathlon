using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionMoniteur : MonoBehaviour
{
    private bool doesTouch;

    public DialogueManager dialManage;
    public List<Dialogue> dialogueList;

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
                    LancementDialogue(0);
                }
            }
            if (touch.phase == TouchPhase.Ended)
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
            if (hit.collider != null && hit.collider.gameObject != null && gameObject != null && hit.collider.gameObject == gameObject)
            {
                doesTouch = true;
            }
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
}
