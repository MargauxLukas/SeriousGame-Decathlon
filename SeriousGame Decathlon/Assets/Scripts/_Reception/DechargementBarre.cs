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
        gameObject.GetComponent<Image>().fillAmount = (float)nb / (float)palette.nbColisTotal;
        blueText.text  = nb.ToString() + "/" + palette.nbColisTotal;
        whiteText.text = nb.ToString() + "/" + palette.nbColisTotal;
    }
}
