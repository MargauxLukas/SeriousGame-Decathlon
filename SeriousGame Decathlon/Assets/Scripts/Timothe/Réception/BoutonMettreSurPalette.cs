using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonMettreSurPalette : MonoBehaviour
{
    public TapisRoulant tapisRoule;
    public void PoserSurPalette()
    {
        if(TutoManagerRecep.instance != null && tapisRoule.lastColis != null)
        {
           if(tapisRoule.lastColis.GetComponent<ScriptColisRecep>().colisScriptable.estAbime || tapisRoule.lastColis.GetComponent<ScriptColisRecep>().colisScriptable.carton.codeRef == "CBGrand")
           {
                tapisRoule.turnMenu.SetActive(false);
                tapisRoule.colisSurLeTapis.Remove(tapisRoule.lastColis);
                Destroy(tapisRoule.lastColis);
                TutoManagerRecep.instance.Manager(13);
           }
           else
           {
                tapisRoule.turnMenu.SetActive(false);
                //tapisRoule.OpenTurnMenu();
           }
        }

        if(TutoManagerRecep.instance == null && tapisRoule.lastColis != null)
        {
            if(tapisRoule.lastColis.GetComponent<ScriptColisRecep>() != null)
            {
                if(!tapisRoule.lastColis.GetComponent<ScriptColisRecep>().colisScriptable.estAbime && tapisRoule.lastColis.GetComponent<ScriptColisRecep>().colisScriptable.carton.codeRef != "CBGrand")
                {
                    Scoring.instance.RecepMalus(90);
                    Scoring.instance.AffichageErreur("Tu as posé un colis sans problème sur la palette");
                }
                else
                {
                    Scoring.instance.RecepRenvoieColis();
                }
                tapisRoule.turnMenu.SetActive(false);
                tapisRoule.colisSurLeTapis.Remove(tapisRoule.lastColis);
                Destroy(tapisRoule.lastColis);
            }
        }
    }
}
