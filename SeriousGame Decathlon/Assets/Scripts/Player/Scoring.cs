using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public static Scoring instance;

    public GameObject player;
    public int score;

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
}
