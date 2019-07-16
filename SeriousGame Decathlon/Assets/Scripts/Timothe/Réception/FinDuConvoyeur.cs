using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinDuConvoyeur : MonoBehaviour
{
    public AnomalieDetection detect;

    public DechargementBarre dechargeBar;

    public List<Colis> listColisEnvoye;

    public List<string> listAnomalieDejaDetectee;
    //public GameObject menuDeroulant;

    public int nbAnomalieMax;

    public GameObject prefabText;
    private List<ZoneAffichageAnomalieFiche> zoneAffichage;

    private float posXInitial;


    public List<Button> listButton;
    public List<TextMeshProUGUI> listNb;
    public List<TextMeshProUGUI> listText;

    private void Start()
    {
        foreach(TextMeshProUGUI nb in listNb)
        {
            nb.text = "1";
        }
        foreach (TextMeshProUGUI text in listText)
        {
            text.text = "";
        }

        posXInitial = listButton[0].transform.position.x;
    }

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
            int i = 0;
            for (i = 0; i < listAnomalieDejaDetectee.Count; i++)
            {
                if (listAnomalieDejaDetectee[i] == colis.listAnomalies[j])
                {
                    listNb[i].text = (int.Parse(listNb[i].text) + 1).ToString();
                    StartCoroutine(AnomalieMove(listButton[i]));
                }
            }

            if (!listAnomalieDejaDetectee.Contains(colis.listAnomalies[j]) && listAnomalieDejaDetectee.Count <= nbAnomalieMax)
            {
                if(zoneAffichage == null || zoneAffichage.Count<=0)
                {
                    zoneAffichage = new List<ZoneAffichageAnomalieFiche>();
                }
                listText[i].text = colis.listAnomalies[j];
                StartCoroutine(AnomalieMove(listButton[i]));
                if (listAnomalieDejaDetectee == null || listAnomalieDejaDetectee.Count <= 0)
                {
                    listAnomalieDejaDetectee = new List<string>();
                }
                listAnomalieDejaDetectee.Add(colis.listAnomalies[j]);
            }
        }
    }

    IEnumerator AnomalieMove(Button button)
    {
        button.transform.position = Vector3.MoveTowards(button.transform.position, new Vector3(posXInitial+7f, button.transform.position.y, button.transform.position.z),1f);
        yield return new WaitForSeconds(Time.fixedDeltaTime);

        if (Vector3.Distance(button.transform.position, new Vector3(posXInitial + 7f, button.transform.position.y, button.transform.position.z)) <= 0.2f)
        {
            yield return new WaitForSeconds(4f);
            StartCoroutine(AnomalieMoveBack(button));
        }
        else
        {
            StartCoroutine(AnomalieMove(button));
        }
    }

    IEnumerator AnomalieMoveBack(Button button)
    {
        button.transform.position = Vector3.MoveTowards(button.transform.position, new Vector3(posXInitial+0.8f, button.transform.position.y, button.transform.position.z), 1f);
        yield return new WaitForSeconds(Time.fixedDeltaTime);

        if (Vector3.Distance(button.transform.position, new Vector3(posXInitial+0.8f, button.transform.position.y, button.transform.position.z)) <= 0.2f)
        {
            yield return new WaitForSeconds(4f);
            StopCoroutine(AnomalieMoveBack(button));
        }
        else
        {
            StartCoroutine(AnomalieMoveBack(button));
        }
    }

    public void AfficherAnomalie(int i)
    {
        StartCoroutine(AnomalieMove(listButton[i]));
    }
}
