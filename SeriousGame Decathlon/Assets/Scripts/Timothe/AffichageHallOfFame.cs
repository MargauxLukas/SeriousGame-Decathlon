using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AffichageHallOfFame : MonoBehaviour
{
    public float coef;
    public List<GameObject> listHoF;

    public List<Text> nomDesVIP;
    public List<Text> scoreDesVIP;

    public List<Text> nomDesVipMF;
    public List<Text> scoreDesVipMF;

    public List<Text> nomDesVipGTP;
    public List<Text> scoreDesVipGTP;

    public List<Text> nomDesVipRecep;
    public List<Text> scoreDesVipRecep;

    // Start is called before the first frame update
    void Start()
    {
        if (SaveLoadSystem.instance != null)
        {
            BestScoreScript newBest = SaveLoadSystem.instance.LoadBestScore();
            BestScoreScript newBestMF = SaveLoadSystem.instance.LoadBestScoreMF();
            BestScoreScript newBestGTP = SaveLoadSystem.instance.LoadBestScoreGTP();
            BestScoreScript newBestRecep = SaveLoadSystem.instance.LoadBestScoreRecep();

            for (int i = 0; i < newBest.nomDesJoueurs.Count; i++)
            {
                Debug.Log(newBest.nomDesJoueurs[i]);
                nomDesVIP[i].text = newBest.nomDesJoueurs[i];
                scoreDesVIP[i].text = newBest.scoreDesJoueurs[i].ToString();
            }

            if(newBestMF!=null)
            {
                for (int i = 0; i < newBestMF.nomDesJoueurs.Count; i++)
                {
                    Debug.Log(newBestMF.nomDesJoueurs[i]);
                    scoreDesVipMF[i].text = newBestMF.nomDesJoueurs[i];
                    nomDesVipMF[i].text = newBestMF.scoreDesJoueurs[i].ToString();
                }
            }

            if (newBestGTP != null)
            {
                for (int i = 0; i < newBestGTP.nomDesJoueurs.Count; i++)
                {
                    Debug.Log(newBestGTP.nomDesJoueurs[i]);
                    nomDesVipGTP[i].text = newBestGTP.nomDesJoueurs[i];
                    scoreDesVipGTP[i].text = newBestGTP.scoreDesJoueurs[i].ToString();
                }
            }

            if (newBestRecep != null)
            {
                for (int i = 0; i < newBestRecep.nomDesJoueurs.Count; i++)
                {
                    Debug.Log(newBestRecep.nomDesJoueurs[i]);
                    nomDesVipRecep[i].text = newBestRecep.nomDesJoueurs[i];
                    scoreDesVipRecep[i].text = newBestRecep.scoreDesJoueurs[i].ToString();
                }
            }
        }
    }

    public void MoveMenu(bool isSupp)
    {
        float distance = 0;
        GameObject gm = null;
        if(isSupp)
        {
            for(int i = 0; i < listHoF.Count; i++)
            {
                listHoF[i].transform.position += new Vector3(1, 0, 0) * coef;
                if (Vector3.Distance(listHoF[i].transform.position, transform.position) > distance)
                {
                    distance = Vector3.Distance(listHoF[i].transform.position, transform.position);
                    gm = listHoF[i];
                }
            }
            gm.transform.position -= new Vector3(1, 0, 0) * coef * 4;
        }
        else
        {
            for (int i = 0; i < listHoF.Count; i++)
            {
                listHoF[i].transform.position -= new Vector3(1, 0, 0) * coef;
                if (Vector3.Distance(listHoF[i].transform.position, transform.position) > distance)
                {
                    distance = Vector3.Distance(listHoF[i].transform.position, transform.position);
                    gm = listHoF[i];
                }
            }
            gm.transform.position += new Vector3(1, 0, 0) * coef * 4;
        }
    }
}
