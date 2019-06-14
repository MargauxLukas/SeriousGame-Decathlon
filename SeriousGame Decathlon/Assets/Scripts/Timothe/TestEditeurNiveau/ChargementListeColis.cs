using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargementListeColis : MonoBehaviour
{
    public AnomalieDetection anomDetect;

    public ColisManager manageColis;

    public int nbLevel;
    public LevelScriptable currentLevel;

    public void ChargementNiveau()
    {
        List<Colis> newList = new List<Colis>();

        SaveLoadSystem.instance.LoadLevel(currentLevel);

        foreach(int nb in currentLevel.nbColisParNomColis)
        {
            for(int i = 0; i < nb; i++)
            {
                newList.Add(Colis.CreateInstance<Colis>());
            }
        }

        foreach(Colis colis in newList)
        {
            SaveLoadSystem.instance.LoadColis(colis);
        }

        manageColis.listeColisTraiter = newList;
    }
}
