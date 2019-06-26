using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSaveLoadScript : MonoBehaviour
{
    public ChargementListeColis chargeColis;

    public Colis colisToSaveSolo;
    public Colis colisToLoadSolo;
    public string nomColisLoad;

    public LevelScriptable levelToSave;
    public LevelScriptable levelToLoad;
    public List<Colis> colisToSaveLevel;

    public Text saveColisText;
    public Text saveLevelText;
    public Text loadColisText;
    public Text loadLevelText;

    public List<Player> playerList;

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
            loadColisText.text = (colisToLoadSolo != null).ToString();
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            SaveLoadSystem.instance.SaveLevel(levelToSave, colisToSaveLevel);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            levelToLoad = LevelScriptable.CreateInstance<LevelScriptable>();
            levelToLoad = SaveLoadSystem.instance.LoadLevel(1);
            loadLevelText.text = (levelToLoad != null).ToString();
        }
        else if(Input.GetKeyDown(KeyCode.Y))
        {
            foreach (Player perso in playerList)
            {
                SaveLoadSystem.instance.SaveScore(perso);
            }       
        }
    }

    public void SaveColis()
    {
        SaveLoadSystem.instance.SaveColis(colisToSaveSolo);
    }

    public void LoadColis()
    {
        colisToLoadSolo = Colis.CreateInstance<Colis>();
        colisToLoadSolo = SaveLoadSystem.instance.LoadColis(nomColisLoad);
        loadColisText.text = (colisToLoadSolo != null).ToString();
    }

    public void SaveLevel()
    {
        SaveLoadSystem.instance.SaveLevel(levelToSave, colisToSaveLevel);
    }

    public void LoadLevel()
    {
        levelToLoad = LevelScriptable.CreateInstance<LevelScriptable>();
        levelToLoad = SaveLoadSystem.instance.LoadLevel(1001);
        loadLevelText.text = (levelToLoad != null).ToString();
    }
}
