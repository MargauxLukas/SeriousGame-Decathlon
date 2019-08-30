using Mono.Data.Sqlite;
using System;
using System.IO;
using UnityEngine.UI;
using UnityEngine;
using System.Data;
using System.Collections.Generic;

public class DBAccess : MonoBehaviour
{
    public List<string> listTable = new List<string> { "RankingTabAll", "RankingMFAll", "RankingRecepAll", "RankingGTPAll"};                         //Liste des Tables SQL (Je prends pas en compte RankingAll que je remplis de toute évidence

    public void Start()
    {
        string connection = "URI=file:" + Application.persistentDataPath + "/RankingDatabase";                                                       //Lien de l'emplacement du fichier SQL (Pour y acceder : C:\Users\Designer\AppData\LocalLow\DefaultCompany\DecathlonMeca-Server )                            
        IDbConnection dbcon = new SqliteConnection(connection);                                                                                      //On donne le lien auquel on veut accéder 
        dbcon.Open();                                                                                                                                //On ouvre la base de donnée (IMPORTANT : Penser à la fermer lorsqu'on l'utilise plus (Après une recherche, un INSERT, Un UPDATE ...)

        IDbCommand dbcmd;                                                                                                                            //Sert à prendre une requête SQL avec "dbcmd.CommandText = ... " et à l'éxecuter avec "dbcmd.ExecuteNonQuery()".                                            
        IDataReader reader;                                                                                                                          //Sert à lire une requête, utile lorsqu'on veut juste avoir des informations avec  "reader = cmnd_read.ExecuteReader()"

        #region Create Table
        #region RankingGeneral
        dbcmd = dbcon.CreateCommand();

        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS 'RankingTabAll'( " +
                           " 'rank' INTEGER NOT NULL UNIQUE, " +
                           " 'name' TEXT NOT NULL, " +
                           " 'score' INTEGER NOT NULL, " +
                           " 'date' TEXT NOT NULL" +
                           ");";
        dbcmd.ExecuteNonQuery();
        #endregion

        #region RankingMF
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS 'RankingMFAll'( " +
                   " 'rank' INTEGER NOT NULL UNIQUE, " +
                   " 'name' TEXT NOT NULL, " +
                   " 'score' INTEGER NOT NULL, " +
                   " 'date' TEXT NOT NULL" +
                   ");";
        dbcmd.ExecuteNonQuery();
        #endregion

        #region RankingRecep
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS 'RankingRecepAll'( " +
           " 'rank' INTEGER NOT NULL UNIQUE, " +
           " 'name' TEXT NOT NULL, " +
           " 'score' INTEGER NOT NULL, " +
           " 'date' TEXT NOT NULL" +
           ");";
        dbcmd.ExecuteNonQuery();
        #endregion

        #region RankingGTP
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS 'RankingGTPAll'( " +
           " 'rank' INTEGER NOT NULL UNIQUE, " +
           " 'name' TEXT NOT NULL, " +
           " 'score' INTEGER NOT NULL, " +
           " 'date' TEXT NOT NULL" +
           ");";
        dbcmd.ExecuteNonQuery();
        #endregion

        #region RankingAll
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS 'RankingAll'( " +
                   " 'nb' INTEGER NOT NULL, " +
                   " 'name' TEXT NOT NULL, " +
                   " 'scoreG' INTEGER NOT NULL, " +
                   " 'scoreMF' INTEGER NOT NULL, " +
                   " 'scoreRecep' INTEGER NOT NULL, " +
                   " 'scoreGTP' INTEGER NOT NULL, " +
                   " 'date' TEXT NOT NULL" +
                   ");";
        dbcmd.ExecuteNonQuery();

        /*IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "INSERT INTO 'RankingAll' (nb, name, scoreG, scoreMF, scoreRecep, scoreGTP, date) VALUES (1, 'test',50 ,50 ,50 ,50,'66/06/2070')";
        cmnd.ExecuteNonQuery();*/
        #endregion
        #endregion

        #region Initialisation and Delete
        //Insert dans la table
        /*IDbCommand cmnd = dbcon.CreateCommand();
        foreach(string tab in listTable)
        {
            cmnd.CommandText = "INSERT INTO '" + tab + "' (rank, name, score, date) VALUES (1, 'moi1', 50, '66/06/2070')";
            cmnd.ExecuteNonQuery();
            cmnd.CommandText = "INSERT INTO '" + tab + "' (rank, name, score, date) VALUES (2, 'moi2', 45, '66/06/2070')";
            cmnd.ExecuteNonQuery();
            cmnd.CommandText = "INSERT INTO '" + tab + "' (rank, name, score, date) VALUES (3, 'moi3', 40, '66/06/2070')";
            cmnd.ExecuteNonQuery();
            cmnd.CommandText = "INSERT INTO '" + tab + "' (rank, name, score, date) VALUES (4, 'moi4', 30, '66/06/2070')";
            cmnd.ExecuteNonQuery();
            cmnd.CommandText = "INSERT INTO '" + tab + "' (rank, name, score, date) VALUES (5, 'moi5', 25, '66/06/2070')";
            cmnd.ExecuteNonQuery();
            cmnd.CommandText = "INSERT INTO '" + tab + "' (rank, name, score, date) VALUES (6, 'moi6', 20, '66/06/2070')";
            cmnd.ExecuteNonQuery();
            cmnd.CommandText = "INSERT INTO '" + tab + "' (rank, name, score, date) VALUES (7, 'moi7', 15, '66/06/2070')";
            cmnd.ExecuteNonQuery();
            cmnd.CommandText = "INSERT INTO '" + tab + "' (rank, name, score, date) VALUES (8, 'moi8', 10, '66/06/2070')";
            cmnd.ExecuteNonQuery();
            cmnd.CommandText = "INSERT INTO '" + tab + "' (rank, name, score, date) VALUES (9, 'moi9',  5, '66/06/2070')";
            cmnd.ExecuteNonQuery();
            cmnd.CommandText = "INSERT INTO '" + tab + "' (rank, name, score, date) VALUES (10,'moi10', 1, '66/06/2070')";
            cmnd.ExecuteNonQuery();
        }*/

        //Delete All
        //IDbCommand cmnd = dbcon.CreateCommand();
        /*cmnd.CommandText = "DELETE FROM 'RankingTab' where rank = " + 1;
        cmnd.ExecuteNonQuery();
        cmnd.CommandText = "DELETE FROM 'RankingTab' where rank = " + 2;
        cmnd.ExecuteNonQuery();
        cmnd.CommandText = "DELETE FROM 'RankingTab' where rank = " + 3;
        cmnd.ExecuteNonQuery();
        cmnd.CommandText = "DELETE FROM 'RankingTab' where rank = " + 4;
        cmnd.ExecuteNonQuery();
        cmnd.CommandText = "DELETE FROM 'RankingTab' where rank = " + 5;
        cmnd.ExecuteNonQuery();
        cmnd.CommandText = "DELETE FROM 'RankingTab' where rank = " + 6;
        cmnd.ExecuteNonQuery();
        cmnd.CommandText = "DELETE FROM 'RankingTab' where rank = " + 7;
        cmnd.ExecuteNonQuery();
        cmnd.CommandText = "DELETE FROM 'RankingTab' where rank = " + 8;
        cmnd.ExecuteNonQuery();
        cmnd.CommandText = "DELETE FROM 'RankingTab' where rank = " + 9;
        cmnd.ExecuteNonQuery();*/
        //cmnd.CommandText = "DELETE FROM 'RankingTab' where rank = " + 10;
        // cmnd.ExecuteNonQuery();
        #endregion

        //Lire toute la table
        /*
        IDbCommand cmnd_read = dbcon.CreateCommand();

        cmnd_read.CommandText = "SELECT * FROM 'RankingTab' ";
        reader = cmnd_read.ExecuteReader();
        */

        dbcon.Close();
    }

    #region GetHallOfFame
    /**************************************************
     *      Permet d'avoir le nom des joueurs         *
     *************************************************/
    public string getHallOfFameName(int rank, string tab)
    {
        string connection = "URI=file:" + Application.persistentDataPath + "/RankingDatabase";
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        IDbCommand cmnd_read = dbcon.CreateCommand();
        cmnd_read.CommandText = "SELECT name FROM '" + tab + "' where rank =" + rank;
        IDataReader reader = cmnd_read.ExecuteReader();

        return reader[0].ToString();
    }

    /**************************************************
    *      Permet d'avoir le score des joueurs        *
    ***************************************************/
    public int getHallOfFameScore(int rank, string tab)
    {
        string connection = "URI=file:" + Application.persistentDataPath + "/RankingDatabase";
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        IDbCommand cmnd_read = dbcon.CreateCommand();
        cmnd_read.CommandText = "SELECT score FROM '" + tab + "' where rank =" + rank;
        IDataReader reader = cmnd_read.ExecuteReader();

        return int.Parse(reader[0].ToString());
    }

    /******************************************************************
    *      Met à jour le classement avec les informations reçu        *
    *******************************************************************/
    public void SetRanking(int score, string name, string date, int scoreMF, int scoreRecep, int scoreGTP)
    {
        int rankG = 0;
        int rankMF = 0;
        int rankRecep = 0;
        int rankGTP = 0;

        string connection = "URI=file:" + Application.persistentDataPath + "/RankingDatabase";
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        #region ScoreGeneral
        for (int i = 10; i > 0; i--)
        {
            IDbCommand cmndScore = dbcon.CreateCommand();
            cmndScore.CommandText = "SELECT score FROM 'RankingTabAll' where rank =" + i.ToString();
            IDataReader readerScore = cmndScore.ExecuteReader();

            if (int.Parse(readerScore[0].ToString()) >= score)
            {
                break;
            }
            else { rankG = i; }
            readerScore.Close();
        }
        #endregion

        #region ScoreMF
        for (int i = 10; i > 0; i--)
        {
            IDbCommand cmndScore = dbcon.CreateCommand();
            cmndScore.CommandText = "SELECT score FROM 'RankingMFAll' where rank =" + i.ToString();
            IDataReader readerScore;
            readerScore = cmndScore.ExecuteReader();

            if (int.Parse(readerScore[0].ToString()) >= scoreMF)
            {
                break;
            }
            else { rankMF = i; }
            readerScore.Close();
        }
        #endregion

        #region ScoreRecep
            for (int i = 10; i > 0; i--)
            {
                IDbCommand cmndScore = dbcon.CreateCommand();
                cmndScore.CommandText = "SELECT score FROM 'RankingRecepAll' where rank =" + i.ToString();
                IDataReader readerScore;
                readerScore = cmndScore.ExecuteReader();

            if (int.Parse(readerScore[0].ToString()) >= scoreRecep)
            {
                break;
            }
            else { rankRecep = i; }
            readerScore.Close();
        }
        #endregion

        #region ScoreGTP
        for (int i = 10; i > 0; i--)
        {
            IDbCommand cmndScore = dbcon.CreateCommand();
            cmndScore.CommandText = "SELECT score FROM 'RankingGTPAll' where rank =" + i.ToString();
            IDataReader readerScore;
            readerScore = cmndScore.ExecuteReader();

            if (int.Parse(readerScore[0].ToString()) >= scoreGTP)
            {
                break;
            }
            else { rankMF = i; }
            readerScore.Close();
        }
        #endregion

        #region ScoreTOUT

        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "SELECT MAX(nb) FROM RankingAll";
        IDataReader readercmnd = cmnd.ExecuteReader();

        int nbGeneral = int.Parse(readercmnd[0].ToString());
        nbGeneral++;

        cmnd_read.CommandText = "INSERT INTO 'RankingAll' VALUES ('" + nbGeneral + "' , '" +
                                                                       name       + "' , '" +
                                                                       score      + "' , '" +
                                                                       scoreMF    + "' , '" +
                                                                       scoreRecep + "' , '" +
                                                                       scoreGTP   + "' , '" +
                                                                       date +     "')";
        cmnd_read.ExecuteNonQuery();
        #endregion
        

        dbcon.Close();

        if (rankG != 0) { TriRanking(score, name, rankG, date, "RankingTabAll"); }
        if (rankMF != 0){TriRanking(scoreMF, name, rankMF, date, "RankingMFAll");}
        if (rankRecep != 0){TriRanking(scoreRecep, name, rankRecep, date, "RankingRecepAll");}
        if (rankGTP != 0){TriRanking(scoreGTP, name, rankGTP, date, "RankingGTPAll");}
    }

    /************************************
    *         C'est pour trier          *
    *************************************/
    public void TriRanking(int scoreT, string nameT, int rankT, string date, string tab)
    {
        string connection = "URI=file:" + Application.persistentDataPath + "/RankingDatabase";
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        IDbCommand cmndReader = dbcon.CreateCommand();
        cmndReader.CommandText = "DELETE FROM '" + tab + "' where rank = 10";
        cmndReader.ExecuteNonQuery();

        for (int i = 10; i > 0; i--)
        {
            int nbToUpdate = i + 1;
            if (rankT == i)
            {
                cmndReader.CommandText = "UPDATE '" + tab + "' SET rank = " + nbToUpdate + " WHERE rank = " + i.ToString();
                cmndReader.ExecuteNonQuery();

                cmndReader.CommandText = "INSERT INTO '" + tab + "' VALUES ( '" + rankT + "' , '" +
                                                                            nameT + "' , '" +
                                                                            scoreT + "' , '" +
                                                                            date + "')";
                cmndReader.ExecuteNonQuery();
                dbcon.Close();
                break;
            }
            else
            {
                Debug.Log("Rank " + i + " devient rank " + nbToUpdate);
                cmndReader.CommandText = "UPDATE '" + tab + "' SET rank = " + nbToUpdate + " WHERE rank = " + i.ToString();
                cmndReader.ExecuteNonQuery();
            }
        }
    }
    #endregion

    public Net_SendNbData SendnbData()
    {
        Net_SendNbData SNBD = new Net_SendNbData();
        string connection = "URI=file:" + Application.persistentDataPath + "/RankingDatabase";
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        IDbCommand cmnd_read = dbcon.CreateCommand();
        cmnd_read.CommandText = "SELECT MAX(nb) FROM RankingAll";
        IDataReader reader = cmnd_read.ExecuteReader();

        SNBD.nbGeneral = int.Parse(reader[0].ToString());

        return SNBD;
    }

    public Net_SendAllData SendAllData(string tab, int rank)
    {
        Net_SendAllData sad = new Net_SendAllData();
        string connection = "URI=file:" + Application.persistentDataPath + "/RankingDatabase";
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        IDbCommand cmnd_read = dbcon.CreateCommand();
        cmnd_read.CommandText = "SELECT * FROM '" + tab + "' WHERE nb =" + rank;
        IDataReader reader = cmnd_read.ExecuteReader();

        sad.tab = tab;
        sad.data = reader[1].ToString() + ";" + reader[2].ToString() + ";" + reader[3].ToString() + ";" + reader[4].ToString() + ";" + reader[5].ToString() + ";" + reader[6].ToString();
        sad.rank = rank;

        return sad;
    }
}
