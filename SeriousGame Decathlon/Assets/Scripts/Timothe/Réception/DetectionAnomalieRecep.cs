using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionAnomalieRecep : MonoBehaviour
{
    public ScriptColisRecep colisATraiter;

    public TapisRoulantGeneral tapisGeneral;

    public GameObject gestionAnomalie;

    public bool doesDetectDimension;
    public bool doesDetectOrientation;
    public bool doesDetectPoids;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Colis")
        {
            ScriptColisRecep currentColis = collision.GetComponent<ScriptColisRecep>();
            if(doesDetectOrientation)
            {
                if (currentColis.colisScriptable.isBadOriented)
                {
                    //Afficher les feedbacks de l'anomalie
                    tapisGeneral.doesStop = true;
                }
               
            }

            if(doesDetectDimension)
            {
                if(currentColis.colisScriptable.carton.codeRef == "CBGrand")
                {
                    //Afficher les feedbacks de l'anomalie
                    tapisGeneral.doesStop = true;
                }
            }

            if(doesDetectPoids)
            {
                if(currentColis.colisScriptable.poids >= 35)
                {
                    //Afficher les feedbacks de l'anomalie
                    tapisGeneral.doesStop = true;
                }
            }
        }
    }
}
