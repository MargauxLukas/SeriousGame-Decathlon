using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSaveLoadScript : MonoBehaviour
{
    public ChargementListeColis chargeColis;

    public Colis colisToSaveSolo;
    public Colis colisToLoadSolo;
    public string nomColisLoad;

    public LevelScriptable levelToSave;
    public LevelScriptable levelToLoad;
    public List<Colis> colisToSaveLevel;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            SaveLoadSystem.instance.SaveColis(colisToSaveSolo);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            colisToLoadSolo = Colis.CreateInstance<Colis>();
            colisToLoadSolo = SaveLoadSystem.instance.LoadColis(nomColisLoad);
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            SaveLoadSystem.instance.SaveLevel(levelToSave, colisToSaveLevel);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            levelToLoad = LevelScriptable.CreateInstance<LevelScriptable>();
            levelToLoad = SaveLoadSystem.instance.LoadLevel(1);
        }
    }
}
