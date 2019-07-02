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
    public List<Carton> carton;

    public ColisManager colisManage;

    //Variables Nécessaire pour le colis
    //A affecté au Colis
    private GameObject menuTourner;
    private GameObject spriteArticleTableUn;
    private GameObject spriteArticleTableDeux;
    private Image circleImage;

    public RepackTab repackTab;

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
            GameObject theNewColis = Instantiate(newColis, new Vector3(68.1f, -1.6f, 0), Quaternion.identity);
            scriptColis = theNewColis.GetComponent<ColisScript>();
            scriptColis.colisScriptable = Colis.CreateInstance<Colis>();

            //scriptColis.colisScriptable.carton.codeRef = buttonName;
            //scriptColis.colisScriptable.carton.Initialize();

            scriptColis.tournerMenu = menuTourner;
            scriptColis.spriteArticleTableUn = spriteArticleTableUn;
            scriptColis.spriteArticleTableDeux = spriteArticleTableDeux;
            scriptColis.circleImage = circleImage;

            if (scriptColis.colisScriptable.fillPercent > 0)
            {
                if (scriptColis.colisScriptable.fillPercent <= 50)
                {
                    scriptColis.spriteArticleDansColis.sprite = scriptColis.colisScriptable.listArticles[0].spriteList[0];
                }
                else if (scriptColis.colisScriptable.fillPercent >= 125)
                {
                    scriptColis.spriteArticleDansColis.sprite = scriptColis.colisScriptable.listArticles[0].spriteList[1];
                }
                else
                {
                    scriptColis.spriteArticleDansColis.sprite = scriptColis.colisScriptable.listArticles[0].spriteList[2];
                }
            }
            else
            {
                scriptColis.spriteArticleDansColis.sprite = null;
            }

            //scriptColis.textArtcileTableRFID = textArtcileTableRFID;
            //scriptColis.textArticleTableNombre = textArticleTableNombre;

            foreach (BoutonDirection bouton in listeBoutonsMenuTourner)
            {
                bouton.scriptColis = scriptColis;
            }

            scriptColis.colisScriptable.isBadOriented = false;
            if(scriptColis.IWayEtiquette != null && !scriptColis.colisScriptable.isBadOriented && scriptColis.colisScriptable.wayTicket != null)
            {
                scriptColis.IWayEtiquette.SetActive(true);
            }
            else
            {
                scriptColis.IWayEtiquette.SetActive(false);
            }

            if (buttonName == "CB02")
            {
                Debug.Log("Test CB2");
                scriptColis.colisScriptable.carton = carton[1];
            }
            else if (buttonName == "CB01")
            {
                Debug.Log("Test CB1");
                scriptColis.colisScriptable.carton = carton[0];
            }

            scriptColis.spriteMaskArticleColis.sprite = scriptColis.colisScriptable.carton.cartonOuvert;

            colisManage.scriptRotation.cartonObj = theNewColis;
            repackTab.colisVide = colisManage.scriptRotation.cartonObj;
            colisManage.scriptRotation.ColisEnter();
            colisManage.scriptRotation.UpdateSprite(scriptColis.colisScriptable.carton.spriteCartonsListe, theNewColis.GetComponent<SpriteRenderer>());
        }
        spriteArticleTableUn.GetComponent<PileArticle>().UpdatePileArticle();
        spriteArticleTableDeux.GetComponent<PileArticle>().UpdatePileArticle();
    }
}
