using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AffichageScoreFin : MonoBehaviour
{
    public Text affichageScore;
    public Text affichageNom;
    public Text affichageDate;

    // Start is called before the first frame update
    void Start()
    {
        if(ChargementListeColis.instance != null)
        {
            affichageScore.text = ChargementListeColis.instance.currentPlayerScriptable.score.ToString();
            affichageNom.text = ChargementListeColis.instance.currentPlayerScriptable.name.ToString();
            affichageDate.text = ChargementListeColis.instance.currentPlayerScriptable.date.ToString();

            SaveLoadSystem.instance.SaveBestScore(ChargementListeColis.instance.currentPlayerScriptable.score, ChargementListeColis.instance.currentPlayerScriptable.name);
        }
    }
}
