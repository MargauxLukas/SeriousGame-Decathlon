using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinDuConvoyeur : MonoBehaviour
{
    [Header("AnomalieDetection")]
    public AnomalieDetection detect;

    [Header("Image - DechargementBarre")]
    public DechargementBarre dechargeBar;

    [Header("Liste Colis")]
    public List<Colis> listColisEnvoye;

    [Header("Liste Anomalie")]
    public ZoneAffichageAnomalieFiche zoneAffichage;
    public List<string> listAnomalieDejaDetectee;
    public int nbAnomalieMax;

    private void Start()
    {
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(Time.deltaTime * 2);
        dechargeBar.UpdateProgression(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Colis")
        {
            if (TutoManagerRecep.instance != null && collision.GetComponent<ScriptColisRecep>().colisScriptable.listAnomalies.Count >= 1)
            {
                Debug.Log("Affiche Anomalie");
                UpdateAffichage(collision.GetComponent<ScriptColisRecep>().colisScriptable);
                listColisEnvoye.Add(collision.GetComponent<ScriptColisRecep>().colisScriptable);
                dechargeBar.UpdateProgression(listColisEnvoye.Count);

                Destroy(collision.gameObject);
            }
            else if(TutoManagerRecep.instance == null)
            {
                detect.CheckColis(collision.GetComponent<ScriptColisRecep>().colisScriptable);
                UpdateAffichage(collision.GetComponent<ScriptColisRecep>().colisScriptable);
                listColisEnvoye.Add(collision.GetComponent<ScriptColisRecep>().colisScriptable);
                dechargeBar.UpdateProgression(listColisEnvoye.Count);

                Destroy(collision.gameObject);
            }
        }
    }

    /*******************************************************************
     *   Permet de mettre à jour l'affichage des anomalies du colis    *
     *******************************************************************/
    private void UpdateAffichage(Colis colis)
    {
        for (int j = 0; j < colis.listAnomalies.Count; j++)
        {
            int i = 0;
            for (i = 0; i < listAnomalieDejaDetectee.Count; i++)
            {
                if (listAnomalieDejaDetectee[i] == colis.listAnomalies[j])
                {
                    zoneAffichage.listNb[i].text = (int.Parse(zoneAffichage.listNb[i].text) + 1).ToString();
                    StartCoroutine(zoneAffichage.AnomalieMove(zoneAffichage.listButton[i]));
                }
            }

            if (!listAnomalieDejaDetectee.Contains(colis.listAnomalies[j]) && listAnomalieDejaDetectee.Count <= nbAnomalieMax)
            {
                zoneAffichage.listText[i].text = colis.listAnomalies[j];
                StartCoroutine(zoneAffichage.AnomalieMove(zoneAffichage.listButton[i]));
                if (listAnomalieDejaDetectee == null || listAnomalieDejaDetectee.Count <= 0)
                {
                    listAnomalieDejaDetectee = new List<string>();
                }
                listAnomalieDejaDetectee.Add(colis.listAnomalies[j]);
            }
        }
    }
}
