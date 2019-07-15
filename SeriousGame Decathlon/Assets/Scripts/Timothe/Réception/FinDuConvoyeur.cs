using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinDuConvoyeur : MonoBehaviour
{
    public AnomalieDetection detect;

    public DechargementBarre dechargeBar;

    public List<Colis> listColisEnvoye;

    public List<string> listAnomalieDejaDetectee;
    public GameObject menuDeroulant;

    public int nbAnomalieMax;

    public GameObject prefabText;
    private List<ZoneAffichageAnomalieFiche> zoneAffichage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Colis")
        {
            detect.CheckColis  (collision.GetComponent<ScriptColisRecep>().colisScriptable);
            UpdateAffichage    (collision.GetComponent<ScriptColisRecep>().colisScriptable);
            listColisEnvoye.Add(collision.GetComponent<ScriptColisRecep>().colisScriptable);
            dechargeBar.UpdateProgression(listColisEnvoye.Count);
            //Afficher les anomalies
            Destroy(collision.gameObject);
        }
    }

    private void UpdateAffichage(Colis colis)
    {
        for (int j = 0; j < colis.listAnomalies.Count; j++)
        {
            Debug.Log("Test Affiche Anomalie2");
            int i = 0;
            for (i = 0; i < listAnomalieDejaDetectee.Count; i++)
            {
                Debug.Log("Test Affiche Anomalie3");
                Debug.Log(listAnomalieDejaDetectee[i]);
                Debug.Log(colis.listAnomalies[j]);
                if (listAnomalieDejaDetectee[i] == colis.listAnomalies[j])
                {
                    zoneAffichage[i].zoneNombreAnomaliePresente.text = (int.Parse(zoneAffichage[i].zoneNombreAnomaliePresente.text) + 1).ToString();
                    zoneAffichage[i].zoneNombreAnomaliePresente.CrossFadeAlpha(255f, 0f, false);
                    zoneAffichage[i].zoneAffichageAnomalie     .CrossFadeAlpha(255f, 0f, false);
                }
            }

            if (!listAnomalieDejaDetectee.Contains(colis.listAnomalies[j]) && listAnomalieDejaDetectee.Count <= nbAnomalieMax)
            {
                Debug.Log("Test Affiche Anomalie4");
                if(zoneAffichage == null || zoneAffichage.Count<=0)
                {
                    zoneAffichage = new List<ZoneAffichageAnomalieFiche>();
                }
                zoneAffichage.Add(Instantiate(prefabText, menuDeroulant.transform).GetComponent<ZoneAffichageAnomalieFiche>());
                zoneAffichage[i].zoneAffichageAnomalie.text = colis.listAnomalies[j];
                if (listAnomalieDejaDetectee == null || listAnomalieDejaDetectee.Count <= 0)
                {
                    listAnomalieDejaDetectee = new List<string>();
                }
                listAnomalieDejaDetectee.Add(colis.listAnomalies[j]);
            }
        }
    }
}
