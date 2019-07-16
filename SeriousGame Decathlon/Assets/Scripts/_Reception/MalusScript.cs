using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalusScript : MonoBehaviour
{
    public static MalusScript instance;

    public GaugeConvoyeur gaugeConvoyeur;
    public CreationDePalette palette;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        //DontDestroyOnLoad(gameObject);
    }

    public void HaveAMalus()
    {
        if(gaugeConvoyeur.currentLevel == palette.GetNBColonnes())
        {
            //Pas de malus
        }
        else
        {
            Scoring.instance.LosePointOnTime();
        }
    }
}
