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
            case "RankingTabAll":
                dataGeneral.Add(data);
                if(rank == maxGeneral)
                {
                    isDataGeneralDone = true;
                }
                break;

            case "RankingMFAll":
                dataMF.Add(data);
                if (rank == maxMF)
                {
                    isDataMFDone = true;
                }
                break;

            case "RankingRecepAll":
                dataRecep.Add(data);
                if (rank == maxRecep)
                {
                    isDataRecepDone = true;
                }
                break;

            case "RankingGTPAll":
                dataGTP.Add(data);
                if (rank == maxGTP)
                {
                    isDataGTPDone = true;
                }
                break;
        }

        if(isDataGeneralDone && isDataGTPDone && isDataMFDone && isDataRecepDone)
        {
            SetToAFileData();
            SetToAFileMF();
            SetToAFileRecep();
            SetToAFileGTP();
        }
    }

    public void SetToAFileData()
    {
        StreamWriter file = new StreamWriter(Application.persistentDataPath + "/data_general");

        foreach (string text in dataGeneral)
        {
            file.Write(text + "\r\n");
        }

        file.Close();
    }
    public void SetToAFileMF()
    {
        StreamWriter file = new StreamWriter(Application.persistentDataPath + "/data_MF");

        foreach (string text in dataMF)
        {
            file.Write(text + "\n");
        }

        file.Close();
    }
    public void SetToAFileRecep()
    {
        StreamWriter file = new StreamWriter(Application.persistentDataPath + "/data_Recep");

        foreach (string text in dataRecep)
        {
            file.Write(text + "\n");
        }

        file.Close();
    }
    public void SetToAFileGTP()
    {
        StreamWriter file = new StreamWriter(Application.persistentDataPath + "/data_GTP");

        foreach (string text in dataGTP)
        {
            file.Write(text + "\n");
        }

        file.Close();
    }
}
