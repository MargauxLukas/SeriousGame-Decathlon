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
    public List<string> listAnomalieDejaDetectee;
    public int nbAnomalieMax;
    private List<ZoneAffichageAnomalieFiche> zoneAffichage;

    [Header("Button")]
    public List<Button> listButton;

    [Header("Nb")]
    public List<TextMeshProUGUI> listNb;

    [Header("Texte")]
    public List<TextMeshProUGUI> listText;

    private float posXInitial;

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

            Destroy(collision.gameObject);
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

    /*******************************************************************
    *   Coroutine qui permet de déplacer l'anomalie vers la droite     *
    *******************************************************************/
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

    /*******************************************************************
    *   Coroutine qui permet de ranger l'anomalie vers la gauche       *
    *******************************************************************/
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

    /*******************************************************************
    *   Onclick() qui permet de déplacer une anomalie vers la droite   *
    *******************************************************************/
    public void AfficherAnomalie(int i)
    {
        StartCoroutine(AnomalieMove(listButton[i]));
    }
}
