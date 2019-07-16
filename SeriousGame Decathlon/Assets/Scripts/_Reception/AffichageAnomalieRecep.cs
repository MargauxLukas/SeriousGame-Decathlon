using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AffichageAnomalieRecep : MonoBehaviour
{
    bool isOpen = false;
    bool isOpening = false;
    bool isClosing = false;

    private float swipeDifference;          //Au cas ou, on veut mettre un glisser pour refermer
    private float posY;

    public TextMeshProUGUI textAnomalieAmpoule;

    public  GameObject fondTexteAnomalie;
    private GameObject fiche;

    public CreationDePalette paletteManager;

    public void Start()
    {
        fondTexteAnomalie.SetActive(false);

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
            //Mettre la fin du niveau + bonus si le container était bien défectueux
        }
        else
        {
            //Mettre un gros malus
        }
    }

    public void ChangeText(string error)
    {
        fondTexteAnomalie.SetActive(true);
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
    }
}
