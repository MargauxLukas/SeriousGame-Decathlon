using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AffichageHallOfFame : MonoBehaviour
{
    public static AffichageHallOfFame instance { set; get; }

    public List<Text> nomDesVIP;
    public List<Text> scoreDesVIP;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) { instance = this; }
        else { Destroy(instance); }

        /*if (SaveLoadSystem.instance != null)
        {
            BestScoreScript newBest = SaveLoadSystem.instance.LoadBestScore();

            for(int i = 0; i < newBest.nomDesJoueurs.Count; i++)
            {
                Debug.Log(newBest.nomDesJoueurs[i]);
                nomDesVIP[i].text = newBest.nomDesJoueurs[i];
                scoreDesVIP[i].text = newBest.scoreDesJoueurs[i].ToString();
            }
        }*/

        Client.instance.RequestHallOfFame(true);
    }

    public void SetScore(string name, int score , int rank)
    {
        nomDesVIP[rank - 1].text = name;
        scoreDesVIP[rank - 1].text = score.ToString();
    }
}
