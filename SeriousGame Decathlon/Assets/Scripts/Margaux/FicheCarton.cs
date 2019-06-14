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
    private GameObject spriteArticleTable;
    private Image circleImage;

    //Auquel le colis doit être affecté
    private List<BoutonDirection> listeBoutonsMenuTourner;

    private void Start()
    {
        menuTourner = colisManage.menuTourner;
        spriteArticleTable = colisManage.spriteArticleTable;
        circleImage = colisManage.circleImage;
        listeBoutonsMenuTourner = colisManage.listeBoutonsMenuTourner;
    }

    public void InstantiateCarton(string buttonName)
    {
        scriptColis = newColis.GetComponent<ColisScript>();
        scriptColis.colisScriptable = Colis.CreateInstance<Colis>();
        scriptColis.colisScriptable.carton = carton;
        scriptColis.colisScriptable.carton.codeRef = buttonName;
        scriptColis.colisScriptable.carton.Initialize();

        scriptColis.doesEntrance = true;
        scriptColis.tournerMenu = menuTourner;
        scriptColis.spriteArticleTable = spriteArticleTable;
        scriptColis.circleImage = circleImage;

        foreach (BoutonDirection bouton in listeBoutonsMenuTourner)
        {
            bouton.scriptColis = scriptColis;
        }

        Instantiate(newColis, new Vector3(-2, -1.4f, 0),Quaternion.identity);
    }
}
