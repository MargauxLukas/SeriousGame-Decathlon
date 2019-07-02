using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PédaleTuto : MonoBehaviour
{
    /*public GameObjectsManager gameObjectsManager;

    int numColis = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (numColis == 1)
        {
            gameObjectsManager.GameObjectToColisScript(gameObjectsManager.colis1).doesEntrance = true;
        }
        else if (numColis == 2)
        {
            gameObjectsManager.GameObjectToColisScript(gameObjectsManager.colis2).doesEntrance = true;
        }
    }

    public void AppelColisTuto()
    {

        if (listeColisActuel.Length <= 0 && listeColisTraiter.Count > 0)
        {

            foreach (BoutonDirection bouton in listeBoutonsMenuTourner)
            {
                bouton.scriptColis = colisTemporaire.GetComponent<ColisScript>();
            }

            //scanPistol.scriptColis   = scriptColis;
            recountTab.colis = colisTemporaire;
            repackTab.colis = colisTemporaire;
            scriptRotation.cartonObj = colisTemporaire;
            scriptRotation.ColisEnter();
            Debug.Log("Test Colis Manager");
            scriptRotation.UpdateSprite(scriptColis.colisScriptable.carton.spriteCartonsListe, colisTemporaire.GetComponent<SpriteRenderer>());
            if (scriptColis.colisScriptable.estOuvert)
            {
                Color newColo = colisTemporaire.GetComponent<SpriteRenderer>().color;
                newColo.a = 0.3f;
                colisTemporaire.GetComponent<SpriteRenderer>().color = newColo;
            }

            //Debug.Log(listeColisTraiter[0]);
            listeColisTraiter.RemoveAt(0);
            if (listeColisTraiter.Count > 0 && listeColisTraiter[0] == null && listeColisTraiter[1] != null)
            {
                for (int i = 0; i < listeColisTraiter.Count - 1; i++)
                {
                    if (listeColisTraiter[i + 1] != null)
                    {
                        listeColisTraiter[i] = listeColisTraiter[i + 1];
                    }
                    else
                    {
                        listeColisTraiter.RemoveAt(i);
                    }
                }
            }

            /*if(scriptColis.colisScriptable.fillPercent<=50)
            {
                scriptColis.spriteArticleDansColis.sprite = scriptColis.colisScriptable.listArticles[0].spriteList[0];
            }
            else if(scriptColis.colisScriptable.fillPercent >= 125)
            {
                scriptColis.spriteArticleDansColis.sprite = scriptColis.colisScriptable.listArticles[0].spriteList[1];
            }
            else
            {
                scriptColis.spriteArticleDansColis.sprite = scriptColis.colisScriptable.listArticles[0].spriteList[2];
            }

            scriptColis.spriteMaskArticleColis.sprite = scriptColis.colisScriptable.carton.cartonOuvert;*/
       /* }
        else if (listeColisActuel.Length > 0)
        {
            Scoring.instance.MinorPenalty();
            Scoring.instance.AffichageErreur("Tu a déjà un colis a traiter");
        }

        spriteArticleTableUn.GetComponent<PileArticle>().UpdatePileArticle();
        spriteArticleTableDeux.GetComponent<PileArticle>().UpdatePileArticle();
    }*/
}
