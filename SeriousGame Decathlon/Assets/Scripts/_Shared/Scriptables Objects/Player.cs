using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nouveau Joueur", menuName = "New Player")]
public class Player : ScriptableObject
{
    public string name  = "";
    public string date  = System.DateTime.Now.ToString("dd MMMM yyyy");
    public    int score = 0;
    public int scoreGTP;
    public int scoreReception;
    public int scoreMultifonction;

    public List<string> erreursReception;
    public List<string> erreursMultifonction;
    public List<string> erreursGTP;

    public List<int> nbErreursReception;
    public List<int> nbErreursMultifonction;
    public List<int> nbErreursGTP;
}
