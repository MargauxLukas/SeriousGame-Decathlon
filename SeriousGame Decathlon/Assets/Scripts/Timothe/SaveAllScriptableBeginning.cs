using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAllScriptableBeginning : MonoBehaviour
{

    public List<Colis> allColisCreated;
    public List<LevelScriptable> allLevelCreated;

    public int currentVersion;
    
    void Start()
    {
        if(!SaveLoadSystem.instance.IsSaveFile())
        {
            foreach(Colis coli in allColisCreated)
            {
                SaveLoadSystem.instance.SaveColis(coli);
            }

            foreach(LevelScriptable level in allLevelCreated)
            {
                SaveLoadSystem.instance.SaveLevelWithoutColis(level);
            }
        }
        else if(SaveLoadSystem.instance.LoadGeneralData().version != currentVersion)
        {
            SavedData newData = SaveLoadSystem.instance.LoadGeneralData();
            newData.version = currentVersion;
            SaveLoadSystem.instance.SaveGeneralData(newData);

            foreach (Colis coli in allColisCreated)
            {
                SaveLoadSystem.instance.SaveColis(coli);
            }

            foreach (LevelScriptable level in allLevelCreated)
            {
                SaveLoadSystem.instance.SaveLevelWithoutColis(level);
            }
        }
    }

    public void Desinstall()
    {
        SaveLoadSystem.instance.DeleteAllFile();
    }

}
