using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AffichageHallOfFame : MonoBehaviour
{
    public List<Text> nomDesVIP;
    public List<Text> scoreDesVIP;

    // Start is called before the first frame update
    void Start()
    {
        if (SaveLoadSystem.instance != null)
        {
            BestScoreScript newBest = SaveLoadSystem.instance.LoadBestScore();

            for(int i = 0; i < newBest.nomDesJoueurs.Count; i++)
            {
                Debug.Log(newBest.nomDesJoueurs[i]);
                nomDesVIP[i].text = newBest.nomDesJoueurs[i];
                scoreDesVIP[i].text = newBest.scoreDesJoueurs[i].ToString();
            }
        }
    }
}
