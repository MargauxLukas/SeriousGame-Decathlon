using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileArticle : MonoBehaviour
{
    public List<Article> listArticles;

    public void ChangeRFID(RFID refid)
    {
        foreach(Article art in listArticles)
        {
            art.rfid = refid;
        }
    }
}
