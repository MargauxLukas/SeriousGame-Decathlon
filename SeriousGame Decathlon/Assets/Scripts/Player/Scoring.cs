using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public static Scoring instance;

    [Header("Player")]
    public GameObject player;
    public Player playerScriptable;

    [Header("Texte Anomalie")]
    public Text errorTextZone;

    [Header("Score")]
    public int score;
    public int scoreGTP;
    public int scoreReception;
    public int scoreMultifonction;

    [Header("---")]
    public bool gotNewColis;

    private float multiplicator = 1;
    private float timeColisMaking;

    private int solveAnomalieCombo             = 0;      //Série d'anomalie résolues
    private int solveAnomalieComboWithoutMalus = 0;      //Série d'anomalie résolues sans avoir eu de Malus
    private int sendColisCombo                 = 0;      //Série de colis renvoyé
    private int sendColisComboWithoutMalus     = 0;      //Série de colis renvoyé sans avoir eu de Malus
    private int noHelp                         = 0;      //Série de colis renvoyé sans demander d'aide

    private bool lastGotColis;
    private bool tookHelp         = false;
    private bool hadMalusAnomalie = false;
    private bool hadMalusColis    = false;

    //Réception
    int comboPallier=6;
    float pointToLoseWithTime = 0;
    float recepCombo = 1;
    public float currentTime;
    int currentColisInCombo;
    public bool pauseCombo;

    public bool isReception;
    public bool isMf;
    public bool isGTP;

    public float timeForGTP;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if(ChargementListeColis.instance != null)
        {
            playerScriptable = ChargementListeColis.instance.currentPlayerScriptable;
            score = playerScriptable.score;

            scoreGTP = playerScriptable.scoreGTP;
            scoreReception = playerScriptable.scoreReception;
            scoreMultifonction = playerScriptable.scoreMultifonction;
        }
    }

    public void EndLevel()
    {
        
    }

    private void Update()
    {
        if (timeForGTP > 0)
        {
            timeForGTP -= Time.deltaTime;
        }

        if (gotNewColis)
        {
            timeColisMaking += Time.deltaTime;
        }
        lastGotColis = gotNewColis;

        if(score < 0)
        {
            score = 0;
        }
        if(scoreGTP<0)
        {
            scoreGTP = 0;
        }
        if(scoreMultifonction<0)
        {
            scoreMultifonction = 0;
        }
        if(scoreReception<0)
        {
            scoreReception = 0;
        }

        if(playerScriptable != null && score != playerScriptable.score)
        {
            playerScriptable.score = score;
            playerScriptable.scoreGTP = scoreGTP;
            playerScriptable.scoreReception = scoreReception;
            playerScriptable.scoreMultifonction = scoreMultifonction;
            if (ChargementListeColis.instance != null)
            {
                ChargementListeColis.instance.currentPlayerScriptable = playerScriptable;
            }
        }

        //Reception
        //Calcul du temps mit

        if (!pauseCombo)
        {
            currentTime += Time.deltaTime;
        }
        switch(comboPallier)
        {
            case 1:
                if (currentTime > 1f)
                {
                    comboPallier++;
                }
                break;
            case 2:
                if (currentTime > 2f)
                {
                    comboPallier++;
                }
                break;
            case 3:
                if (currentTime > 4f)
                {
                    comboPallier++;
                }
                break;
            case 4:
                if (currentTime > 8f)
                {
                    comboPallier++;
                }
                break;
            case 5:
                if (currentTime > 15f)
                {
                    comboPallier++;
                }
                break;
        }

    }

    #region GTP
    public void LosePointGTP(int amount, string text)
    {
        Debug.Log("Lose Some Point");
        scoreGTP -= amount;
        AffichageErreur(text);
    }

    public void WinPointGTP(int amount)
    {
        Debug.Log("Win Some Point");
        scoreGTP += amount;
    }

    public void BeginComboGTP(float amountOfTime)
    {
        timeForGTP = amountOfTime;
    }

    public void StopComboGTP(float amountOfPointPerSecond)
    {
        scoreGTP += Mathf.RoundToInt(timeForGTP * amountOfPointPerSecond);
        timeForGTP = 0;
    }

    #endregion

    #region Réception

    public void LosePointOnTime(int coef)
    {
        pointToLoseWithTime += Time.deltaTime*coef;
    }

    public void EndLosePointOnTime()
    {
        scoreReception -= (int)pointToLoseWithTime;
        pointToLoseWithTime = 0;
    }

    public void PauseCombo(float time)
    {
        StartCoroutine(PauseComboWait(time));
    }

    public IEnumerator PauseComboWait(float timeToWait)
    {
        pauseCombo = true;
        yield return new WaitForSeconds(timeToWait);
        pauseCombo = false;
    }

    public void RecepMalus(int valeurMalus)
    {
        Debug.Log("Malus Recep : " + valeurMalus);
        scoreReception -= valeurMalus;
        ResetComboRpcep();
    }

    public void RecepBonus(int valeurBonus)
    {
        scoreReception += valeurBonus;
    }

    public void UpCombo()
    {
        Debug.Log("Current combo : " + comboPallier + " And current nbColis for Combo : " + currentColisInCombo);
        switch (comboPallier)
        {
            case 1:
                if (currentTime > 1f)
                {
                    currentColisInCombo++;
                }
                break;
            case 2:
                if (currentTime > 2f)
                {
                    currentColisInCombo++;
                }
                break;
            case 3:
                if (currentTime > 4f)
                {
                    currentColisInCombo++;
                }
                break;
            case 4:
                if (currentTime > 8f)
                {
                    currentColisInCombo++;
                }
                break;
            default:
                currentColisInCombo++;
                break;
        }

        currentTime = 0;
        if (currentColisInCombo > 4 && comboPallier>1)
        {
            comboPallier--;
            currentColisInCombo = 0;
        }

        switch (comboPallier)
        {
            case 1:
                recepCombo = 1.4f + 0.02f * currentColisInCombo;
                break;
            case 2:
                recepCombo = 1.3f + 0.02f * currentColisInCombo;
                break;
            case 3:
                recepCombo = 1.2f + 0.02f * currentColisInCombo;
                break;
            case 4:
                recepCombo = 1.1f + 0.02f * currentColisInCombo;
                break;
            case 5:
                recepCombo = 1f + 0.02f * currentColisInCombo;
                break;
            case 6:
                recepCombo = 1f;
                break;
        }
    }

    public void RecepRenvoieColis()
    {
        scoreReception += (int)Mathf.Pow(75, recepCombo);
    }

    public void ResetComboRpcep()
    {
        comboPallier = 6;
        currentColisInCombo = 0;
    }

    #endregion

    #region Multifonction

    //MALUS
    // -15
    public void MinorPenalty()
    {
        if(!hadMalusColis)
        {
            hadMalusColis = true;
            ResetComboColisSansMalus();
        }
        if(!hadMalusAnomalie)
        {
            hadMalusAnomalie = true;
            ResetComboAnomalieSansMalus();
        }
        scoreMultifonction = scoreMultifonction - 15;
        player.GetComponent<PlayerTest>().player.scoreMultifonction = scoreMultifonction;
        
    }

    // -30
    public void MidPenalty()
    {
        if (!hadMalusColis)
        {
            hadMalusColis = true;
            ResetComboColisSansMalus();
        }
        if (!hadMalusAnomalie)
        {
            hadMalusAnomalie = true;
            ResetComboAnomalieSansMalus();
        }
        scoreMultifonction = scoreMultifonction - 30;
        player.GetComponent<PlayerTest>().player.scoreMultifonction = scoreMultifonction;
    }

    // -70
    public void MajorPenalty()
    {
        if (!hadMalusColis)
        {
            hadMalusColis = true;
            ResetComboColisSansMalus();
        }
        if (!hadMalusAnomalie)
        {
            hadMalusAnomalie = true;
            ResetComboAnomalieSansMalus();
        }
        scoreMultifonction -= 70;
        player.GetComponent<PlayerTest>().player.scoreMultifonction = scoreMultifonction;
    }

    // -150
    public void Danger()
    {

        if (!hadMalusColis)
        {
            hadMalusColis = true;
            ResetComboColisSansMalus();
        }
        if (!hadMalusAnomalie)
        {
            hadMalusAnomalie = true;
            ResetComboAnomalieSansMalus();
        }
    }

    public void WhatTheFuck()
    {
        scoreMultifonction -= (int)TimeBonus();
        if (!hadMalusColis)
        {
            hadMalusColis = true;
            ResetComboColisSansMalus();
        }
        if (!hadMalusAnomalie)
        {
            hadMalusAnomalie = true;
            ResetComboAnomalieSansMalus();
        }
    }


    public void AffichageErreur(string errorText)
    {
        if (errorTextZone != null && TutoManagerMulti.instance == null)
        {
            StopAllCoroutines();
            errorTextZone.gameObject.SetActive(true);
            errorTextZone.text = errorText;
            StartCoroutine(TempsAffichageErreur());
        }
        
        if(isMf)
        {
            if (playerScriptable.erreursMultifonction == null)
            {
                playerScriptable.erreursMultifonction = new List<string>();
                playerScriptable.nbErreursMultifonction = new List<int>();
            }
            if(!playerScriptable.erreursMultifonction.Contains(errorText))
            {
                playerScriptable.erreursMultifonction.Add(errorText);
                playerScriptable.nbErreursMultifonction.Add(1);
            }
            else
            {
                for(int i = 0; i < playerScriptable.erreursMultifonction.Count; i++)
                {
                    if (playerScriptable.erreursMultifonction[i] == errorText)
                    {
                        playerScriptable.nbErreursMultifonction[i]++;
                    }
                }
            }
            
        }
        else if(isGTP)
        {
            if (playerScriptable == null || playerScriptable.erreursGTP == null)
            {
                playerScriptable.erreursGTP = new List<string>();
                playerScriptable.nbErreursGTP = new List<int>();
            }
            if (!playerScriptable.erreursGTP.Contains(errorText))
            {
                playerScriptable.erreursGTP.Add(errorText);
                playerScriptable.nbErreursGTP.Add(1);
            }
            else
            {
                for (int i = 0; i < playerScriptable.erreursGTP.Count; i++)
                {
                    if (playerScriptable.erreursGTP[i] == errorText)
                    {
                        playerScriptable.nbErreursGTP[i]++;
                    }
                }
            }
        }
        else if(isReception)
        {
            if (playerScriptable.erreursReception == null)
            {
                playerScriptable.erreursReception = new List<string>();
                playerScriptable.nbErreursReception = new List<int>();
            }
            if (!playerScriptable.erreursReception.Contains(errorText))
            {
                playerScriptable.erreursReception.Add(errorText);
                playerScriptable.nbErreursReception.Add(1);
            }
            else
            {
                for (int i = 0; i < playerScriptable.erreursReception.Count; i++)
                {
                    if (playerScriptable.erreursReception[i] == errorText)
                    {
                        playerScriptable.nbErreursReception[i]++;
                    }
                }
            }
        }
    }

    IEnumerator TempsAffichageErreur()
    {
        yield return new WaitForSeconds(7f);
        errorTextZone.gameObject.SetActive(false);
    }

    //BONUS
    // +50
    public void solveAnomalie()
    {
        scoreMultifonction += (int)(100 * multiplicator);
        solveAnomalieCombo++;
    }

    // +200
    public void solveAnomalieWithoutMalus()
    {
        if (!hadMalusAnomalie)
        {
            scoreMultifonction += (int)(400 * multiplicator);
            solveAnomalieComboWithoutMalus++;
        }
        else
        {
            solveAnomalie();
        }
    }

    // +100
    public void sendColis()
    {
        scoreMultifonction += (int)(200 * multiplicator);
        sendColisCombo++;
        if(!tookHelp)
        {
            noHelp++;
        }
        //score += (int)TimeBonus();
    }

    // +450
    public void sendColisWithoutMalus()
    {
        if (!hadMalusColis)
        {
            scoreMultifonction += (int)(900 * multiplicator);
            sendColisComboWithoutMalus++;
            if (!tookHelp)
            {
                noHelp++;
            }
            scoreMultifonction += (int)TimeBonus();
        }
        else
        {
            sendColis();
        }
    }

    public float TimeBonus()
    {
        float calcul = (6 * 60 - timeColisMaking) * 6f; //Calcul le temps bonus (Temps mit pour le colis - 3 fois 60 secondes (3 minutes)) * 3
        timeColisMaking = 0;
        gotNewColis = false;
        if (calcul <= 0)
        {
            return 0;
        }
        return calcul; 
    }

    public void ResetCombo()
    {
        solveAnomalieCombo = 0;
        solveAnomalieComboWithoutMalus = 0;
        sendColisCombo = 0;
        sendColisComboWithoutMalus = 0;
        noHelp = 0;
    }

    public void ResetComboAnomalie()
    {
        solveAnomalieCombo = 0;
    }
    public void ResetComboAnomalieSansMalus()
    {
        solveAnomalieComboWithoutMalus = 0;
    }
    public void ResetComboColis()
    {
        sendColisCombo = 0;
    }
    public void ResetComboColisSansMalus()
    {
        sendColisComboWithoutMalus = 0;
    }

    public void ResetComboNoHelp()
    {
        noHelp = 0;
    }

    public void CalculMultiplicator()
    {
        multiplicator = 1 + ((solveAnomalieCombo * 3 + solveAnomalieComboWithoutMalus * 6 + sendColisCombo * 7 + sendColisComboWithoutMalus * 13 + noHelp * 3)/ 100);
    }

    #endregion
}
