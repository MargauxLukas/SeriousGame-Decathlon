using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadSystem : MonoBehaviour
{
    public static SaveLoadSystem instance;
    public Server server;

    //private Colis colisToSave;

    //https://www.youtube.com/watch?v=SNwPq01yHds

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    public void DeleteAllFile()
    {
        if (IsSaveFile())
        {
            if (Directory.Exists(Application.persistentDataPath + "/game_save"))
            {
                DeleteFileInDirectory(Application.persistentDataPath + "/game_save");
                Application.Quit();
            }
        }
    }

    public void DeleteFileInDirectory(string direc)
    {
        string[] files = Directory.GetFiles(direc);
        string[] dirs = Directory.GetDirectories(direc);

        if (dirs.Length > 0)
        {
            foreach (string dir in dirs)
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

    //CLIENT -> SERVEUR
    public void SaveColis(string json, string name)
    {
        if (!IsSaveFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }

        if (!Directory.Exists(Application.persistentDataPath + "/game_save/colis_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/colis_data");
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game_save/colis_data/" + name + ".txt");
        bf.Serialize(file, json);
        file.Close();

        SavedData temporarySave = LoadGeneralDataIntern();
        if (temporarySave.nomColisConnus != null)
        {
            if (!temporarySave.nomColisConnus.Contains(name))
            {
                temporarySave.nomColisConnus.Add(name);
            }
        }
        else
        {
            temporarySave.nomColisConnus = new List<string>();
            temporarySave.nomColisConnus.Add(name);
        }
        SaveGeneralData(temporarySave);
    }

    //CLIENT -> SERVEUR
    public void SaveWayTicket(string json, string name)
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
        FileStream file = File.Create(Application.persistentDataPath + "/game_save/wayTicket_data/" + name + ".txt");
        bf.Serialize(file, json);
        file.Close();
    }

    //SERVEUR -> CLIENT
    public string LoadColis(string colisName)
    {
        if (!IsSaveFile())
        {
            return null;
        }

        string save = string.Empty;
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/game_save/colis_data/" + colisName + ".txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/colis_data/" + colisName + ".txt", FileMode.Open);
            save = (string)bf.Deserialize(file);
            file.Close();
        }

        return save;
    }


    //SERVEUR -> CLIENT
    public string LoadWayTicket(string ticket)
    {
        if (!IsSaveFile())
        {
            return null;
        }

        string save = string.Empty;
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/game_save/wayTicket_data/" + ticket + ".txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/wayTicket_data/" + ticket + ".txt", FileMode.Open);
            save = (string)bf.Deserialize(file);
            file.Close();
        }
        return save;
    }

    public void SaveGeneralData(SavedData dataToSave)
    {
        if (!IsSaveFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }

        BinaryFormatter bf = new BinaryFormatter();
        //dataToSave = Versionning(dataToSave);
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

    public void LoadGeneralData(int connectId, int channelId, int recHostId)
    {
        if (!IsSaveFile())
        {
            return;
        }
        else
        {
            SavedData dataToLoad = SavedData.CreateInstance<SavedData>();
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(Application.persistentDataPath + "/game_save/generalData.txt"))
            {
                FileStream file = File.Open(Application.persistentDataPath + "/game_save/generalData.txt", FileMode.Open);
                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), dataToLoad);
                file.Close();
                SendLevel(dataToLoad, connectId, channelId, recHostId);
            }
        }
    }

    public void LoadGeneralDataAndSavingFile(int connectId, int channelId, int recHostId)
    {
        Net_SendGDAndSF sgdsf = new Net_SendGDAndSF();

        if (Directory.Exists(Application.persistentDataPath + "/game_save"))
        {
            sgdsf.isSaveFile = true;
        }
        else
        {
            sgdsf.isSaveFile = false;
        }

        SavedData dataToLoad = SavedData.CreateInstance<SavedData>();
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/game_save/generalData.txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/generalData.txt", FileMode.Open);
            string dataSaved = (string)bf.Deserialize(file);
            sgdsf.dataSaved = dataSaved;
            file.Close();
            server.SendClient(recHostId, connectId, sgdsf);
        }
    }

    public void SendLevel(SavedData dataSaved, int connectId, int channelId, int recHostId)
    {
        BinaryFormatter bf = new BinaryFormatter();

        for (int i = 1; i <= dataSaved.nombreNiveauCree; i++)
        {
            string save = SaveLoadSystem.instance.LoadLevel(i);
            LevelScriptable levelScript = new LevelScriptable();
            Colis colisScript = new Colis();
            JsonUtility.FromJsonOverwrite(save, levelScript);
            Net_SendLevel sl = new Net_SendLevel();
            sl.file = save;
            sl.nbLevel = i;

            server.SendClient(recHostId, connectId, sl);

            for (int nbColis = 0 ; nbColis < levelScript.colisDuNiveauNoms.Count ; nbColis++)
            {
                Net_SendColisMF scmf = new Net_SendColisMF();
                scmf.fileColisMF = LoadColis(levelScript.colisDuNiveauNoms[nbColis]);
                JsonUtility.FromJsonOverwrite(scmf.fileColisMF, colisScript);
                scmf.fileticket = LoadWayTicket(colisScript.nomWayTicket);
                scmf.nbLevel = i;

                server.SendClient(recHostId, connectId, scmf);
            }

            for (int nbColis = 0; nbColis < levelScript.colisDuNiveauNomReception.Count; nbColis++)
            {
                Net_SendColisRecep scr = new Net_SendColisRecep();
                scr.fileColisRecep = LoadColis(levelScript.colisDuNiveauNomReception[nbColis]);
                JsonUtility.FromJsonOverwrite(scr.fileColisRecep, colisScript);
                scr.fileticket = LoadWayTicket(colisScript.nomWayTicket);
                scr.nbLevel = i;

                server.SendClient(recHostId, connectId, scr);
            }
        }
    }

    public SavedData LoadGeneralDataIntern()
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


    //FAIT
    public void SaveLevelWithoutColis(string json, int nbLevel)
    {
        if (!IsSaveFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }

        if (!Directory.Exists(Application.persistentDataPath + "/game_save/level_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/level_data");
        }

        BinaryFormatter bf = new BinaryFormatter();

        if (!File.Exists(Application.persistentDataPath + "/game_save/level_data/Level" + nbLevel + ".txt"))
        {
            SavedData temporarySave = LoadGeneralDataIntern();
            temporarySave.nombreNiveauCree++;
            nbLevel = temporarySave.nombreNiveauCree;

            FileStream file = File.Create(Application.persistentDataPath + "/game_save/level_data/Level" + nbLevel + ".txt");
            bf.Serialize(file, json);
            file.Close();

            SaveGeneralData(temporarySave);
        }
        else
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/level_data/Level" + nbLevel + ".txt", FileMode.Open);
            bf.Serialize(file, json);
            file.Close();
        }
    }

    //FAIT
    public void SaveLevel(string json, int nbLevel)
    {
        if (!IsSaveFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }

        if (!Directory.Exists(Application.persistentDataPath + "/game_save/level_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/level_data");
        }

        BinaryFormatter bf = new BinaryFormatter();
        if (!File.Exists(Application.persistentDataPath + "/game_save/level_data/Level" + nbLevel + ".txt"))
        {
            SavedData temporarySave = LoadGeneralDataIntern();
            temporarySave.nombreNiveauCree++;
            nbLevel = temporarySave.nombreNiveauCree;

            FileStream file = File.Create(Application.persistentDataPath + "/game_save/level_data/Level" + nbLevel + ".txt");
            bf.Serialize(file, json);
            file.Close();

            SaveGeneralData(temporarySave);
        }
        else
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/level_data/Level" + nbLevel + ".txt", FileMode.Open);
            bf.Serialize(file, json);
            file.Close();
        }
    }

    //FAIT
    public string LoadLevel(int levelNb)
    {
        if (!IsSaveFile())
        {
            return null;
        }

        string save = string.Empty;
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/game_save/level_data/Level" + levelNb.ToString() + ".txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/level_data/Level" + levelNb.ToString() + ".txt", FileMode.Open);
            save = (string)bf.Deserialize(file);

            file.Close();
        }

        return save;
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

    public void SaveBestBegin(BestScoreScript bestBegin)
    {
        if (!IsSaveFile())
        {
            Debug.Log("Test");
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }

        BinaryFormatter bf = new BinaryFormatter();

        if (!File.Exists(Application.persistentDataPath + "/game_save/hallOfFame.txt"))
        {
            FileStream file = File.Create(Application.persistentDataPath + "/game_save/hallOfFame.txt");
            var json = JsonUtility.ToJson(bestBegin);
            bf.Serialize(file, json);
            file.Close();
        }
    }

    public void SaveBestScore(int score, string nom)
    {
        if (!IsSaveFile())
        {
            Debug.Log("Test");
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }

        BinaryFormatter bf = new BinaryFormatter();
        BestScoreScript newBest = new BestScoreScript();

        if (File.Exists(Application.persistentDataPath + "/game_save/hallOfFame.txt"))
        {
            newBest = LoadBestScore();
        }

        if (newBest.nomDesJoueurs != null && newBest.nomDesJoueurs.Count > 0)
        {
            if (score >= newBest.scoreDesJoueurs[newBest.scoreDesJoueurs.Count - 1])
            {
                newBest.scoreDesJoueurs[newBest.scoreDesJoueurs.Count - 1] = score;
                newBest.nomDesJoueurs[newBest.nomDesJoueurs.Count - 1] = nom;
                for (int i = newBest.nomDesJoueurs.Count - 2; i >= 0; i--)
                {
                    if (score >= newBest.scoreDesJoueurs[i])
                    {
                        int tempoScore = newBest.scoreDesJoueurs[i];
                        string tempoNom = newBest.nomDesJoueurs[i];
                        newBest.scoreDesJoueurs[i] = score;
                        newBest.nomDesJoueurs[i] = nom;
                        newBest.scoreDesJoueurs[i + 1] = tempoScore;
                        newBest.nomDesJoueurs[i + 1] = tempoNom;
                    }
                }
            }
        }

        if (!File.Exists(Application.persistentDataPath + "/game_save/hallOfFame.txt"))
        {
            FileStream file = File.Create(Application.persistentDataPath + "/game_save/hallOfFame.txt");
            var json = JsonUtility.ToJson(newBest);
            bf.Serialize(file, json);
            file.Close();
        }
        else
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/hallOfFame.txt", FileMode.Open);
            var json = JsonUtility.ToJson(newBest);
            bf.Serialize(file, json);
            file.Close();
        }
    }

    public BestScoreScript LoadBestScore()
    {
        if (!IsSaveFile())
        {
            return null;
        }
        BestScoreScript hallToLoad = BestScoreScript.CreateInstance<BestScoreScript>();

        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/game_save/hallOfFame.txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/hallOfFame.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), hallToLoad);
            file.Close();
        }
        return hallToLoad;
    }
}
