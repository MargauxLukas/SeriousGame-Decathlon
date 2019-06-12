using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFIDScan : MonoBehaviour
{
    private BoxCollider2D triggerRFID;
    private ColisScript scriptColis;
    public RFIDInfoManager infoRFID;
    private int numRFID = 0;
    private int listArtLength;

    private void Start()
    {
        triggerRFID = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("CollideRFID");
        if (collision.gameObject.tag == "Colis" && collision.gameObject.GetComponent<ColisScript>().estSecoue == true && !collision.gameObject.GetComponent<ColisScript>().hasBeenScannedByRFID && collision.gameObject.GetComponent<ColisScript>().hasBeenScannedByPistol)
        {
            scriptColis = collision.gameObject.GetComponent<ColisScript>();
            infoRFID.refIntRFID = scriptColis.colisScriptable.listArticles[0].rfid.refArticle.numeroRef;
            scriptColis.hasBeenScannedByRFID = true;

            foreach (Article item in scriptColis.colisScriptable.listArticles)
            {
                numRFID++;
            }

            infoRFID.numIntRFID = numRFID; 
        }
    }
}
