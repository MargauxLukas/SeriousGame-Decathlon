using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolScan : MonoBehaviour
{
    private BoxCollider2D triggerPistol;
    public ColisScript scriptColis;
    public IWayInfoManager iWayInfoManager;

    void Start()
    {
        triggerPistol = GetComponent<BoxCollider2D>();
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
}
