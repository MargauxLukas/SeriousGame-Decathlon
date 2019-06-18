using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FicheCarton : MonoBehaviour
{
    //public string buttonName;
    public GameObject newColis;
    private ColisScript scriptColis;
    public Carton carton;

    public ColisManager colisManage;

    //Variables Nécessaire pour le colis
    //A affecté au Colis
    private GameObject menuTourner;
    private GameObject spriteArticleTableUn;
    private GameObject spriteArticleTableDeux;
    private Image circleImage;

   // public Text textArticleTableNombre;
    //public Text textArtcileTableRFID;

    //Auquel le colis doit être affecté
    private List<BoutonDirection> listeBoutonsMenuTourner;

    private void Start()
    {
        menuTourner = colisManage.menuTourner;
        spriteArticleTableUn = colisManage.spriteArticleTableUn;
        spriteArticleTableDeux = colisManage.spriteArticleTableUn;
        circleImage = colisManage.circleImage;
        listeBoutonsMenuTourner = colisManage.listeBoutonsMenuTourner;
    }

    public void InstantiateCarton(string buttonName)
    {
        colisManage.listeColisActuel = new GameObject[0];
        colisManage.listeColisActuel = GameObject.FindGameObjectsWithTag("Colis");

        if (colisManage.listeColisActuel.Length <= 1)
        {
            scriptColis = newColis.GetComponent<ColisScript>();
            scriptColis.colisScriptable = Colis.CreateInstance<Colis>();
            scriptColis.colisScriptable.carton = carton;
            scriptColis.colisScriptable.carton.codeRef = buttonName;
            scriptColis.colisScriptable.carton.Initialize();

            scriptColis.tournerMenu = menuTourner;
            scriptColis.spriteArticleTableUn = spriteArticleTableUn;
            scriptColis.spriteArticleTableDeux = spriteArticleTableDeux;
            scriptColis.circleImage = circleImage;


            //scriptColis.textArtcileTableRFID = textArtcileTableRFID;
            //scriptColis.textArticleTableNombre = textArticleTableNombre;

            foreach (BoutonDirection bouton in listeBoutonsMenuTourner)
            {
                bouton.scriptColis = scriptColis;
            }

            colisManage.scriptRotation.cartonObj = Instantiate(newColis, new Vector3(-2, -1.4f, 0), Quaternion.identity);
        }


        spriteArticleTableUn.GetComponent<PileArticle>().UpdatePileArticle();
        spriteArticleTableDeux.GetComponent<PileArticle>().UpdatePileArticle();
    }
}
