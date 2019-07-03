using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadSystem : MonoBehaviour
{
    public static SaveLoadSystem instance;
    public AnomalieDetection detection;

    //private Colis colisToSave;

    //https://www.youtube.com/watch?v=SNwPq01yHds

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    public void DeleteAllFile()
    {
        if(IsSaveFile())
        {
            if (Directory.Exists(Application.persistentDataPath + "/game_save"))
            {
                DeleteFileInDirectory(Application.persistentDataPath + "/game_save");
            }
        }
    }

    public void DeleteFileInDirectory(string direc)
    {
        string[] files = Directory.GetFiles(direc);
        string[] dirs = Directory.GetDirectories(direc);

        if(dirs.Length > 0)
        {
            foreach(string dir in dirs)
            {
                DeleteFileInDirectory(dir);
            }
        }

        foreach (string file in files)
        {
            File.SetAttributes(file, FileAttributes.Normal);
            File.Delete(file);
        }

        Directory.Delete(direc);
    }

    public bool IsSaveFile()
    {
        return Directory.Exists(Application.persistentDataPath + "/game_save");
    }

    public void DeleteLevel(int levelNb)
    {
        if (!IsSaveFile())
        {
            return;
        }

        if (File.Exists(Application.persistentDataPath + "/game_save/level_data/Level" + levelNb.ToString() + ".txt"))
        {
            File.Delete(Application.persistentDataPath + "/game_save/level_data/Level" + levelNb.ToString() + ".txt");
        }
    }

    public void SaveColis(Colis colisToSave)
    {
        if(!IsSaveFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }

        if (!Directory.Exists(Application.persistentDataPath + "/game_save/colis_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/colis_data");
        }

        if (colisToSave.wayTicket != null)
        {
            colisToSave.nomWayTicket = colisToSave.wayTicket.NamingTicket();

            SaveWayTicket(colisToSave.wayTicket);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game_save/colis_data/" + colisToSave.name + ".txt");
        var json = JsonUtility.ToJson(colisToSave);
        bf.Serialize(file, json);
        file.Close();

        SavedData temporarySave = LoadGeneralData();
        Debug.Log(colisToSave.name);
        if (temporarySave.nomColisConnus != null)
        {
            if (!temporarySave.nomColisConnus.Contains(colisToSave.name))
            {
                temporarySave.nomColisConnus.Add(colisToSave.name);
            }
        }
        else
        {
            temporarySave.nomColisConnus = new List<string>();
            temporarySave.nomColisConnus.Add(colisToSave.name);
        }
        SaveGeneralData(temporarySave);
    }

    public void SaveWayTicket(WayTicket ticket)
    {
        if (!IsSaveFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }

        if (!Directory.Exists(Application.persistentDataPath + "/game_save/wayTicket_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/wayTicket_data");
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game_save/wayTicket_data/" + ticket.NamingTicket() + ".txt");
        var json = JsonUtility.ToJson(ticket);
        bf.Serialize(file, json);
        file.Close();
    }

    public Colis LoadColis(/*Colis colisToLoad, */string colisName)
    {
        if (!IsSaveFile())
        {
            return null;
        }

        Colis colisToLoad = Colis.CreateInstance<Colis>();
        BinaryFormatter bf = new BinaryFormatter();
        if(File.Exists(Application.persistentDataPath + "/game_save/colis_data/" + colisName + ".txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/colis_data/" + colisName + ".txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), colisToLoad);
            file.Close();
        }

        colisToLoad.wayTicket = LoadWayTicket(colisToLoad.nomWayTicket);

        return colisToLoad;
    }

    public WayTicket LoadWayTicket(string ticket)
    {
        if (!IsSaveFile())
        {
            return null;
        }

        WayTicket newTicket = WayTicket.CreateInstance<WayTicket>();
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/game_save/wayTicket_data/" + ticket + ".txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/wayTicket_data/" + ticket + ".txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), newTicket);
            file.Close();
        }

        return newTicket;
    }

    public void SaveGeneralData(SavedData dataToSave)
    {
        if (!IsSaveFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }

        BinaryFormatter bf = new BinaryFormatter();
        if (!File.Exists(Application.persistentDataPath + "/game_save/generalData.txt"))
        {
            FileStream file = File.Create(Application.persistentDataPath + "/game_save/generalData.txt");
            var json = JsonUtility.ToJson(dataToSave);
            bf.Serialize(file, json);
            file.Close();
        }
        else
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/generalData.txt", FileMode.Open);
            var json = JsonUtility.ToJson(dataToSave);
            bf.Serialize(file, json);
            file.Close();
        }
    }

    public SavedData Versionning (SavedData dataSaved)
    {
        dataSaved.version += 1;
        return dataSaved;
    }

    public SavedData LoadGeneralData()
    {
        if (!IsSaveFile())
        {
            return null;
        }

        SavedData dataToLoad = SavedData.CreateInstance<SavedData>();

        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/game_save/generalData.txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/generalData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), dataToLoad);
            file.Close();
        }
        return dataToLoad;
    }

    public void SaveLevelWithoutColis(LevelScriptable levelToSave)
    {
        if (!IsSaveFile())
        {
            Debug.Log("Test");
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }

        if (!Directory.Exists(Application.persistentDataPath + "/game_save/level_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/level_data");
        }

        BinaryFormatter bf = new BinaryFormatter();
        if (!File.Exists(Application.persistentDataPath + "/game_save/level_data/Level" + levelToSave.nbLevel.ToString() + ".txt"))
        {
            SavedData temporarySave = LoadGeneralData();
            temporarySave.nombreNiveauCree++;
            levelToSave.nbLevel = temporarySave.nombreNiveauCree;

            FileStream file = File.Create(Application.persistentDataPath + "/game_save/level_data/Level" + levelToSave.nbLevel.ToString() + ".txt");
            var json = JsonUtility.ToJson(levelToSave);
            bf.Serialize(file, json);
            file.Close();

            SaveGeneralData(temporarySave);
        }
        else
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/level_data/Level" + levelToSave.nbLevel.ToString() + ".txt", FileMode.Open);
            var json = JsonUtility.ToJson(levelToSave);
            bf.Serialize(file, json);
            file.Close();
        }
    }

    public void SaveLevel(LevelScriptable levelToSave, List<Colis> colisDuLevel)
    {
        if (!IsSaveFile())
        {
            Debug.Log("Test");
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }

        if (!Directory.Exists(Application.persistentDataPath + "/game_save/level_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/level_data");
        }

        BinaryFormatter bf = new BinaryFormatter();
        if (!File.Exists(Application.persistentDataPath + "/game_save/level_data/Level" + levelToSave.nbLevel.ToString() + ".txt"))
        {
            SavedData temporarySave = LoadGeneralData();
            temporarySave.nombreNiveauCree++;
            levelToSave.nbLevel = temporarySave.nombreNiveauCree;

            FileStream file = File.Create(Application.persistentDataPath + "/game_save/level_data/Level" + levelToSave.nbLevel.ToString() + ".txt");
            var json = JsonUtility.ToJson(levelToSave);
            bf.Serialize(file, json);
            file.Close();

            SaveGeneralData(temporarySave);
        }
        else
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/level_data/Level" + levelToSave.nbLevel.ToString() + ".txt", FileMode.Open);
            var json = JsonUtility.ToJson(levelToSave);
            bf.Serialize(file, json);
            file.Close();
        }

        //colisDuLevel = levelToSave.colisToSave;

        if (colisDuLevel != null && colisDuLevel.Count > 0)
        {
            foreach (Colis lisco in colisDuLevel)
            {
                SaveColis(lisco);
            }
        }
    }

    public LevelScriptable LoadLevel(int levelNb)
    {
        if (!IsSaveFile())
        {
            return null;
        }

        LevelScriptable levelToSave = LevelScriptable.CreateInstance<LevelScriptable>();

        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/game_save/level_data/Level" + levelNb.ToString() + ".txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/level_data/Level" + levelNb.ToString() + ".txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), levelToSave);
            file.Close();
            Debug.Log("Bonjour : " + levelToSave.colisDuNiveauNoms.Count);
        }

        Debug.Log("nb level : " + levelNb);

        Debug.Log(File.Exists(Application.persistentDataPath + "/game_save/level_data/Level" + levelNb.ToString() + ".txt"));

        return levelToSave;
    }

    public void SaveScore(Player playerToSave)
    {
        if (!IsSaveFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }

        string path = Application.persistentDataPath + "/game_save/allScoringSave.txt";
        string content = playerToSave.name + ";" + System.DateTime.Now.ToString("dd MMMM yyyy") + ";" + playerToSave.score + "\r\n";

        if (!File.Exists(path))
        {
            File.WriteAllText(path, content);
        }
        else
        {
            File.AppendAllText(path, content);
        }
    }
}
