using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Nouveau Colis", menuName = "NewColis")]
public class Colis : ScriptableObject
{
    public string name;

    enum Direction { Up, Down, Right, Left, Forward, Back };

    public int PCB = 0;
    public Carton carton;
    public List<Article> listArticles;
    public WayTicket wayTicket;
    public string nomWayTicket;
    public float poids = 0;
    public float fillPercent = 0;
    public bool isBadOriented;
    public bool estOuvert;
    public bool estAbime;
    public int nbAnomalie;
    public List<string> listAnomalies;
    public string provenance;
    Direction orientation;
    public bool needQualityControl;

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

    public void Tourner(Vector2 direction, List<Sprite> spriteList)
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
        }*/
    }

    public List<Article> Vider()
    {
        List<Article> artcilePile;
        PCB = 0;
        poids = 0;
        artcilePile = listArticles;
        listArticles = new List<Article>();
        return artcilePile;
    }

    public void Remplir (int newPCB, List<Article> newListArticle)
    {
        poids = 0;
        foreach(Article art in newListArticle)
        {
            poids += art.poids;
        }
        PCB = newPCB;
        listArticles = newListArticle;
    }
}
