using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFIDScan : MonoBehaviour
{
    private BoxCollider2D triggerRFID;
    private ColisScript scriptColis;
    public RFIDInfoManager infoRFID = null;
    public RFIDInfoManager infoRFID2 = null;
    public ArticleFind artFind;

    private int numRFID = 0;
    private int numRFID2 = 0;
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
                
                for(int i = 0; i< scriptColis.colisScriptable.listArticles.Count;i++)
                {                 
                    if (i == 0)
                    {
                        infoRFID.rfidComplet = scriptColis.colisScriptable.listArticles[i].rfid;
                        infoRFID.refIntRFID = scriptColis.colisScriptable.listArticles[i].rfid.refArticle.numeroRef;
                        numRFID++;
                    }
                    else
                    {
                        if (scriptColis.colisScriptable.listArticles[i].rfid == infoRFID.rfidComplet)
                        {
                            infoRFID.rfidComplet = scriptColis.colisScriptable.listArticles[i].rfid;
                            infoRFID.refIntRFID = scriptColis.colisScriptable.listArticles[i].rfid.refArticle.numeroRef;
                            numRFID++;
                        }
                        else
                        {
                            infoRFID2.rfidComplet = scriptColis.colisScriptable.listArticles[i].rfid;
                            infoRFID2.refIntRFID = scriptColis.colisScriptable.listArticles[i].rfid.refArticle.numeroRef;
                            numRFID2++;
                        }
                    }
                }
                
                scriptColis.hasBeenScannedByRFID = true;
                /*foreach (Article item in scriptColis.colisScriptable.listArticles)
                {
                    numRFID++;
                }*/

                infoRFID.numIntRFID  = numRFID ;
                infoRFID2.numIntRFID = numRFID2;

                if (numRFID2 == 0)                  // 1 seul produit
                {
                    artFind.afficherSingleArticle(numRFID, infoRFID.refIntRFID);
                }
                else                               // 2 produits
                {
                    artFind.afficherDoubleArticle(numRFID, numRFID2, infoRFID.refIntRFID, infoRFID2.refIntRFID);
                }
            }
        }
    }
}
