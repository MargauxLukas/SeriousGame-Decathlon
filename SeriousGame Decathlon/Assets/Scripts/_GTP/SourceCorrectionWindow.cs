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

    public ManagerColisVider mcv;

    public void AffichageDamaged()
    {
        textDamaged.text = nbDamaged.ToString();
    }

    public void AffichageMissing()
    {
        textMissing.text = nbMissing.ToString();

        Debug.Log("Test Missing 2");
        if (TutoManagerGTP.instance != null && nbMissing >= 1)
        {
            Debug.Log("Test Missing 1");
            TutoManagerGTP.instance.Manager(16);
        }
    }

    public void AffichageWrong()
    {
        textWrong.text = nbWrong.ToString();

        if(TutoManagerGTP.instance != null && nbWrong >= TutoManagerGTP.instance.correctSourceQtyWrongInputValue)
        {
            TutoManagerGTP.instance.Manager(12);
        }
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
        gameObject.SetActive(false);

        if(TutoManagerGTP.instance != null) { TutoManagerGTP.instance.Manager(14); }
    }

    public void Confirm()
    {
        mcv.aEteVerifier = true;
        nbDamaged = 0;
        nbWrong = 0;
        nbMissing = 0;
        AffichageWrong();
        AffichageDamaged();
        AffichageMissing();

        if(TutoManagerGTP.instance != null) { TutoManagerGTP.instance.Manager(13); }
    }

    public void ResetButton()
    {
        nbDamaged = 0;
        nbWrong = 0;
        nbMissing = 0;
        AffichageWrong();
        AffichageDamaged();
        AffichageMissing();
    }
}
