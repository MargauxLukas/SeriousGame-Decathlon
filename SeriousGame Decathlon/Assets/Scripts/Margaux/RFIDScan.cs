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

    public bool isActive = false;

    private void Start()
    {
        triggerRFID = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isActive)
        {
            if (collision.gameObject.tag == "Colis" && collision.gameObject.GetComponent<ColisScript>().estSecoue == true && !collision.gameObject.GetComponent<ColisScript>().hasBeenScannedByRFID && collision.gameObject.GetComponent<ColisScript>().hasBeenScannedByPistol)
            {
                scriptColis = collision.gameObject.GetComponent<ColisScript>();

                //if (scriptColis.colisScriptable.listArticles[0].rfid != null)//Vérification si RFID est nul ou pas
                
                    infoRFID.rfidComplet = scriptColis.colisScriptable.listArticles[0].rfid;
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
}
