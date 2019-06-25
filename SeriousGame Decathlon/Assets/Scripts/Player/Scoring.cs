using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public static Scoring instance;

    public GameObject player;
    public int score;

    private int solveAnomalieCombo             = 0;      //Série d'anomalie résolues
    private int solveAnomalieComboWithoutMalus = 0;      //Série d'anomalie résolues sans avoir eu de Malus
    private int sendColisCombo                 = 0;      //Série de colis renvoyé
    private int sendColisComboWithoutMalus     = 0;      //Série de colis renvoyé sans avoir eu de Malus
    private int noHelp                         = 0;      //Série de colis renvoyé sans demander d'aide

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
    }


    //MALUS
    // -5
    public void MinorPenalty()
    {
        score = score - 5;
        player.GetComponent<PlayerTest>().player.score = score;
    }

    // -10
    public void MidPenalty()
    {
        score = score - 10;
        player.GetComponent<PlayerTest>().player.score = score;
    }

    // -15
    public void MajorPenalty()
    {
        score = score - 15;
        player.GetComponent<PlayerTest>().player.score = score;
    }

    // -50
    public void Danger()
    {

    }

    public void WhatTheFuck()
    {

    }


    //BONUS
    // +50
    public void solveAnomalie()
    {

    }

    // +250
    public void solveAnomalieWithoutMalus()
    {

    }

    // +150
    public void sendColis()
    {

    }

    // +550
    public void sendColisWithoutMalus()
    {

    }
}
