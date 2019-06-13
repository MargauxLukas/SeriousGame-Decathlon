using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Nouveau Carton", menuName ="New Carton")]
public class Carton : ScriptableObject
{
    public int longueur;
    public int largeur;
    public int hauteur;
    public string codeRef;
    public string cartonName;

    [ExecuteAlways]
    public void Initialize()
    {
        cartonName.ToUpper();
        switch (codeRef)
        {
            case "CB01":
                cartonName = "Standard";
                longueur = 60;
                largeur = 40;
                hauteur = 40;
                break;

            case "CB02":
                cartonName = "Petit";
                longueur = 30;
                largeur = 20;
                hauteur = 20;
                break;

            case "CB03":
                cartonName = "Allumette";
                longueur = 60;
                largeur = 20;
                hauteur = 20;
                break;

            case "CB04":
                cartonName = "1/4 Standard";
                longueur = 40;
                largeur = 30;
                hauteur = 20;
                break;

            case "CB05":
                cartonName = "1/2 Standard";
                longueur = 60;
                largeur = 40;
                hauteur = 20;
                break;

            case "CB06":
                cartonName = "Standard Court";
                longueur = 40;
                largeur = 40;
                hauteur = 30;
                break;
        }
    }
}
