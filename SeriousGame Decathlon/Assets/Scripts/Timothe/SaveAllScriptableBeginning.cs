using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAllScriptableBeginning : MonoBehaviour
{

    public List<Colis> allColisCreated;
    public List<LevelScriptable> allLevelCreated;
    
    void Awake()
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
    }

}
