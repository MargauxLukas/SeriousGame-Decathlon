﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMonitor : MonoBehaviour
{
    public MiniMonitor miniMonitor;

    private  Vector2 targetPosition = new Vector2( 2.66f,1.22f);
    private Vector2 initialPosition = new Vector2(15.22f,1.22f);

    private bool isOpen         = false;                         //Le grand écran est-il ouvert ?
    public  bool monitorOpening = false;                         //Le grand écran est-il entrain de s'ouvrir ?
    private bool monitorClosing = false;

    private float        startPos;                              //Position de départ du doigt
    private float          endPos;                              //Position de fin du doigt
    private float swipeDifference;                              //Différence entre startPos et endPos

    private void Update()
    {
        if (monitorClosing)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began) { startPos = touch.position.x; }
                if (touch.phase == TouchPhase.Ended) { endPos = touch.position.x; }
                else { return; }

                swipeDifference = Mathf.Abs(startPos - endPos);
                //Debug.Log("StartPos : " + startPos + "/ endPos : " + endPos + " /diff : " + swipeDifference);
            }
        }

        if (monitorOpening) { openBigMonitor(); }                                                            //Condition pour éviter de l'ouvrir en boucle et consommé
        else
        {
            if ((endPos > startPos) && isOpen && swipeDifference > 100f)                                         //Swipe vers la droite
            {
                transform.position = Vector2.MoveTowards(transform.position, initialPosition, 1f);

                if (Vector2.Distance(transform.position, initialPosition) <= 0.2f)
                {
                    isOpen = false;
                }
            }
            else if ((endPos < startPos) && isOpen && swipeDifference > 100f)                                 //Swipe vers la gauche
            {
                return;
            }
            else { return; }
        }
    }


    /**************************************
     *  Permet d'ouvrir le grand écran    *
     **************************************/
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
