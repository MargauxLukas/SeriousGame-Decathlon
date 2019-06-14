using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nouveau Niveau", menuName = "NewLevel")]
public class LevelScriptable : ScriptableObject
{
    public List<string> colisDuNiveauNoms;
    public List<int> nbColisParNomColis;
    public int nbLevel;

    public void AddColis(Colis colis)
    {
        if(!colisDuNiveauNoms.Contains(colis.name))
        {
            colisDuNiveauNoms.Add(colis.name);
        }
    }
}
