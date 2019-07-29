using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArticleAffichage : MonoBehaviour
{
    public TextMeshProUGUI TName;
    public TextMeshProUGUI TTarget;
    public TextMeshProUGUI TActual;

    public void Start()
    {
        TName.text   = "";
        TTarget.text = "";
        TActual.text = "";
    }
}
