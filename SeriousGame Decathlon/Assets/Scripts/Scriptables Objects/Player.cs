using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nouveau Joueur", menuName = "New Player")]
public class Player : ScriptableObject
{
    public string name = "";
    public string date = System.DateTime.Now.ToString("dd MMMM yyyy");
    public int score = 0;
}
