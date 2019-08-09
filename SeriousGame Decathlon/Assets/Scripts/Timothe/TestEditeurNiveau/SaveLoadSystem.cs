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
}
