using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nouveau Article", menuName = "NewArticle")]
public class Article : ScriptableObject
{
    public float poids;
    public RFID rfid;
    public List<Sprite> spriteList;
    public Sprite spriteGTP;
    public Sprite spritePackGTP;
    public Sprite photoGTP;
}
