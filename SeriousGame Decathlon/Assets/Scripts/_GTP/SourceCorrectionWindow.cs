using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SourceCorrectionWindow : MonoBehaviour
{
    public TextMeshProUGUI textDamaged;
    public TextMeshProUGUI textMissing;
    public TextMeshProUGUI textWrong  ;

    private int nbDamaged = 0;
    private int nbMissing = 0;
    private int nbWrong   = 0;

    public void AffichageDamaged()
    {
        textDamaged.text = nbDamaged.ToString();
    }

    public void AffichageMissing()
    {
        textMissing.text = nbMissing.ToString();
    }

    public void AffichageWrong()
    {
        textWrong.text = nbWrong.ToString();
    }

    public void AddDamaged()
    {
        nbDamaged++;
        AffichageDamaged();
    }

    public void LessDamaged()
    {
        if (nbDamaged != 0)
        {
            nbDamaged--;
            AffichageDamaged();
        }
    }

    public void AddMissing()
    {
        nbMissing++;
        AffichageMissing();
    }

    public void LessMissing()
    {
        if (nbMissing != 0)
        {
            nbMissing--;
            AffichageMissing();
        }
    }

    public void AddWrong()
    {
        nbWrong++;
        AffichageWrong();
    }

    public void LessWrong()
    {
        if (nbWrong != 0)
        {
            nbWrong--;
            AffichageWrong();
        }
    }

    public void Back()
    {

    }

    public void Confirm()
    {

    }
}
