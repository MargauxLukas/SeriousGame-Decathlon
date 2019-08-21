using Mono.Data.Sqlite;
using System;
using System.IO;
using UnityEngine.UI;
using UnityEngine;
using System.Data;
using System.Collections.Generic;

public class DBAccess : MonoBehaviour
{
    public List<string> listTable = new List<string> { "RankingTabAll", "RankingMFAll", "RankingRecepAll", "RankingGTPAll"};

    public void Start()
    {
        string connection = "URI=file:" + Application.persistentDataPath + "/RankingDatabase";
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        IDbCommand dbcmd;
        IDataReader reader;

        #region Create Table
        #region RankingGeneral
        dbcmd = dbcon.CreateCommand();
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS 'RankingTab'( " +
                            " 'rank' INTEGER NOT NULL UNIQUE, " +
                            " 'name' TEXT NOT NULL, " +
                            " 'score' INTEGER NOT NULL, " +
                            " 'date' TEXT NOT NULL" +
                            ");";
        dbcmd.ExecuteNonQuery();

        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS 'RankingTabAll'( " +
                           " 'rank' INTEGER NOT NULL UNIQUE, " +
                           " 'name' TEXT NOT NULL, " +
                           " 'score' INTEGER NOT NULL, " +
                           " 'date' TEXT NOT NULL" +
                           ");";
        dbcmd.ExecuteNonQuery();
        #endregion

        #region RankingMF
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS 'RankingMF'( " +
                           " 'rank' INTEGER NOT NULL UNIQUE, " +
                           " 'name' TEXT NOT NULL, " +
                           " 'score' INTEGER NOT NULL, " +
                           " 'date' TEXT NOT NULL" +
                           ");";
        dbcmd.ExecuteNonQuery();

        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS 'RankingMFAll'( " +
                   " 'rank' INTEGER NOT NULL UNIQUE, " +
                   " 'name' TEXT NOT NULL, " +
                   " 'score' INTEGER NOT NULL, " +
                   " 'date' TEXT NOT NULL" +
                   ");";
        dbcmd.ExecuteNonQuery();
        #endregion

        #region RankingRecep
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS 'RankingRecep'( " +
                   " 'rank' INTEGER NOT NULL UNIQUE, " +
                   " 'name' TEXT NOT NULL, " +
                   " 'score' INTEGER NOT NULL, " +
                   " 'date' TEXT NOT NULL" +
                   ");";
        dbcmd.ExecuteNonQuery();

        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS 'RankingRecepAll'( " +
           " 'rank' INTEGER NOT NULL UNIQUE, " +
           " 'name' TEXT NOT NULL, " +
           " 'score' INTEGER NOT NULL, " +
           " 'date' TEXT NOT NULL" +
           ");";
        dbcmd.ExecuteNonQuery();
        #endregion

        #region RankingGTP
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS 'RankingGTP'( " +
                   " 'rank' INTEGER NOT NULL UNIQUE, " +
                   " 'name' TEXT NOT NULL, " +
                   " 'score' INTEGER NOT NULL, " +
                   " 'date' TEXT NOT NULL" +
                   ");";
        dbcmd.ExecuteNonQuery();

        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS 'RankingGTPAll'( " +
           " 'rank' INTEGER NOT NULL UNIQUE, " +
           " 'name' TEXT NOT NULL, " +
           " 'score' INTEGER NOT NULL, " +
           " 'date' TEXT NOT NULL" +
           ");";
        dbcmd.ExecuteNonQuery();
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

    public void CanCloseDB()
    {
        //
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
        string connection = "URI=file:" + Application.persistentDataPath + "/RankingDatabase";
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        #region ScoreGeneral
        IDbCommand cmnd_read = dbcon.CreateCommand();
        cmnd_read.CommandText = "SELECT MAX(rank) FROM RankingTabAll";
        IDataReader reader = cmnd_read.ExecuteReader();

        int max = int.Parse(reader[0].ToString());
        int rank = int.Parse(reader[0].ToString());
        int min = 1;

        if (score > 0)
        {
            for (int i = (min+max)/2; i <= rank; i--)
            {   
                cmnd_read.CommandText = "SELECT score FROM 'RankingTabAll' where rank =" + i.ToString();
                reader = cmnd_read.ExecuteReader();

                if (int.Parse(reader[0].ToString()) > score)
                {
                    min = (min + max)/2;
                }
                else if(int.Parse(reader[0].ToString()) < score)
                {
                    max = (min + max) / 2;
                }
                else
                {
                    rank = i+1;
                    string tab = "RankingTabAll";
                    TriRanking(score, name, rank, date, tab);
                    break;
                }
            }
        }
        else
        {
            //Je prefere ne pas mettre de rang à 0 pour le moment
            /*cmnd_read.CommandText = "INSERT INTO 'RankingTabAll' VALUES ( '" + max   + "' , '" +
                                                                            name  + "' , '" + 
                                                                            score + "' , '" + 
                                                                            date  + "')";
            reader = cmnd_read.ExecuteReader();
            dbcon.Close();
            return;*/
        }
        #endregion

        #region ScoreMF
        cmnd_read = dbcon.CreateCommand();
        cmnd_read.CommandText = "SELECT MAX(rank) FROM RankingMFAll";
        reader = cmnd_read.ExecuteReader();

        max = int.Parse(reader[0].ToString());
        rank = int.Parse(reader[0].ToString());
        min = 1;

        if (scoreMF > 0)
        {
            for (int i = (min + max) / 2; i <= rank; i--)
            {
                cmnd_read.CommandText = "SELECT score FROM 'RankingMFAll' where rank =" + i.ToString();
                reader = cmnd_read.ExecuteReader();

                if (int.Parse(reader[0].ToString()) > scoreMF)
                {
                    min = (min + max) / 2;
                }
                else if (int.Parse(reader[0].ToString()) < scoreMF)
                {
                    max = (min + max) / 2;
                }
                else
                {
                    rank = i + 1;
                    string tab = "RankingMFAll";
                    TriRanking(scoreMF, name, rank, date, tab);
                    break;
                }
            }
        }
        #endregion

        #region ScoreRecep
        cmnd_read = dbcon.CreateCommand();
        cmnd_read.CommandText = "SELECT MAX(rank) FROM RankingRecepAll";
        reader = cmnd_read.ExecuteReader();

        max = int.Parse(reader[0].ToString());
        rank = int.Parse(reader[0].ToString());
        min = 1;

        if (scoreRecep > 0)
        {
            for (int i = (min + max) / 2; i <= rank; i--)
            {
                cmnd_read.CommandText = "SELECT score FROM 'RankingRecepAll' where rank =" + i.ToString();
                reader = cmnd_read.ExecuteReader();

                if (int.Parse(reader[0].ToString()) > scoreRecep)
                {
                    min = (min + max) / 2;
                }
                else if (int.Parse(reader[0].ToString()) < scoreRecep)
                {
                    max = (min + max) / 2;
                }
                else
                {
                    rank = i + 1;
                    string tab = "RankingRecepAll";
                    TriRanking(scoreRecep, name, rank, date, tab);
                    break;
                }
            }
        }
        #endregion

        #region ScoreGTP
        cmnd_read = dbcon.CreateCommand();
        cmnd_read.CommandText = "SELECT MAX(rank) FROM RankingGTPAll";
        reader = cmnd_read.ExecuteReader();

        max = int.Parse(reader[0].ToString());
        rank = int.Parse(reader[0].ToString());
        min = 1;

        if (scoreGTP > 0)
        {
            for (int i = (min + max) / 2; i <= rank; i--)
            {
                cmnd_read.CommandText = "SELECT score FROM 'RankingGTPAll' where rank =" + i.ToString();
                reader = cmnd_read.ExecuteReader();

                if (int.Parse(reader[0].ToString()) > scoreGTP)
                {
                    min = (min + max) / 2;
                }
                else if (int.Parse(reader[0].ToString()) < scoreGTP)
                {
                    max = (min + max) / 2;
                }
                else
                {
                    rank = i + 1;
                    string tab = "RankingGTPAll";
                    TriRanking(scoreGTP, name, rank, date, tab);
                    break;
                }
            }
        }
        #endregion
    }


    /************************************
    *         C'est pour trier          *
    *************************************/
    public void TriRanking(int scoreT, string nameT, int rankT, string date, string tab)
    {
        string connection = "URI=file:" + Application.persistentDataPath + "/RankingDatabase";
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();
        
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "SELECT MAX(rank) FROM  '" + tab + "'";
        IDataReader reader = cmnd.ExecuteReader();

        int max = int.Parse(reader[0].ToString());

        for (int i = max; i > 0; i--)
        {
            int nbToUpdate = i - 1;
            if (rankT == i)
            {
                cmnd.CommandText = "UPDATE '" + tab +"' SET rank = " + i + " WHERE rank = " + nbToUpdate;
                cmnd.ExecuteNonQuery();

                cmnd.CommandText = "INSERT INTO '" + tab + "' VALUES ( '" + rankT  + "' , '" +  
                                                                            nameT  + "' , '" +  
                                                                            scoreT + "' , '" + 
                                                                            date   + "')" ;
                cmnd.ExecuteNonQuery();
                dbcon.Close();
                break;
            }
            else
            {
                cmnd.CommandText = "UPDATE '" + tab + "' SET rank = " + i + " WHERE rank = " + nbToUpdate;
                cmnd.ExecuteNonQuery();
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
        cmnd_read.CommandText = "SELECT MAX(rank) FROM RankingTabAll";
        IDataReader reader = cmnd_read.ExecuteReader();

        SNBD.nbGeneral = int.Parse(reader[0].ToString());

        cmnd_read = dbcon.CreateCommand();
        cmnd_read.CommandText = "SELECT MAX(rank) FROM RankingMFAll";
        reader = cmnd_read.ExecuteReader();

        SNBD.nbMF = int.Parse(reader[0].ToString());

        cmnd_read = dbcon.CreateCommand();
        cmnd_read.CommandText = "SELECT MAX(rank) FROM RankingRecepAll";
        reader = cmnd_read.ExecuteReader();

        SNBD.nbRecep = int.Parse(reader[0].ToString());

        cmnd_read = dbcon.CreateCommand();
        cmnd_read.CommandText = "SELECT MAX(rank) FROM RankingGTPAll";
        reader = cmnd_read.ExecuteReader();

        SNBD.nbGTP = int.Parse(reader[0].ToString());

        return SNBD;
    }

    public Net_SendAllData SendAllData(string tab, int rank)
    {
        Net_SendAllData sad = new Net_SendAllData();
        string connection = "URI=file:" + Application.persistentDataPath + "/RankingDatabase";
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        IDbCommand cmnd_read = dbcon.CreateCommand();
        cmnd_read.CommandText = "SELECT * FROM '" + tab + "' WHERE rank =" + rank;
        IDataReader reader = cmnd_read.ExecuteReader();

        sad.tab = tab;
        sad.data = reader[0].ToString() + "," + reader[1].ToString() + "," + reader[2].ToString() + "," + reader[3].ToString();
        sad.rank = rank;

        return sad;
    }
}
