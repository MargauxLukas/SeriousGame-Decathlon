using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AffichageAnomalieRecep : MonoBehaviour
{
    public Vector3 initialPos;
    public Vector3 targetPos;

    bool isOpen = false;
    bool isOpening = false;
    bool isClosing = false;

    public float startPos;
    public float endPos;

    private float swipeDifference;

    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;

    public TextMeshProUGUI textAnomalieAmpoule;

    private GameObject fiche;

    public CreationDePalette paletteManager;

    public void Start()
    {
        initialPos = transform.position;
        targetPos = new Vector3(initialPos.x ,1.38f, initialPos.z);

        text1.text               = "";
        text2.text               = "";
        text3.text               = "";
        textAnomalieAmpoule.text = "";
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
        if(!isOpen)
        {
            isOpening = true;
        }
        else
        {
            isClosing = true;
        }
    }

    private void Open()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, 1f);

        if (Vector3.Distance(transform.position, targetPos) <= 0.1f)
        {
            isOpening = false;
            isOpen = true;
        }
    }

    private void Close()
    {
        transform.position = Vector3.MoveTowards(transform.position, initialPos, 1f);

        if (Vector3.Distance(transform.position, initialPos) <= 0.1f)
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
            //Mettre la fin du niveau + bonus si le container était bien défectueux
        }
        else
        {
            //Mettre un gros malus
        }
    }

    public void ChangeText(string error)
    {
        switch(error)
        {
            case "badOriented":
                textAnomalieAmpoule.text = "Bad Oriented";
                break;
            case "dimension":
                textAnomalieAmpoule.text = "Dimension Out";
                break;
            case "heavy":
                textAnomalieAmpoule.text = "Too HEAVY";
                break;
            default:
                break;
        }
        //Recuperation des anomalies detecté
        //A récupéré sur le DetectionAnomalie sur le collider FinDuConvoyeur
    }
}
