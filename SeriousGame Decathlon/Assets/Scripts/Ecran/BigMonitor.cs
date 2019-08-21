using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMonitor : MonoBehaviour
{
    public MiniMonitor miniMonitor;

    private Vector2 targetPosition;
    private Vector2 initialPosition;

    private bool isOpen           = false;                      //Le grand écran est-il ouvert ?
    public  bool monitorOpening   = false;                      //Le grand écran est-il entrain de s'ouvrir ?
    private bool monitorClosing   = false;
    private bool closeMonitorTuto = false;
    private bool openMonitorTuto  = false;

    private float        startPos;                              //Position de départ du doigt
    private float          endPos;                              //Position de fin du doigt
    private float swipeDifference;                              //Différence entre startPos et endPos

    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = new Vector2(72.6f,initialPosition.y);
    }

    private void Update()
    {
        if (Input.touchCount > 0 && !monitorClosing && !monitorOpening && !openMonitorTuto &&!closeMonitorTuto)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) { startPos = touch.position.x; }
            if (touch.phase == TouchPhase.Ended) { endPos   = touch.position.x; }
            else { return; }

            swipeDifference = Mathf.Abs(startPos - endPos);
            //Debug.Log("StartPos : " + startPos + "/ endPos : " + endPos + " /diff : " + swipeDifference);
        }
        
        if (monitorOpening || openMonitorTuto) { OpenBigMonitor(); }                                                                //Condition pour éviter de l'ouvrir en boucle et consommé
        else
        {
            if (((endPos > startPos) && isOpen && swipeDifference > 100f) || closeMonitorTuto)                                      //Swipe vers la droite
            {
                CloseBigMonitor();

            }
            else if ((endPos < startPos) && isOpen && swipeDifference > 100f)                                   //Swipe vers la gauche
            {
                return;
            }
            else { return; }
        }
    }

    /**************************************
     *  Permet d'ouvrir le grand écran    *
     **************************************/
    public void OpenBigMonitor()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, 1f);

        if(Vector2.Distance(transform.position, targetPosition) <= 0.2f)
        {
            swipeDifference = 0;
            isOpen = true;
            monitorOpening = false;
            if (TutoManagerMulti.instance != null && !openMonitorTuto) {TutoManagerMulti.instance.Manager(3);}
            openMonitorTuto = false;
        }
    }

    public void OpenMonitorTuto()
    {
        openMonitorTuto = true;
    }

    public void CloseBigMonitor()
    {
        miniMonitor.monitorClosing = true;
        transform.position = Vector2.MoveTowards(transform.position, initialPosition, 1f);
        monitorClosing = true;

        if (Vector2.Distance(transform.position, initialPosition) <= 0.2f)
        {
            monitorClosing = false;
            if (TutoManagerMulti.instance != null && !closeMonitorTuto) { TutoManagerMulti.instance.Manager(7); }
            isOpen = false;
            closeMonitorTuto = false;
            miniMonitor.monitorClosing = false;  
        }
    }

    public void CloseMonitorTuto()
    {
        closeMonitorTuto = true;
    }
}
