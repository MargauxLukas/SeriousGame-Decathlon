using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionAnomalieRecep : MonoBehaviour
{
    public ScriptColisRecep colisATraiter;

    public TapisRoulantGeneral tapisGeneral;

    public GameObject gestionAnomalie;

    public GameObject avertissementZoom;

    public GameObject signalBoiteOrange;
    public GameObject signalBoiteVert;
    public GameObject signalBoiteOrangeClignotant;

    public bool doesDetectDimension;
    public bool doesDetectOrientation;
    public bool doesDetectPoids;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Colis")
        {
            ScriptColisRecep currentColis = collision.GetComponent<ScriptColisRecep>();
            signalBoiteOrange.SetActive(false);
            signalBoiteOrangeClignotant.SetActive(false);
            if (doesDetectOrientation)
            {
                if (currentColis.colisScriptable.isBadOriented)
                {
                    //Afficher les feedbacks de l'anomalie
                    tapisGeneral.doesStop = true;
                    avertissementZoom.SetActive(true);
                    avertissementZoom.GetComponent<affichageAmouleRecep>().alertLevel = 0;
                    signalBoiteOrangeClignotant.SetActive(true);
                }
               
            }

            if(doesDetectDimension)
            {
                if(currentColis.colisScriptable.carton.codeRef == "CBGrand")
                {
                    //Afficher les feedbacks de l'anomalie
                    tapisGeneral.doesStop = true;
                    avertissementZoom.SetActive(true);
                    avertissementZoom.GetComponent<affichageAmouleRecep>().alertLevel = 0;
                    signalBoiteOrangeClignotant.SetActive(true);
                }
            }

            if(doesDetectPoids)
            {
                if(currentColis.colisScriptable.poids >= 35)
                {
                    //Afficher les feedbacks de l'anomalie
                    tapisGeneral.doesStop = true;
                    avertissementZoom.SetActive(true);
                    avertissementZoom.GetComponent<affichageAmouleRecep>().alertLevel = 1;
                    signalBoiteOrange.SetActive(true);
                }
            }
        }
    }
}
