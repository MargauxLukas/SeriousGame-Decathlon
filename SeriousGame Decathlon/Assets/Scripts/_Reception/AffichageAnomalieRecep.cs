using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AffichageAnomalieRecep : MonoBehaviour
{
    bool isOpen = false;
    bool isOpening = false;
    bool isClosing = false;

    private float swipeDifference;          //Au cas ou, on veut mettre un glisser pour refermer
    private float posY;

    public TextMeshProUGUI textAnomalieGestion;

    public GameObject fondTextAnomalieDezoom;
    private GameObject fiche;

    public CreationDePalette paletteManager;

    public void Start()
    {
        fondTextAnomalieDezoom.SetActive(false);

        textAnomalieGestion.text = "";
    }

    private void Update()
    {
        if(isOpening)
        {
            Open();
        }
        else if(isClosing)
        {
            Close();
        }
        else
        {
            return;
        }
    }

    private void OnMouseDown()
    {
        /*if(!isOpen)
        {
            posY = transform.position.y + 3.2f;
            isOpening = true;
        }
        else
        {
            posY = transform.position.y - 3.2f;
            isClosing = true;
        }*/
    }

    private void Open()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, posY, transform.position.z), 1f);

        if (Vector3.Distance(transform.position, new Vector3(transform.position.x, posY, transform.position.z)) <= 0.1f)
        {
            isOpening = false;
            isOpen = true;
        }
    }

    private void Close()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, posY, transform.position.z), 1f);

        if (Vector3.Distance(transform.position, new Vector3(transform.position.x, posY, transform.position.z)) <= 0.1f)
        {
            isClosing = false;
            isOpen = false;
        }
    }

    public void ContenerReturn()
    {
        //Seulement si replier a fond
        if(paletteManager.chanceHavingAnomaliesMF >= 100 || paletteManager.nbColisTraite >= paletteManager.nbColisTotal)
        {
            if (ChargementListeColis.instance == null)
            {
                Scoring.instance.RecepBonus(100 * (paletteManager.nbColisTotal - paletteManager.nbColisTraite));
                SceneManager.LoadScene(6);
            }
            else
            {
                ChargementListeColis.instance.QuitReceptionLevel(paletteManager.nbColisTotal - paletteManager.nbColisTraite, false);
            }
        }
        else
        {
            Scoring.instance.RecepMalus(50 * (paletteManager.nbColisTotal - paletteManager.nbColisTraite) + 500);
        }
    }

    public void ChangeText(string error)
    {
        fondTextAnomalieDezoom.SetActive(true);
        switch (error)
        {
            case "badOriented":
                textAnomalieGestion.text = "Bad Oriented";
                break;
            case "dimension":
                textAnomalieGestion.text = "Dimension Out";
                break;
            case "heavy":
                textAnomalieGestion.text = "Too HEAVY";
                break;
            default:
                break;
        }
    }
}
