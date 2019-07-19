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
        }
    }

    public void EndLevel()
    {
        
    }

    private void Update()
    {
        if(gotNewColis)
        {
            timeColisMaking += Time.deltaTime;
        }
        lastGotColis = gotNewColis;

        if(score < 0)
        {
            score = 0;
        }

        if(playerScriptable != null && score != playerScriptable.score)
        {
            playerScriptable.score = score;
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

    #region Réception

    public void LosePointOnTime(int coef)
    {
        pointToLoseWithTime += Time.deltaTime*coef;
    }

    public void EndLosePointOnTime()
    {
        score -= (int)pointToLoseWithTime;
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
        score -= valeurMalus;
        ResetComboRpcep();
    }

    public void RecepBonus(int valeurBonus)
    {
        score += valeurBonus;
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
        score += (int)Mathf.Pow(75, recepCombo);
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
        score = score - 15;
        player.GetComponent<PlayerTest>().player.score = score;
        
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
        score = score - 30;
        player.GetComponent<PlayerTest>().player.score = score;
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
        score = score - 70;
        player.GetComponent<PlayerTest>().player.score = score;
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
        score -= (int)TimeBonus();
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
        if (errorTextZone != null && TutoManager.instance == null)
        {
            StopAllCoroutines();
            errorTextZone.gameObject.SetActive(true);
            errorTextZone.text = errorText;
            StartCoroutine(TempsAffichageErreur());
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
        score += (int)(100 * multiplicator);
        solveAnomalieCombo++;
    }

    // +200
    public void solveAnomalieWithoutMalus()
    {
        if (!hadMalusAnomalie)
        {
            score += (int)(400 * multiplicator);
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
        score += (int)(200 * multiplicator);
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
            score += (int)(900 * multiplicator);
            sendColisComboWithoutMalus++;
            if (!tookHelp)
            {
                noHelp++;
            }
            score += (int)TimeBonus();
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
