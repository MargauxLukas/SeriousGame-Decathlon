using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadSystem : MonoBehaviour
{
    public static SaveLoadSystem instance;

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
            Destroy(instance.gameObject);
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    public void DeleteAllFile()
    {
        
    }

    public void DeleteFileInDirectory(string direc)
    {
        
    }

    public void DeleteLevel(int levelNb)
    {
        
    }

    public void SaveColis(Colis colisToSave)
    {
        SaveWayTicket(colisToSave.wayTicket);
    }

    public void SaveWayTicket(WayTicket ticket)
    {
        string json = JsonUtility.ToJson(ticket);
        Client.instance.SendWayticket(json, ticket.NamingTicket());
    }

    public Colis LoadColis(string colisName)
    {
        return null;
    }

    public WayTicket LoadWayTicket(string ticket)
    {
        return null;
    }

    public void SaveGeneralData(SavedData dataToSave)
    {
       
    }

    public SavedData LoadGeneralData()
    {
        return null;
    }

    public void SaveLevelWithoutColis(LevelScriptable levelToSave)
    {
        
    }

    public void SaveLevel(LevelScriptable levelToSave, List<Colis> colisDuLevel)
    {
        
    }

    public LevelScriptable LoadLevel(int levelNb)
    {
        return null;
    }

    public void SaveScore(Player playerToSave)
    {
        
        if (!IsSaveFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }

        string path = Application.persistentDataPath + "/game_save/allScoringSave.txt";
        string content = playerToSave.name + ";" + System.DateTime.Now.ToString("dd MMMM yyyy") + ";" + playerToSave.score + "\r\n";

        SaveBestScore(playerToSave.score, playerToSave.name);
        SaveBestScoreMF(playerToSave.scoreMultifonction, playerToSave.name);
        SaveBestScoreGTP(playerToSave.scoreGTP, playerToSave.name);
        SaveBestScoreRecep(playerToSave.scoreReception, playerToSave.name);

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

        if (!File.Exists(Application.persistentDataPath + "/game_save/hallOfFameMF.txt"))
        {
            FileStream file = File.Create(Application.persistentDataPath + "/game_save/hallOfFameMF.txt");
            var json = JsonUtility.ToJson(bestBegin);
            bf.Serialize(file, json);
            file.Close();
        }

        if (!File.Exists(Application.persistentDataPath + "/game_save/hallOfFameGTP.txt"))
        {
            FileStream file = File.Create(Application.persistentDataPath + "/game_save/hallOfFameGTP.txt");
            var json = JsonUtility.ToJson(bestBegin);
            bf.Serialize(file, json);
            file.Close();
        }

        if (!File.Exists(Application.persistentDataPath + "/game_save/hallOfFameRecep.txt"))
        {
            FileStream file = File.Create(Application.persistentDataPath + "/game_save/hallOfFameRecep.txt");
            var json = JsonUtility.ToJson(bestBegin);
            bf.Serialize(file, json);
            file.Close();
        }
    }

    public void SaveBestScore(int score, string nom)
    {
        
    }

    public void SaveBestScoreMF(int score, string nom)
    {
        if (!IsSaveFile())
        {
            Debug.Log("Test");
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }

        BinaryFormatter bf = new BinaryFormatter();
        BestScoreScript newBest = new BestScoreScript();

        if (File.Exists(Application.persistentDataPath + "/game_save/hallOfFameMF.txt"))
        {
            newBest = LoadBestScoreMF();
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

        if (!File.Exists(Application.persistentDataPath + "/game_save/hallOfFameMF.txt"))
        {
            FileStream file = File.Create(Application.persistentDataPath + "/game_save/hallOfFameMF.txt");
            var json = JsonUtility.ToJson(newBest);
            bf.Serialize(file, json);
            file.Close();
        }
        else
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/hallOfFameMF.txt", FileMode.Open);
            var json = JsonUtility.ToJson(newBest);
            bf.Serialize(file, json);
            file.Close();
        }
    }

    public void SaveBestScoreGTP(int score, string nom)
    {
        if (!IsSaveFile())
        {
            Debug.Log("Test");
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }

        BinaryFormatter bf = new BinaryFormatter();
        BestScoreScript newBest = new BestScoreScript();

        if (File.Exists(Application.persistentDataPath + "/game_save/hallOfFameGTP.txt"))
        {
            newBest = LoadBestScoreGTP();
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

        if (!File.Exists(Application.persistentDataPath + "/game_save/hallOfFameGTP.txt"))
        {
            FileStream file = File.Create(Application.persistentDataPath + "/game_save/hallOfFameGTP.txt");
            var json = JsonUtility.ToJson(newBest);
            bf.Serialize(file, json);
            file.Close();
        }
        else
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/hallOfFameGTP.txt", FileMode.Open);
            var json = JsonUtility.ToJson(newBest);
            bf.Serialize(file, json);
            file.Close();
        }
    }

    public void SaveBestScoreRecep(int score, string nom)
    {
        if (!IsSaveFile())
        {
            Debug.Log("Test");
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }

        BinaryFormatter bf = new BinaryFormatter();
        BestScoreScript newBest = new BestScoreScript();

        if (File.Exists(Application.persistentDataPath + "/game_save/hallOfFameRecep.txt"))
        {
            newBest = LoadBestScoreRecep();
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

        if (!File.Exists(Application.persistentDataPath + "/game_save/hallOfFameRecep.txt"))
        {
            FileStream file = File.Create(Application.persistentDataPath + "/game_save/hallOfFameRecep.txt");
            var json = JsonUtility.ToJson(newBest);
            bf.Serialize(file, json);
            file.Close();
        }
        else
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/hallOfFameRecep.txt", FileMode.Open);
            var json = JsonUtility.ToJson(newBest);
            bf.Serialize(file, json);
            file.Close();
        }
    }

    public BestScoreScript LoadBestScore()
    {
        return null;
    }

    public BestScoreScript LoadBestScoreMF()
    {
        if (!IsSaveFile())
        {
            return null;
        }
        BestScoreScript hallToLoad = BestScoreScript.CreateInstance<BestScoreScript>();

        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/game_save/hallOfFameMF.txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/hallOfFameMF.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), hallToLoad);
            file.Close();
        }
        return hallToLoad;
    }

    public BestScoreScript LoadBestScoreGTP()
    {
        if (!IsSaveFile())
        {
            return null;
        }
        BestScoreScript hallToLoad = BestScoreScript.CreateInstance<BestScoreScript>();

        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/game_save/hallOfFameGTP.txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/hallOfFameGTP.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), hallToLoad);
            file.Close();
        }
        return hallToLoad;
    }

    public BestScoreScript LoadBestScoreRecep()
    {
        if (!IsSaveFile())
        {
            return null;
        }
        BestScoreScript hallToLoad = BestScoreScript.CreateInstance<BestScoreScript>();

        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/game_save/hallOfFameRecep.txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/hallOfFameRecep.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), hallToLoad);
            file.Close();
        }
        return hallToLoad;
    }
}
