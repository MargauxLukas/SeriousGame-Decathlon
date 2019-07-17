using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ZoneAffichageAnomalieFiche : MonoBehaviour
{
    [Header("Button")]
    public List<Button> listButton;

    [Header("Nb")]
    public List<TextMeshProUGUI> listNb;

    [Header("Texte")]
    public List<TextMeshProUGUI> listText;

    public Camera cameraToFollow;

    private float posXInitial;

    public void Update()
    {
        transform.position = new Vector3(transform.position.x, cameraToFollow.transform.position.y+1.3f, transform.position.z);
    }

    private void Start()
    {
        foreach (TextMeshProUGUI nb in listNb)
        {
            nb.text = "1";
        }
        foreach (TextMeshProUGUI text in listText)
        {
            text.text = "";
        }

        posXInitial = listButton[0].transform.position.x;
    }

    public IEnumerator AnomalieMove(Button button)
    {
        button.transform.position = Vector3.MoveTowards(button.transform.position, new Vector3(posXInitial + 7f, button.transform.position.y, button.transform.position.z), 1f);
        yield return new WaitForSeconds(Time.fixedDeltaTime);

        if (Vector3.Distance(button.transform.position, new Vector3(posXInitial + 7f, button.transform.position.y, button.transform.position.z)) <= 0.1f)
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
    public IEnumerator AnomalieMoveBack(Button button)
    {
        button.transform.position = Vector3.MoveTowards(button.transform.position, new Vector3(posXInitial + 0.8f, button.transform.position.y, button.transform.position.z), 1f);
        yield return new WaitForSeconds(Time.fixedDeltaTime);

        if (Vector3.Distance(button.transform.position, new Vector3(posXInitial + 0.8f, button.transform.position.y, button.transform.position.z)) <= 0.1f)
        {
            yield return new WaitForSeconds(4f);
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
