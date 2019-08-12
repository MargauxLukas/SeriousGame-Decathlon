using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadSystem : MonoBehaviour
{
    public static SaveLoadSystem instance;
    public bool hasReponse = false;

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

    }

    public void DeleteFileInDirectory(string direc)
    {

    }

    public void DeleteLevel(int levelNb)
    {

    }

    //FAIT
    public void SaveColis(Colis colisToSave)
    {
        if (colisToSave.wayTicket != null)
        {
            colisToSave.nomWayTicket = colisToSave.wayTicket.NamingTicket();

            //SaveWayTicket(colisToSave.wayTicket);
        }

        string json = JsonUtility.ToJson(colisToSave);
        Client.instance.SendColis(json, colisToSave.name);

        SaveWayTicket(colisToSave.wayTicket);
    }

    //FAIT
    public void SaveWayTicket(WayTicket ticket)
    {
        string json = JsonUtility.ToJson(ticket);
        Client.instance.SendWayticket(json, ticket.NamingTicket());
    }

    public Colis LoadColis(string colisName)
    {
        Colis colisToLoad = Colis.CreateInstance<Colis>();

        Net_Request request = new Net_Request();
        request.stringRequest = "Colis";
        request.colis = colisName;
        Client.instance.SendServer(request);

        JsonUtility.FromJsonOverwrite(Client.instance.swSave.file , colisToLoad);

        colisToLoad.wayTicket = LoadWayTicket(colisToLoad.nomWayTicket);

        return colisToLoad;
    }

    //FAIT
    public WayTicket LoadWayTicket(string ticket)
    {
        WayTicket newTicket = WayTicket.CreateInstance<WayTicket>();

        Net_Request request = new Net_Request();
        request.stringRequest = "WayTicket";
        request.colis = ticket;
        Client.instance.SendServer(request);

        JsonUtility.FromJsonOverwrite(Client.instance.swSave.file, newTicket);

        return newTicket;
    }

    public void SaveGeneralData(SavedData dataToSave)
    {

    }

    public SavedData LoadGeneralData()
    {
        SavedData dataToLoad = SavedData.CreateInstance<SavedData>();

        Net_Request request = new Net_Request();
        request.stringRequest = "GeneralData";
        Client.instance.SendServer(request);

        JsonUtility.FromJsonOverwrite(Client.instance.gdSave.file , dataToLoad);

        return dataToLoad;
    }

    //FAIT
    public void SaveLevelWithoutColis(LevelScriptable levelToSave)
    {
        string json = JsonUtility.ToJson(levelToSave);
        Client.instance.SendLevelWithoutColis(json, levelToSave.nbLevel);
    }


    //FAIT
    public void SaveLevel(LevelScriptable levelToSave, List<Colis> colisDuLevel)
    {
        if (levelToSave.name == null)
        {
            levelToSave.name = "Niveau " + levelToSave.nbLevel.ToString();
        }

        string json = JsonUtility.ToJson(levelToSave);
        Client.instance.SendLevel(json, levelToSave.nbLevel);

        if (colisDuLevel != null && colisDuLevel.Count > 0)
        {
            foreach (Colis lisco in colisDuLevel)
            {
                SaveColis(lisco);
            }
        }
    }

    //FAIT
    public LevelScriptable LoadLevel(int levelNb)
    {
        LevelScriptable levelToSave = LevelScriptable.CreateInstance<LevelScriptable>();

        Net_Request request = new Net_Request();
        request.stringRequest = "Level";
        request.integer = levelNb;
        Client.instance.SendServer(request);

        JsonUtility.FromJsonOverwrite(Client.instance.slSave.file, levelToSave);

        return levelToSave;
    }

    public void SaveScore(Player playerToSave)
    {

    }

    public void SaveBestBegin(BestScoreScript bestBegin)
    {

    }

    public void SaveBestScore(int score, string nom)
    {

    }

    public BestScoreScript LoadBestScore()
    {
        return null;
    }

   /* public void SaveBestScoreMF(int score, string nom)
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
    }*/
}
