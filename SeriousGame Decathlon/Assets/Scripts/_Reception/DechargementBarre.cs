using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DechargementBarre : MonoBehaviour
{
    public TextMeshProUGUI blueText;
    public TextMeshProUGUI whiteText;

    public CreationDePalette palette;

    public void UpdateProgression(int nb)
    {
        gameObject.GetComponent<Image>().fillAmount = ((float)palette.nbColisTotal - (float)nb) / (float)palette.nbColisTotal;
        blueText.text  = ((palette.nbColisTotal - nb) * 100 / palette.nbColisTotal).ToString();
        whiteText.text = ((palette.nbColisTotal - nb) * 100 / palette.nbColisTotal).ToString();
    }
}
