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
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    public bool IsSaveFile()
    {
        return Directory.Exists(Application.persistentDataPath + "/game_save");
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

        colisToSave.nomWayTicket = colisToSave.wayTicket.NamingTicket();

        SaveWayTicket(colisToSave.wayTicket);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game_save/colis_data/" + colisToSave.name + ".txt");
        Debug.Log(File.Exists(Application.persistentDataPath + "/game_save/colis_data/" + colisToSave.name + ".txt"));
        var json = JsonUtility.ToJson(colisToSave);
        bf.Serialize(file, json);
        file.Close();
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
        Debug.Log(File.Exists(Application.persistentDataPath + "/game_save/wayTicket_data/" + ticket.NamingTicket() + ".txt"));
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
        }

        return newTicket;
    }

    public void SaveLevel(LevelScriptable levelToSave, List<Colis> colisDuLevel)
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
        FileStream file = File.Create(Application.persistentDataPath + "/game_save/level_data/Level" + levelToSave.nbLevel.ToString() + ".txt");
        var json = JsonUtility.ToJson(levelToSave);
        bf.Serialize(file, json);
        file.Close();

        foreach(Colis lisco in colisDuLevel)
        {
            SaveColis(lisco);
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
            Debug.Log("Test");
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/level_data/Level" + levelNb.ToString() + ".txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), levelToSave);
        }
        return levelToSave;
    }
}
