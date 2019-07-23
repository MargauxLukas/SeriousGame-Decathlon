using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerColisAttendu : MonoBehaviour
{
    public List<Colis> colisVoulus = new List<Colis>();

    public void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            colisVoulus.Add(new Colis());
            colisVoulus[i].listArticles = new List<Article>();
        }
    }

    public void AjoutArticleColisVoulu(Article art, int emplacement, int nombreArtVoulu)
    {
        for(int j = 0; j < nombreArtVoulu; j++)
        {
            colisVoulus[emplacement].listArticles.Add(art);
        }
    }

    public bool DetectionColis(Colis colisCompare, int emplacement)
    {
        if(colisCompare.listArticles.Count == colisVoulus[emplacement].listArticles.Count)
        {
            for(int i = 0; i < colisCompare.listArticles.Count; i++)
            {
                if (colisCompare.listArticles[i].rfid != colisVoulus[emplacement].listArticles[i].rfid)
                {
                    colisVoulus[emplacement] = new Colis();
                    return false;
                }
            }
            colisVoulus[emplacement] = new Colis();
            return true;
        }
        colisVoulus[emplacement] = new Colis();
        return false;
    }
}
