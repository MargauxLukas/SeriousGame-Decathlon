using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BestScore", menuName = "Scoring")]
public class BestScoreScript : ScriptableObject
{
    public List<string> nomDesJoueurs;
    public List<int> scoreDesJoueurs;
}
