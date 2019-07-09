using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public static Scoring instance;

    public GameObject player;
    public Player playerScriptable;
    public int score;

    private float multiplicator = 1;
    public bool gotNewColis;
    private bool lastGotColis;
    public Text errorTextZone;

    private float timeColisMaking;

    private int solveAnomalieCombo             = 0;      //Série d'anomalie résolues
    private int solveAnomalieComboWithoutMalus = 0;      //Série d'anomalie résolues sans avoir eu de Malus
    private int sendColisCombo                 = 0;      //Série de colis renvoyé
    private int sendColisComboWithoutMalus     = 0;      //Série de colis renvoyé sans avoir eu de Malus
    private int noHelp                         = 0;      //Série de colis renvoyé sans demander d'aide

    private bool tookHelp = false;
    private bool hadMalusAnomalie = false;
    private bool hadMalusColis = false;


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
    }

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
        if (errorTextZone != null)
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
        score += (int)(50 * multiplicator);
        solveAnomalieCombo++;
    }

    // +200
    public void solveAnomalieWithoutMalus()
    {
        if (!hadMalusAnomalie)
        {
            score += (int)(200 * multiplicator);
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
        score += (int)(100 * multiplicator);
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
            score += (int)(450 * multiplicator);
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
        float calcul = (5 * 60 - timeColisMaking) * 3f; //Calcul le temps bonus (Temps mit pour le colis - 3 fois 60 secondes (3 minutes)) * 3
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
        multiplicator = 1 + ((solveAnomalieCombo * 2 + solveAnomalieComboWithoutMalus * 4 + sendColisCombo * 5 + sendColisComboWithoutMalus * 10 + noHelp * 2)/ 100);
    }
}
