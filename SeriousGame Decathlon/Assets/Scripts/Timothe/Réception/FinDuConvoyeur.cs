using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinDuConvoyeur : MonoBehaviour
{
    public AnomalieDetection detect;

    public DechargementBarre dechargeBar;

    public List<Colis> listColisEnvoye;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Colis")
        {
            detect.CheckColis(collision.GetComponent<ScriptColisRecep>().colisScriptable);
            listColisEnvoye.Add(collision.GetComponent<ScriptColisRecep>().colisScriptable);
            dechargeBar.UpdateProgression(listColisEnvoye.Count);
            //Afficher les anomalies
            Destroy(collision.gameObject);
        }
    }
}
