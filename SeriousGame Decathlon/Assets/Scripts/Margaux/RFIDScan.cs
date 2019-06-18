using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFIDScan : MonoBehaviour
{
    private ColisScript scriptColis;

    [Header("RFIDInfoManager")]
    public RFIDInfoManager infoRFID  = null;
    public RFIDInfoManager infoRFID2 = null;

    [Header("ListArticle dans RecountTab")]
    public ArticleFind artFind;

    private int numRFID   = 0;
    private int numRFID2  = 0;

    public bool isActive = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isActive)
        {
            if (collision.gameObject.tag == "Colis" &&  collision.gameObject.GetComponent<ColisScript>().estSecoue == true 
                                                    && !collision.gameObject.GetComponent<ColisScript>().hasBeenScannedByRFID 
                                                    &&  collision.gameObject.GetComponent<ColisScript>().hasBeenScannedByPistol)
            {
                scriptColis = collision.gameObject.GetComponent<ColisScript>();

                //if (scriptColis.colisScriptable.listArticles[0].rfid != null)//Vérification si RFID est nul ou pas
                
                for(int i = 0; i< scriptColis.colisScriptable.listArticles.Count;i++)
                {                 
                    if (i == 0)                                                                                                     //Le premier me sert de référence pour savoir si les autres articles sont les mêmes.
                    {
                        infoRFID.rfidComplet = scriptColis.colisScriptable.listArticles[i].rfid;
                        infoRFID.refIntRFID = scriptColis.colisScriptable.listArticles[i].rfid.refArticle.numeroRef;
                        numRFID++;
                    }
                    else
                    {
                        if (scriptColis.colisScriptable.listArticles[i].rfid == infoRFID.rfidComplet)                               //Si pareil que référence, je range là.
                        {
                            infoRFID.rfidComplet = scriptColis.colisScriptable.listArticles[i].rfid                     ;
                            infoRFID.refIntRFID  = scriptColis.colisScriptable.listArticles[i].rfid.refArticle.numeroRef;
                            numRFID++;
                        }
                        else                                                                                                        //Sinon, je le range ici.
                        {
                            infoRFID2.rfidComplet = scriptColis.colisScriptable.listArticles[i].rfid                     ;
                            infoRFID2.refIntRFID  = scriptColis.colisScriptable.listArticles[i].rfid.refArticle.numeroRef;
                            numRFID2++;
                        }
                    }
                }
                
                scriptColis.hasBeenScannedByRFID = true;

                infoRFID.numIntRFID  = numRFID ;
                infoRFID2.numIntRFID = numRFID2;

                if (numRFID2 == 0) { artFind.afficherSingleArticle(numRFID,           infoRFID.refIntRFID                      ); }           // 1 seul produit
                else               { artFind.afficherDoubleArticle(numRFID, numRFID2, infoRFID.refIntRFID, infoRFID2.refIntRFID); }           // 2 produits
            }
        }
    }
}
