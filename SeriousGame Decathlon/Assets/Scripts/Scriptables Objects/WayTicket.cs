using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nouveau WayTicket", menuName = "NewWayTicket")]
public class WayTicket : ScriptableObject
{
    public int PCB;
    public RefArticle refArticle;
    public int numeroCodeBarre;
    public float poids;

    public string NamingTicket()
    {
        string nom = "PCB" + PCB + "ART" + refArticle.numeroRef + "NUM" + numeroCodeBarre + "POI" + Mathf.RoundToInt(poids);
        return nom;
    }
}
