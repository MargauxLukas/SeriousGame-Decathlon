using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AffichageScoreFin : MonoBehaviour
{
    public Text affichageScore;
    public Text affichageNom;
    public Text affichageDate;

    public List<Text> textScoreParProcess;
    public List<Transform> lesDiffContents;
    public GameObject affichageDesErreurs;

    public GameObject ongletMF;
    public GameObject ongletRecep;
    public GameObject ongletGTP;


    // Start is called before the first frame update
    void Start()
    {
        if(ChargementListeColis.instance != null)
        {
            ChargementListeColis.instance.currentPlayerScriptable.score = ChargementListeColis.instance.currentPlayerScriptable.scoreMultifonction + ChargementListeColis.instance.currentPlayerScriptable.scoreGTP + ChargementListeColis.instance.currentPlayerScriptable.scoreReception;
            if (SaveLoadSystem.instance!=null)
            {
                SaveLoadSystem.instance.SaveScore(ChargementListeColis.instance.currentPlayerScriptable);
            }

            affichageScore.text = ChargementListeColis.instance.currentPlayerScriptable.score.ToString();
            affichageNom.text = ChargementListeColis.instance.currentPlayerScriptable.name.ToString();
            affichageDate.text = ChargementListeColis.instance.currentPlayerScriptable.date.ToString();

            Client.instance.SendMyRank(ChargementListeColis.instance.currentPlayerScriptable.score, ChargementListeColis.instance.currentPlayerScriptable.name);

            Player currentPlayer = ChargementListeColis.instance.currentPlayerScriptable;
            textScoreParProcess[0].text = currentPlayer.scoreMultifonction.ToString();
            textScoreParProcess[1].text = currentPlayer.scoreReception.ToString();
            textScoreParProcess[2].text = currentPlayer.scoreGTP.ToString();

            for(int i = 0; i < currentPlayer.erreursMultifonction.Count; i++)
            {
                GameObject erreurActuelle = Instantiate(affichageDesErreurs, lesDiffContents[0], false);
                int nbChild = 0;
                foreach(Transform child in erreurActuelle.transform)
                {
                    nbChild++;
                    if(child.GetComponent<Text>()!=null)
                    {
                        if(nbChild == 1)
                        {
                            child.GetComponent<Text>().text = currentPlayer.erreursMultifonction[i];
                        }
                        else if(nbChild == 2)
                        {
                            child.GetComponent<Text>().text = currentPlayer.nbErreursMultifonction[i].ToString();
                        }
                    }
                }
            }

            for (int i = 0; i < currentPlayer.erreursReception.Count; i++)
            {
                GameObject erreurActuelle = Instantiate(affichageDesErreurs, lesDiffContents[1], false);
                int nbChild = 0;
                foreach (Transform child in erreurActuelle.transform)
                {
                    nbChild++;
                    if (child.GetComponent<Text>() != null)
                    {
                        if (nbChild == 1)
                        {
                            child.GetComponent<Text>().text = currentPlayer.erreursReception[i];
                        }
                        else if (nbChild == 2)
                        {
                            child.GetComponent<Text>().text = currentPlayer.nbErreursReception[i].ToString();
                        }
                    }
                }
            }

            for (int i = 0; i < currentPlayer.erreursGTP.Count; i++)
            {
                GameObject erreurActuelle = Instantiate(affichageDesErreurs, lesDiffContents[2], false);
                int nbChild = 0;
                foreach (Transform child in erreurActuelle.transform)
                {
                    nbChild++;
                    if (child.GetComponent<Text>() != null)
                    {
                        if (nbChild == 1)
                        {
                            child.GetComponent<Text>().text = currentPlayer.erreursGTP[i];
                        }
                        else if (nbChild == 2)
                        {
                            child.GetComponent<Text>().text = currentPlayer.nbErreursGTP[i].ToString();
                        }
                    }
                }
            }

            //SaveLoadSystem.instance.SaveBestScore(ChargementListeColis.instance.currentPlayerScriptable.score, ChargementListeColis.instance.currentPlayerScriptable.name);
            //SaveLoadSystem.instance.SaveScore(ChargementListeColis.instance.currentPlayerScriptable);
        }
    }

    public void OpenMF()
    {
        ongletMF.SetActive(true);
        ongletGTP.SetActive(false);
        ongletRecep.SetActive(false);
    }
    public void OpenRecep()
    {
        ongletMF.SetActive(false);
        ongletGTP.SetActive(false);
        ongletRecep.SetActive(true);
    }
    public void OpenGTP()
    {
        ongletMF.SetActive(false);
        ongletGTP.SetActive(true);
        ongletRecep.SetActive(false);
    }
}
