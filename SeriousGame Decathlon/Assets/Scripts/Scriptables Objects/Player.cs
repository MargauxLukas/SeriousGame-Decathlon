using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : ScriptableObject
{
    public string name = "";
    public string date = System.DateTime.Now.ToString("dd MMMM yyyy");
    public int score = 0;
}
