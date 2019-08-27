using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nouveau RFID", menuName = "NewRFID")]
public class RFID : ScriptableObject
{
    public bool estFonctionnel;
    public RefArticle refArticle;
}
