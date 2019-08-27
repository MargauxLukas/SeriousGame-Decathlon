using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SQLToCSV : MonoBehaviour
{
    public List<string> dataGeneral = new List<string>();
    public List<string> dataMF      = new List<string>();
    public List<string> dataRecep   = new List<string>();
    public List<string> dataGTP     = new List<string>();

    public bool isDataGeneralDone = false;
    public bool isDataMFDone      = false;
    public bool isDataRecepDone   = false;
    public bool isDataGTPDone     = false;

    public int maxGeneral;
    public int maxMF;
    public int maxRecep;
    public int maxGTP;

    public void setToAList(string data, int rank, string tab)
    {
        Debug.Log(data);
        switch (tab)
        {
            case "RankingAll":
                dataGeneral.Add(data);
                if(rank == maxGeneral)
                {
                    isDataGeneralDone = true;
                }
                break;
        }

        if(isDataGeneralDone)
        {
            SetToAFileData();
        }
    }

    public void SetToAFileData()
    {
        StreamWriter file = new StreamWriter(Application.persistentDataPath + "/data_ALL");

        foreach (string text in dataGeneral)
        {
            file.Write(text + "\r\n");
        }

        file.Close();
    }
}
