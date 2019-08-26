using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nouveau Niveau", menuName = "NewLevel")]
public class LevelScriptable : ScriptableObject
{
    public bool doesNeedMF;
    public bool doesNeedRecep;
    public bool doesNeedGTP;

    public List<string> colisDuNiveauNoms;
    public List<int> nbColisParNomColis;
    public string name;
    public int nbLevel;

    //Réception
    public List<string> colisDuNiveauNomReception;
    public float chanceReceptionColisHaveAnomalie; //Si à 100, veut dire que le container est défectueux
    public int nombreColisReception;

    //GTP
    public float nbColisVoulu;
    public float chanceMauvaisArticle;
    public float chanceAllMauvaisArticle;
    public float chancePasRemplit;
    public float chanceInternet;
    public float ChanceTropArticle;

    public List<Colis> listColis      = new List<Colis>();
    public List<Colis> listColisMF    = new List<Colis>();
    public List<Colis> listColisRecep = new List<Colis>();

    public void AddColis(Colis colis)
    {
        if(!colisDuNiveauNoms.Contains(colis.name))
        {
            colisDuNiveauNoms.Add(colis.name);
        }
    }

    public void AddColisMF(Colis colis)
    {     
        listColisMF.Add(colis);
        listColis  .Add(colis);   
    }

    public void AddColisRecep(Colis colis)
    {
        listColisRecep.Add(colis);
        listColis     .Add(colis);
    }
}
