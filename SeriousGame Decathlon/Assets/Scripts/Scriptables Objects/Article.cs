using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nouveau Article", menuName = "NewArticle")]
public class Article : ScriptableObject
{
    public float poids;
    public RFID rfid;
    public Sprite sprite;
}
