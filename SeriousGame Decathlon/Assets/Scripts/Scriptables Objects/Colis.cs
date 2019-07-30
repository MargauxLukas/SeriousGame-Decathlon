using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Nouveau Colis", menuName = "NewColis")]
public class Colis : ScriptableObject
{
    enum Direction { Up, Down, Right, Left, Forward, Back };

    [Header("INFO Colis")]
    public string name;

    public int PCB = 0;

    public float poids       = 0;
    public float fillPercent = 0;

    public bool     hasBeenRecount;
    public bool      isBadOriented;
    public bool          estOuvert;
    public bool           estAbime;
    public bool needQualityControl;
    public bool           aEteVide;

    [Header("Type Carton")]
    public Carton carton;

    [Header("Ticket HU")]
    public string nomWayTicket;
    public WayTicket wayTicket;

    [Header("Liste Article")]
    public List<Article> listArticles = new List<Article>();

    [Header("Liste Anomalie")]
    public int nbAnomalie;
    public List<string> listAnomalies;

    //Pour le GTP
    public bool comeFromInternet;
    public Article gtpSupposedToBe;

    public string provenance;
    Direction orientation;

    public SquareFace face;

    public void Initialize()
    {
        PCB = listArticles.Count;
        if(isBadOriented)
        {
            switch(Mathf.RoundToInt(Random.Range(1,5)))
            {
                case 1:
                    orientation = Direction.Up;
                    break;
                case 2:
                    orientation = Direction.Down;
                    break;
                case 3:
                    orientation = Direction.Right;
                    break;
                case 4:
                    orientation = Direction.Left;
                    break;
                case 5:
                    orientation = Direction.Back;
                    break;
            }
        }
        else
        {
            orientation = Direction.Forward;
        }
    }

    public void InitializeWayTicket(WayTicket newTicket)
    {
        wayTicket = newTicket;
    }

    public void OuvrirFermer()
    {
        estOuvert = !estOuvert;
    }

   /*public void Tourner(Vector2 direction, List<Sprite> spriteList)
    {
        switch(orientation)
        {
            case Direction.Up:
                if(direction.x > 0)
                {
                    orientation = Direction.Forward;
                }
                else if(direction.x < 0)
                {
                    orientation = Direction.Back;
                }
                else if (direction.y > 0)
                {
                    orientation = Direction.Right;
                }
                else if (direction.y < 0)
                {
                    orientation = Direction.Left;
                }
                break;
            case Direction.Down:
                if (direction.x < 0)
                {
                    orientation = Direction.Forward;
                }
                else if (direction.x > 0)
                {
                    orientation = Direction.Back;
                }
                else if (direction.y > 0)
                {
                    orientation = Direction.Right;
                }
                else if (direction.y < 0)
                {
                    orientation = Direction.Left;
                }
                break;
            case Direction.Left:
                if (direction.x > 0)
                {
                    orientation = Direction.Up;
                }
                else if (direction.x < 0)
                {
                    orientation = Direction.Down;
                }
                else if (direction.y > 0)
                {
                    orientation = Direction.Back;
                }
                else if (direction.y < 0)
                {
                    orientation = Direction.Forward;
                }
                break;
            case Direction.Right:
                if (direction.x < 0)
                {
                    orientation = Direction.Up;
                }
                else if (direction.x > 0)
                {
                    orientation = Direction.Down;
                }
                else if (direction.y > 0)
                {
                    orientation = Direction.Back;
                }
                else if (direction.y < 0)
                {
                    orientation = Direction.Forward;
                }
                break;
            case Direction.Back:
                if (direction.x > 0)
                {
                    orientation = Direction.Right;
                }
                else if (direction.x < 0)
                {
                    orientation = Direction.Left;
                }
                else if (direction.y > 0)
                {
                    orientation = Direction.Up;
                }
                else if (direction.y < 0)
                {
                    orientation = Direction.Down;
                }
                break;
            case Direction.Forward:
                if (direction.x < 0)
                {
                    orientation = Direction.Right;
                }
                else if (direction.x > 0)
                {
                    orientation = Direction.Left;
                }
                else if (direction.y > 0)
                {
                    orientation = Direction.Up;
                }
                else if (direction.y < 0)
                {
                    orientation = Direction.Down;
                }
                break;
        }
        if(orientation == Direction.Forward)
        {
            isBadOriented = false;
        }
        else
        {
            isBadOriented = true;
        }
        Debug.Log(orientation);

        //Changement de Sprite
        /*switch (orientation)
        {
            case Direction.Up:

            case Direction.Down:

            case Direction.Left:

            case Direction.Right:

            case Direction.Back:

                break;
            case Direction.Forward:

                break;
        }
    }*/

    public void UpdateRotation(List<SquareFace> listFace)
    {
        foreach(SquareFace face in listFace)
        {
            if(face.isCurrentlyPick)
            {
                if (face.face != "Forward")
                {
                    isBadOriented = true;
                }
                else if (isBadOriented)
                {
                    if (TutoManagerMulti.instance != null) {TutoManagerMulti.instance.Manager(27);}
                    isBadOriented = false;
                }
            }
        }
    }

    public List<Article> Vider()
    {
        List<Article> artcilePile;
        PCB   = 0;
        poids = 0;
        artcilePile = listArticles;
        listArticles = new List<Article>();
        return artcilePile;
    }

    public void Remplir (List<Article> newListArticle)
    {
        if(newListArticle == null)
        {
            listArticles = new List<Article>();
        }
        poids = 0;
        foreach(Article art in newListArticle)
        {
            poids += art.poids;
            listArticles.Add(art);
            PCB++;
        }
    }

    public void UpdateWeight()
    {
        if (listArticles.Count > 0)
        {
            poids = PCB * listArticles[0].poids;
        }
    }
}
