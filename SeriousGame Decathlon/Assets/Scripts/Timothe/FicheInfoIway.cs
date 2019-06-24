using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FicheInfoIway : MonoBehaviour
{
    public IWayInfoManager managerWay;

    private Vector2 myStartPosition;
    private Vector2 targetPosition = new Vector2(0f,-5f);

    public bool ficheIsOpening = false;
    public bool ficheIsClosing = false;
    void Start()
    {
        myStartPosition = transform.position;
    }

    private void Update()
    {
        if(ficheIsOpening)
        {
            OpenFiche();
        }

        if(ficheIsClosing)
        {
            CloseFiche();
        }
    }

    public void OpenFiche()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, 0.1f);

        if (Vector2.Distance(transform.position, targetPosition) <= 0.2f)
        {
            ficheIsOpening = false;
        }
    }

    public void CloseFiche()
    {
        transform.position = Vector2.MoveTowards(transform.position, myStartPosition, 0.1f);

        if (Vector2.Distance(transform.position, myStartPosition) <= 0.2f)
        {
            ficheIsClosing = false;
        }
    }
}
