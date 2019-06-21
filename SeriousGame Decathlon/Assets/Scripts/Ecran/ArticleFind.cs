using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArticleFind : MonoBehaviour
{
    [Header("Texte en enfant")]
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;

    public void afficherSingleArticle(int pcb, int articleRef)
    {
        text1.text = pcb + " REF#" + articleRef;
        text2.text = "";
    }

    public void afficherDoubleArticle(int pcb1, int pcb2, int articleRef1, int articleRef2)
    {
        text1.text = pcb1 + " REF#" + articleRef1;
        text2.text = pcb2 + " REF#" + articleRef2;   
    }
}
