using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFIDScan : MonoBehaviour
{
    public BoxCollider2D triggerRFID;
    private ColisScript colisScript;
    public RFIDInfoManager infoRFID;
    private int numRFID = 0;
    private int listArtLength;

    private void Start()
    {
        triggerRFID = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("COlide");
        if(collision.gameObject.tag == "Colis" && collision.gameObject.GetComponent<ColisScript>().estSecoue == true && !collision.gameObject.GetComponent<ColisScript>().asBeenScanned)
        {
            colisScript = collision.gameObject.GetComponent<ColisScript>();
            infoRFID.refIntRFID = colisScript.colisScriptable.listArticles[0].rfid.refArticle.numeroRef;
            colisScript.asBeenScanned = true;

            foreach (Article item in colisScript.colisScriptable.listArticles)
            {
                numRFID++;
            }

            infoRFID.numIntRFID = numRFID; 
        }
    }
}
